using Microsoft.AspNetCore.Mvc;
using RoadSafe.API.DTOs; // <-- This single line connects all your DTO folders!
using RoadSafe.API.Services.Interfaces;

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

        // ==========================================
        // INTERSECTIONS
        // ==========================================
        [HttpGet("intersections")]
        public async Task<ActionResult<IEnumerable<IntersectionResponseDto>>> GetIntersections() 
        {
            var result = await _assetRegistryService.GetIntersectionsAsync();
            return Ok(result);
        }

        [HttpPost("intersections/bulk")]
        public async Task<ActionResult<IEnumerable<IntersectionResponseDto>>> BulkCreateIntersections(IEnumerable<IntersectionCreateDto> intersections) 
        {
            var created = await _assetRegistryService.BulkCreateIntersectionsAsync(intersections);
            return CreatedAtAction(nameof(GetIntersections), created);
        }

        // ==========================================
        // CONTROLLERS
        // ==========================================
        [HttpGet("controllers")]
        public async Task<ActionResult<IEnumerable<ControllerResponseDto>>> GetControllers() 
        {
            var result = await _assetRegistryService.GetControllersAsync();
            return Ok(result);
        }

        [HttpPost("controllers/bulk")]
        public async Task<ActionResult<IEnumerable<ControllerResponseDto>>> BulkCreateControllers(IEnumerable<ControllerCreateDto> controllers) 
        {
            var created = await _assetRegistryService.BulkCreateControllersAsync(controllers);
            return CreatedAtAction(nameof(GetControllers), created);
        }

        // ==========================================
        // DETECTORS
        // ==========================================
        [HttpGet("detectors")]
        public async Task<ActionResult<IEnumerable<DetectorResponseDto>>> GetDetectors() 
        {
            var result = await _assetRegistryService.GetDetectorsAsync();
            return Ok(result);
        }

        [HttpPost("detectors/bulk")]
        public async Task<ActionResult<IEnumerable<DetectorResponseDto>>> BulkCreateDetectors(IEnumerable<DetectorCreateDto> detectors) 
        {
            var created = await _assetRegistryService.BulkCreateDetectorsAsync(detectors);
            return CreatedAtAction(nameof(GetDetectors), created);
        }

        // ==========================================
        // SIGNAL HEADS
        // ==========================================
        [HttpGet("signalheads")]
        public async Task<ActionResult<IEnumerable<SignalHeadResponseDto>>> GetSignalHeads() 
        {
            var result = await _assetRegistryService.GetSignalHeadsAsync();
            return Ok(result);
        }

        [HttpPost("signalheads/bulk")]
        public async Task<ActionResult<IEnumerable<SignalHeadResponseDto>>> BulkCreateSignalHeads(IEnumerable<SignalHeadCreateDto> signalHeads) 
        {
            var created = await _assetRegistryService.BulkCreateSignalHeadsAsync(signalHeads);
            return CreatedAtAction(nameof(GetSignalHeads), created);
        }

        // ==========================================
        // DOCUMENTS
        // ==========================================
        [HttpPost("documents/upload")]
        public async Task<IActionResult> UploadDocument([FromForm] AssetDocumentUploadDto request)
        {
            try
            {
                var newDocument = await _assetRegistryService.UploadDocumentAsync(request);
                
                return Ok(new { 
                    message = "File uploaded successfully!", 
                    documentId = newDocument.DocumentId, 
                    fileUri = newDocument.FileUri 
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                // In production, log the exception here
                return StatusCode(500, "An error occurred while uploading the file.");
            }
        }
    }
}