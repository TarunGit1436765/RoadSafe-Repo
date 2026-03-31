using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoadSafe.TimingPlanModule.DTOs;
using RoadSafe.TimingPlanModule.Services;
using Task = System.Threading.Tasks.Task;

namespace RoadSafe.TimingPlanModule.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TimingPlanController : ControllerBase
	{
		private readonly ITimingPlanService _service;

		public TimingPlanController(ITimingPlanService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

		[HttpGet("compare/{idA}/{idB}")]
		public async Task<IActionResult> Compare(int idA, int idB) => Ok(await _service.ComparePlansAsync(idA, idB));

		[HttpPost]
		public async Task<IActionResult> Create(CreateTimingPlanDto dto) => Ok(await _service.CreateAsync(dto));

		[HttpPost("{id}/submit")]
		public async Task<IActionResult> Submit(int id)
		{
			await _service.SubmitForApprovalAsync(id);
			return Ok();
		}

		[HttpPost("{id}/approve")]
		public async Task<IActionResult> Approve(int id, ApproveTimingPlanDto dto)
		{
			await _service.ApprovePlanAsync(id, dto.ApprovedBy);
			return Ok();
		}

		[HttpPost("{id}/apply")]
		public async Task<IActionResult> Apply(int id, string appliedBy, string reason)
		{
			await _service.ApplyPlanAsync(id, appliedBy, reason);
			return Ok();
		}

		[HttpPost("rollback")]
		public async Task<IActionResult> Rollback(RollbackDto dto)
		{
			await _service.RollbackPlanAsync(dto);
			return Ok();
		}

		[HttpPost("{id}/archive")]
		public async Task<IActionResult> Archive(int id, string fileUri)
		{
			await _service.ArchivePlanAsync(id, fileUri);
			return Ok();
		}

		[HttpPost("import")]
		public async Task<IActionResult> ImportExcel(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return BadRequest("No file uploaded.");

			if (!file.FileName.EndsWith(".xlsx"))
				return BadRequest("Only .xlsx Excel files are supported.");

			using var stream = file.OpenReadStream();
			var result = await _service.ImportFromExcelAsync(stream, "SystemAdmin");

			if (result.FailureCount > 0 && result.SuccessCount == 0)
				return BadRequest(result);

			return Ok(result);
		}
	}
}