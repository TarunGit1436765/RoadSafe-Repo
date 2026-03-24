using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using RoadSafe.API.Models;
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

        // Pass-through methods for basic CRUD
        public async Task<IEnumerable<Intersection>> GetIntersectionsAsync() => await _repository.GetIntersectionsAsync();
        public async Task<IEnumerable<Intersection>> BulkCreateIntersectionsAsync(IEnumerable<Intersection> intersections) => await _repository.BulkCreateIntersectionsAsync(intersections);
        
        public async Task<IEnumerable<Models.Controller>> GetControllersAsync() => await _repository.GetControllersAsync();
        public async Task<IEnumerable<Models.Controller>> BulkCreateControllersAsync(IEnumerable<Models.Controller> controllers) => await _repository.BulkCreateControllersAsync(controllers);
        
        public async Task<IEnumerable<Detector>> GetDetectorsAsync() => await _repository.GetDetectorsAsync();
        public async Task<IEnumerable<Detector>> BulkCreateDetectorsAsync(IEnumerable<Detector> detectors) => await _repository.BulkCreateDetectorsAsync(detectors);
        
        public async Task<IEnumerable<SignalHead>> GetSignalHeadsAsync() => await _repository.GetSignalHeadsAsync();
        public async Task<IEnumerable<SignalHead>> BulkCreateSignalHeadsAsync(IEnumerable<SignalHead> signalHeads) => await _repository.BulkCreateSignalHeadsAsync(signalHeads);

        // Business logic for file upload
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