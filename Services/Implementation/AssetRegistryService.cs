using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using RoadSafe.API.Models;
using RoadSafe.API.DTOs; // <-- This fixes the missing namespace errors!
using RoadSafe.API.Repositories.Interfaces;
using RoadSafe.API.Services.Interfaces;

namespace RoadSafe.API.Services.Implementations
{
    public class AssetRegistryService : IAssetRegistryService
    {
        private readonly IAssetRegistryRepository _repository;
        private readonly IWebHostEnvironment _env;

        public AssetRegistryService(IAssetRegistryRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<IEnumerable<IntersectionResponseDto>> GetIntersectionsAsync()
        {
            var intersections = await _repository.GetIntersectionsAsync();
            
            return intersections.Select(item => new IntersectionResponseDto
            {
                IntersectionId = item.IntersectionId, // Casing fixed!
                Name = item.Name,
                Coordinates = item.Coordinates,
                Region = item.Region,
                Type = item.Type,
                Status = item.Status
            });
        }

        public async Task<IEnumerable<IntersectionResponseDto>> BulkCreateIntersectionsAsync(IEnumerable<IntersectionCreateDto> requestDtos)
        {
            var modelsToSave = requestDtos.Select(dto => new Intersection
            {
                Name = dto.Name,
                Coordinates = dto.Coordinates,
                Region = dto.Region,
                Type = dto.Type,
                Status = "Active" 
            });

            var savedModels = await _repository.BulkCreateIntersectionsAsync(modelsToSave);

            return savedModels.Select(item => new IntersectionResponseDto
            {
                IntersectionId = item.IntersectionId, // Casing fixed!
                Name = item.Name,
                Coordinates = item.Coordinates,
                Region = item.Region,
                Type = item.Type,
                Status = item.Status
            });
        }

        public async Task<IEnumerable<ControllerResponseDto>> GetControllersAsync()
        {
            var controllers = await _repository.GetControllersAsync();
            return controllers.Select(item => new ControllerResponseDto
            {
                ControllerId = item.ControllerId, // Casing fixed!
                IntersectionId = item.IntersectionId, 
                Model = item.Model,
                FirmwareVersion = item.FirmwareVersion,
                InstallDate = item.InstallDate,
                Status = item.Status
            });
        }

        public async Task<IEnumerable<ControllerResponseDto>> BulkCreateControllersAsync(IEnumerable<ControllerCreateDto> requestDtos)
        {
            var modelsToSave = requestDtos.Select(dto => new Models.Controller
            {
                IntersectionId = dto.IntersectionId, 
                Model = dto.Model,
                FirmwareVersion = dto.FirmwareVersion,
                InstallDate = dto.InstallDate ?? DateTime.UtcNow,
                Status = "Active"
            });

            var savedModels = await _repository.BulkCreateControllersAsync(modelsToSave);

            return savedModels.Select(item => new ControllerResponseDto
            {
                ControllerId = item.ControllerId, 
                IntersectionId = item.IntersectionId, 
                Model = item.Model,
                FirmwareVersion = item.FirmwareVersion,
                InstallDate = item.InstallDate,
                Status = item.Status
            });
        }

        public async Task<IEnumerable<DetectorResponseDto>> GetDetectorsAsync()
        {
            var detectors = await _repository.GetDetectorsAsync();
            return detectors.Select(item => new DetectorResponseDto
            {
                DetectorId = item.DetectorId, 
                ControllerId = item.ControllerId, 
                Type = item.Type,
                LocationDesc = item.LocationDesc,
                Status = item.Status
            });
        }

        public async Task<IEnumerable<DetectorResponseDto>> BulkCreateDetectorsAsync(IEnumerable<DetectorCreateDto> requestDtos)
        {
            var modelsToSave = requestDtos.Select(dto => new Detector
            {
                ControllerId = dto.ControllerId, 
                Type = dto.Type,
                LocationDesc = dto.LocationDesc,
                Status = "Active"
            });

            var savedModels = await _repository.BulkCreateDetectorsAsync(modelsToSave);

            return savedModels.Select(item => new DetectorResponseDto
            {
                DetectorId = item.DetectorId, 
                ControllerId = item.ControllerId, 
                Type = item.Type,
                LocationDesc = item.LocationDesc,
                Status = item.Status
            });
        }

        public async Task<IEnumerable<SignalHeadResponseDto>> GetSignalHeadsAsync()
        {
            var signalHeads = await _repository.GetSignalHeadsAsync();
            return signalHeads.Select(item => new SignalHeadResponseDto
            {
                SignalHeadId = item.SignalHeadId, 
                ControllerId = item.ControllerId, 
                Approach = item.Approach,
                LampType = item.LampType,
                Status = item.Status
            });
        }

        public async Task<IEnumerable<SignalHeadResponseDto>> BulkCreateSignalHeadsAsync(IEnumerable<SignalHeadCreateDto> requestDtos)
        {
            var modelsToSave = requestDtos.Select(dto => new SignalHead
            {
                ControllerId = dto.ControllerId, 
                Approach = dto.Approach,
                LampType = dto.LampType,
                Status = "Active"
            });

            var savedModels = await _repository.BulkCreateSignalHeadsAsync(modelsToSave);

            return savedModels.Select(item => new SignalHeadResponseDto
            {
                SignalHeadId = item.SignalHeadId, 
                ControllerId = item.ControllerId, 
                Approach = item.Approach,
                LampType = item.LampType,
                Status = item.Status
            });
        }

        public async Task<AssetDocument> UploadDocumentAsync(AssetDocumentUploadDto request)
        {
            if (request.File == null || request.File.Length == 0)
                throw new ArgumentException("No file was uploaded.");

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? _env.ContentRootPath, "uploads", "documents");
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{request.AssetId}_{request.DocType}_{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(fileStream);
            }

            var newDocument = new AssetDocument
            {
                AssetId = request.AssetId,
                DocType = request.DocType ?? string.Empty,
                FileUri = $"/uploads/documents/{uniqueFileName}",
                UploadedAt = DateTime.UtcNow,
                Status = "Pending Verification"
            };

            return await _repository.AddAssetDocumentAsync(newDocument);
        }
    }
}