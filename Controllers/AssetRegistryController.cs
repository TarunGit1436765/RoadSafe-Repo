using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using RoadSafe.API.Models;
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

        [HttpGet("intersections")]
        public async Task<ActionResult<IEnumerable<Intersection>>> GetIntersections() 
        {
            var result = await _assetRegistryService.GetIntersectionsAsync();
            return Ok(result);
        }

        [HttpPost("intersections/bulk")]
        public async Task<ActionResult<IEnumerable<Intersection>>> BulkCreateIntersections(IEnumerable<Intersection> intersections) 
        {
            var created = await _assetRegistryService.BulkCreateIntersectionsAsync(intersections);
            return CreatedAtAction(nameof(GetIntersections), created);
        }

        [HttpGet("controllers")]
        public async Task<ActionResult<IEnumerable<Models.Controller>>> GetControllers() 
        {
            var result = await _assetRegistryService.GetControllersAsync();
            return Ok(result);
        }

        [HttpPost("controllers/bulk")]
        public async Task<ActionResult<IEnumerable<Models.Controller>>> BulkCreateControllers(IEnumerable<Models.Controller> controllers) 
        {
            var created = await _assetRegistryService.BulkCreateControllersAsync(controllers);
            return CreatedAtAction(nameof(GetControllers), created);
        }

        [HttpGet("detectors")]
        public async Task<ActionResult<IEnumerable<Detector>>> GetDetectors() 
        {
            var result = await _assetRegistryService.GetDetectorsAsync();
            return Ok(result);
        }

        [HttpPost("detectors/bulk")]
        public async Task<ActionResult<IEnumerable<Detector>>> BulkCreateDetectors(IEnumerable<Detector> detectors) 
        {
            var created = await _assetRegistryService.BulkCreateDetectorsAsync(detectors);
            return CreatedAtAction(nameof(GetDetectors), created);
        }

        [HttpGet("signalheads")]
        public async Task<ActionResult<IEnumerable<SignalHead>>> GetSignalHeads() 
        {
            var result = await _assetRegistryService.GetSignalHeadsAsync();
            return Ok(result);
        }

        [HttpPost("signalheads/bulk")]
        public async Task<ActionResult<IEnumerable<SignalHead>>> BulkCreateSignalHeads(IEnumerable<SignalHead> signalHeads) 
        {
            var created = await _assetRegistryService.BulkCreateSignalHeadsAsync(signalHeads);
            return CreatedAtAction(nameof(GetSignalHeads), created);
        }

        [HttpPost("documents/upload")]
        public async Task<IActionResult> UploadDocument([FromForm] AssetDocumentUploadDto request)
        {
            try
            {
                var newDocument = await _assetRegistryService.UploadDocumentAsync(request);
                
                return Ok(new { 
                    message = "File uploaded successfully!", 
                    documentId = newDocument.DocumentId, // Assuming DocumentId is the primary key
                    fileUri = newDocument.FileUri 
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception )
            {
                // In production, log the exception here
                return StatusCode(500, "An error occurred while uploading the file.");
            }
        }
    }
}