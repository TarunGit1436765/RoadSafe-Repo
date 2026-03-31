using System.Collections.Generic;
using System.IO;
using RoadSafe.TimingPlanModule.DTOs;
using Task = System.Threading.Tasks.Task;

namespace RoadSafe.TimingPlanModule.Services
{
	public interface ITimingPlanService
	{
		Task<IEnumerable<TimingPlanDto>> GetAllAsync();
		Task<TimingPlanDto> GetByIdAsync(int id);
		Task<IEnumerable<TimingPlanDto>> GetByControllerAsync(int controllerId);
		Task<IEnumerable<TimingPlanDiffDto>> ComparePlansAsync(int planIdA, int planIdB);

		Task<TimingPlanDto> CreateAsync(CreateTimingPlanDto dto);
		Task SubmitForApprovalAsync(int planId);
		Task ApprovePlanAsync(int planId, string approvedBy);
		Task ApplyPlanAsync(int planId, string appliedBy, string reason);
		Task RollbackPlanAsync(RollbackDto dto);
		Task ArchivePlanAsync(int planId, string fileUri);

		Task<TimingPlanImportResultDto> ImportFromExcelAsync(Stream excelStream, string uploadedBy);
	}
}