using Microsoft.AspNetCore.Mvc;
using RoadSafe.API.DTOs;
using RoadSafe.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadSafe.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssetRegistryController : ControllerBase
	{
		private readonly IAssetRegistryService _assetRegistryService;

		public AssetRegistryController(IAssetRegistryService assetRegistryService)
		{
			_assetRegistryService = assetRegistryService;
		}

		[HttpGet("intersections")]
		public async Task<ActionResult<IEnumerable<IntersectionResponseDto>>> GetIntersections() => Ok(await _assetRegistryService.GetIntersectionsAsync());

		[HttpPost("intersections")]
		public async Task<ActionResult<IntersectionResponseDto>> CreateIntersection(IntersectionCreateDto intersection)
		{
			var createdList = await _assetRegistryService.BulkCreateIntersectionsAsync(new List<IntersectionCreateDto> { intersection });
			return CreatedAtAction(nameof(GetIntersections), createdList.FirstOrDefault());
		}

		[HttpPost("intersections/bulk")]
		public async Task<ActionResult<IEnumerable<IntersectionResponseDto>>> BulkCreateIntersections(IEnumerable<IntersectionCreateDto> intersections) => CreatedAtAction(nameof(GetIntersections), await _assetRegistryService.BulkCreateIntersectionsAsync(intersections));

		[HttpPut("intersections/{id}")]
		public async Task<ActionResult<IntersectionResponseDto>> UpdateIntersection(int id, IntersectionCreateDto intersection)
		{
			try { return Ok(await _assetRegistryService.UpdateIntersectionAsync(id, intersection)); }
			catch (ArgumentException ex) { return NotFound(new { message = ex.Message }); }
		}

		[HttpGet("controllers")]
		public async Task<ActionResult<IEnumerable<ControllerResponseDto>>> GetControllers() => Ok(await _assetRegistryService.GetControllersAsync());

		[HttpPost("controllers")]
		public async Task<ActionResult<ControllerResponseDto>> CreateController(ControllerCreateDto controller)
		{
			try { return CreatedAtAction(nameof(GetControllers), (await _assetRegistryService.BulkCreateControllersAsync(new List<ControllerCreateDto> { controller })).FirstOrDefault()); }
			catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
		}

		[HttpPost("controllers/bulk")]
		public async Task<ActionResult<IEnumerable<ControllerResponseDto>>> BulkCreateControllers(IEnumerable<ControllerCreateDto> controllers)
		{
			try { return CreatedAtAction(nameof(GetControllers), await _assetRegistryService.BulkCreateControllersAsync(controllers)); }
			catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
		}

		[HttpPut("controllers/{id}")]
		public async Task<ActionResult<ControllerResponseDto>> UpdateController(int id, ControllerCreateDto controller)
		{
			try { return Ok(await _assetRegistryService.UpdateControllerAsync(id, controller)); }
			catch (ArgumentException ex) { return NotFound(new { message = ex.Message }); }
		}

		
		[HttpGet("detectors")]
		public async Task<ActionResult<IEnumerable<DetectorResponseDto>>> GetDetectors() => Ok(await _assetRegistryService.GetDetectorsAsync());

		[HttpPost("detectors")]
		public async Task<ActionResult<DetectorResponseDto>> CreateDetector(DetectorCreateDto detector) => CreatedAtAction(nameof(GetDetectors), (await _assetRegistryService.BulkCreateDetectorsAsync(new List<DetectorCreateDto> { detector })).FirstOrDefault());

		[HttpPost("detectors/bulk")]
		public async Task<ActionResult<IEnumerable<DetectorResponseDto>>> BulkCreateDetectors(IEnumerable<DetectorCreateDto> detectors) => CreatedAtAction(nameof(GetDetectors), await _assetRegistryService.BulkCreateDetectorsAsync(detectors));

		[HttpPut("detectors/{id}")]
		public async Task<ActionResult<DetectorResponseDto>> UpdateDetector(int id, DetectorCreateDto detector)
		{
			try { return Ok(await _assetRegistryService.UpdateDetectorAsync(id, detector)); }
			catch (ArgumentException ex) { return NotFound(new { message = ex.Message }); }
		}

		[HttpGet("signalheads")]
		public async Task<ActionResult<IEnumerable<SignalHeadResponseDto>>> GetSignalHeads() => Ok(await _assetRegistryService.GetSignalHeadsAsync());

		[HttpPost("signalheads")]
		public async Task<ActionResult<SignalHeadResponseDto>> CreateSignalHead(SignalHeadCreateDto signalHead) => CreatedAtAction(nameof(GetSignalHeads), (await _assetRegistryService.BulkCreateSignalHeadsAsync(new List<SignalHeadCreateDto> { signalHead })).FirstOrDefault());

		[HttpPost("signalheads/bulk")]
		public async Task<ActionResult<IEnumerable<SignalHeadResponseDto>>> BulkCreateSignalHeads(IEnumerable<SignalHeadCreateDto> signalHeads) => CreatedAtAction(nameof(GetSignalHeads), await _assetRegistryService.BulkCreateSignalHeadsAsync(signalHeads));

		[HttpPut("signalheads/{id}")]
		public async Task<ActionResult<SignalHeadResponseDto>> UpdateSignalHead(int id, SignalHeadCreateDto signalHead)
		{
			try { return Ok(await _assetRegistryService.UpdateSignalHeadAsync(id, signalHead)); }
			catch (ArgumentException ex) { return NotFound(new { message = ex.Message }); }
		}

		[HttpPost("documents/upload")]
		public async Task<IActionResult> UploadDocument([FromForm] AssetDocumentUploadDto request)
		{
			try { return Ok(await _assetRegistryService.UploadDocumentAsync(request)); }
			catch (Exception) { return StatusCode(500, "Error uploading file."); }
		}
	}
}