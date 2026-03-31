using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using RoadSafe.API.Models;
using RoadSafe.TimingPlanModule.Constants;
using RoadSafe.TimingPlanModule.DTOs;
using RoadSafe.TimingPlanModule.Repositories;
using Task = System.Threading.Tasks.Task;

namespace RoadSafe.TimingPlanModule.Services
{
	public class TimingPlanService : ITimingPlanService
	{
		private readonly ITimingPlanRepository _repo;
		private readonly ITrafficLogicService _trafficLogic;

		public TimingPlanService(ITimingPlanRepository repo, ITrafficLogicService trafficLogic)
		{
			_repo = repo;
			_trafficLogic = trafficLogic;
		}

		public async Task<IEnumerable<TimingPlanDto>> GetAllAsync()
			=> (await _repo.GetAllAsync()).Select(Map);

		public async Task<TimingPlanDto> GetByIdAsync(int id)
			=> Map(await _repo.GetByIdAsync(id));

		public async Task<IEnumerable<TimingPlanDto>> GetByControllerAsync(int controllerId)
			=> (await _repo.GetByControllerAsync(controllerId)).Select(Map);

		public async Task<IEnumerable<TimingPlanDiffDto>> ComparePlansAsync(int planIdA, int planIdB)
		{
			var planA = await _repo.GetByIdAsync(planIdA);
			var planB = await _repo.GetByIdAsync(planIdB);

			if (planA == null || planB == null) throw new InvalidOperationException("One or both plans not found.");

			return new List<TimingPlanDiffDto>
			{
				new TimingPlanDiffDto { FieldName = "Name", OldValue = planA.Name, NewValue = planB.Name, IsDifferent = planA.Name != planB.Name },
				new TimingPlanDiffDto { FieldName = "CycleTimeSec", OldValue = planA.CycleTimeSec?.ToString(), NewValue = planB.CycleTimeSec?.ToString(), IsDifferent = planA.CycleTimeSec != planB.CycleTimeSec },
				new TimingPlanDiffDto { FieldName = "PhasesJSON", OldValue = planA.PhasesJson, NewValue = planB.PhasesJson, IsDifferent = planA.PhasesJson != planB.PhasesJson },
				new TimingPlanDiffDto { FieldName = "OffsetsJSON", OldValue = planA.OffsetsJson, NewValue = planB.OffsetsJson, IsDifferent = planA.OffsetsJson != planB.OffsetsJson }
			}.Where(d => d.IsDifferent);
		}

		public async Task<TimingPlanDto> CreateAsync(CreateTimingPlanDto dto)
		{
			_trafficLogic.ValidatePhases(dto);

			var entity = new TimingPlan
			{
				ControllerId = dto.ControllerID,
				Name = dto.Name,
				CycleTimeSec = dto.CycleTimeSec,
				PhasesJson = dto.PhasesJSON,
				OffsetsJson = dto.OffsetsJSON,
				CreatedAt = DateTime.UtcNow,
				Status = TimingPlanStatus.Draft
			};

			await _repo.AddAsync(entity);
			await _repo.SaveChangesAsync();
			return Map(entity);
		}

		public async Task SubmitForApprovalAsync(int planId)
		{
			var plan = await _repo.GetByIdAsync(planId);
			if (plan == null) throw new InvalidOperationException("Plan not found");

			plan.Status = TimingPlanStatus.PendingApproval;
			_repo.Update(plan);
			await _repo.SaveChangesAsync();
		}

		public async Task ApprovePlanAsync(int planId, string approvedBy)
		{
			var plan = await _repo.GetByIdAsync(planId);
			if (plan.Status != TimingPlanStatus.PendingApproval)
				throw new InvalidOperationException("Plan must be in PendingApproval state to be approved.");

			plan.Status = TimingPlanStatus.Approved;
			_repo.Update(plan);
			await _repo.SaveChangesAsync();
		}

		public async Task ApplyPlanAsync(int planId, string appliedBy, string reason)
		{
			var plan = await _repo.GetByIdAsync(planId);
			if (plan.Status != TimingPlanStatus.Approved)
				throw new InvalidOperationException("Only approved plans can be applied.");

			await _repo.DeactivateExistingPlansAsync(plan.ControllerId ?? 0);

			plan.Status = TimingPlanStatus.Active;
			plan.EffectiveFrom = DateTime.UtcNow;
			_repo.Update(plan);

			await _repo.AddTimingChangeAsync(new TimingChange
			{
				ControllerId = plan.ControllerId,
				PlanId = plan.PlanId,
				AppliedBy = int.TryParse(appliedBy, out int userId) ? userId : null,
				AppliedAt = DateTime.UtcNow,
				Reason = reason,
				Status = "Applied"
			});

			await _repo.SaveChangesAsync();
		}

		public async Task RollbackPlanAsync(RollbackDto dto)
		{
			var plan = await _repo.GetByIdAsync(dto.RollbackPlanID);

			await _repo.DeactivateExistingPlansAsync(plan.ControllerId ?? 0);

			plan.Status = TimingPlanStatus.Active;
			_repo.Update(plan);

			await _repo.AddTimingChangeAsync(new TimingChange
			{
				ControllerId = plan.ControllerId,
				PlanId = plan.PlanId,
				AppliedBy = int.TryParse(dto.AppliedBy, out int userId) ? userId : null,
				AppliedAt = DateTime.UtcNow,
				Reason = dto.Reason,
				RollbackPlanId = dto.RollbackPlanID,
				Status = "Rollback"
			});

			await _repo.SaveChangesAsync();
		}

		public async Task ArchivePlanAsync(int planId, string fileUri)
		{
			var plan = await _repo.GetByIdAsync(planId);

			plan.Status = TimingPlanStatus.Archived;
			_repo.Update(plan);

			await _repo.ArchivePlanAsync(new TimingArchive
			{
				ControllerId = plan.ControllerId,
				PlanId = plan.PlanId,
				ArchivedAt = DateTime.UtcNow,
				ArchiveUri = fileUri
			});

			await _repo.SaveChangesAsync();
		}

		public async Task<TimingPlanImportResultDto> ImportFromExcelAsync(Stream excelStream, string uploadedBy)
		{
			var result = new TimingPlanImportResultDto();

			using var workbook = new XLWorkbook(excelStream);
			var worksheet = workbook.Worksheet(1);
			var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

			foreach (var row in rows)
			{
				result.TotalProcessed++;

				try
				{
					string excelStatus = row.Cell(6).GetValue<string>();
					if (!TimingPlanStatus.IsValid(excelStatus))
					{
						throw new ArgumentException($"Invalid status '{excelStatus}'. Valid options are: Draft, PendingApproval, Approved, Active, Archived.");
					}

					var dto = new CreateTimingPlanDto
					{
						ControllerID = row.Cell(1).GetValue<int>(),
						Name = row.Cell(2).GetValue<string>(),
						CycleTimeSec = row.Cell(3).GetValue<int>(),
						PhasesJSON = row.Cell(4).GetValue<string>(),
						OffsetsJSON = row.Cell(5).GetValue<string>()
					};

					_trafficLogic.ValidatePhases(dto);

					var entity = new TimingPlan
					{
						ControllerId = dto.ControllerID,
						Name = dto.Name,
						CycleTimeSec = dto.CycleTimeSec,
						PhasesJson = dto.PhasesJSON,
						OffsetsJson = dto.OffsetsJSON,
						CreatedAt = DateTime.UtcNow,
						Status = excelStatus
					};

					await _repo.AddAsync(entity);
					result.SuccessCount++;
				}
				catch (Exception ex)
				{
					result.FailureCount++;
					result.Errors.Add($"Row {row.RowNumber()}: {ex.Message}");
				}
			}

			if (result.SuccessCount > 0)
			{
				await _repo.SaveChangesAsync();
			}

			return result;
		}

		private TimingPlanDto Map(TimingPlan p) => p == null ? null : new TimingPlanDto
		{
			PlanID = p.PlanId,
			ControllerID = p.ControllerId ?? 0,
			Name = p.Name,
			CycleTimeSec = p.CycleTimeSec ?? 0,
			PhasesJSON = p.PhasesJson,
			OffsetsJSON = p.OffsetsJson,
			EffectiveFrom = p.EffectiveFrom ?? DateTime.MinValue,
			EffectiveTo = p.EffectiveTo,
			Status = p.Status
		};
	}
}