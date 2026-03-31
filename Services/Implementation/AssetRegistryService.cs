using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using RoadSafe.API.Models;
using RoadSafe.API.DTOs;
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
			return intersections.Select(item => new IntersectionResponseDto { IntersectionId = item.IntersectionId, Name = item.Name, Coordinates = item.Coordinates, Region = item.Region, Type = item.Type, Status = item.Status });
		}

		public async Task<IEnumerable<IntersectionResponseDto>> BulkCreateIntersectionsAsync(IEnumerable<IntersectionCreateDto> requestDtos)
		{
			var existingIntersections = await _repository.GetIntersectionsAsync();
			var modelsToSave = new List<Intersection>();

			foreach (var dto in requestDtos)
			{
				if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Coordinates))
					throw new ArgumentException("Validation Failed: Intersection Name and Coordinates are strictly required.");

				
				bool exists = existingIntersections.Any(i => i.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase) || i.Coordinates == dto.Coordinates);
				if (exists) throw new ArgumentException($"Validation Failed: Intersection '{dto.Name}' or Coordinates '{dto.Coordinates}' already exists.");

				modelsToSave.Add(new Intersection { Name = dto.Name, Coordinates = dto.Coordinates, Region = dto.Region, Type = dto.Type, Status = dto.Status });
			}

			var savedModels = await _repository.BulkCreateIntersectionsAsync(modelsToSave);
			return savedModels.Select(item => new IntersectionResponseDto { IntersectionId = item.IntersectionId, Name = item.Name, Coordinates = item.Coordinates, Region = item.Region, Type = item.Type, Status = item.Status });
		}

		public async Task<IntersectionResponseDto> UpdateIntersectionAsync(int id, IntersectionCreateDto dto)
		{
			var existing = await _repository.GetIntersectionByIdAsync(id);
			if (existing == null) throw new ArgumentException($"Intersection #{id} not found.");

			existing.Name = dto.Name; existing.Coordinates = dto.Coordinates; existing.Region = dto.Region; existing.Type = dto.Type; existing.Status = dto.Status;
			var updated = await _repository.UpdateIntersectionAsync(existing);
			return new IntersectionResponseDto { IntersectionId = updated.IntersectionId, Name = updated.Name, Coordinates = updated.Coordinates, Region = updated.Region, Type = updated.Type, Status = updated.Status };
		}

		
		public async Task<IEnumerable<ControllerResponseDto>> GetControllersAsync()
		{
			var controllers = await _repository.GetControllersAsync();
			return controllers.Select(item => new ControllerResponseDto { ControllerId = item.ControllerId, IntersectionId = item.IntersectionId, Model = item.Model, FirmwareVersion = item.FirmwareVersion, InstallDate = item.InstallDate, Status = item.Status });
		}

		public async Task<IEnumerable<ControllerResponseDto>> BulkCreateControllersAsync(IEnumerable<ControllerCreateDto> requestDtos)
		{
			var existingControllers = await _repository.GetControllersAsync();
			var validIntersections = await _repository.GetIntersectionsAsync();
			var validIntersectionIds = validIntersections.Select(i => i.IntersectionId).ToHashSet();

			var modelsToSave = new List<Models.Controller>();

			foreach (var dto in requestDtos)
			{
				if (!validIntersectionIds.Contains(dto.IntersectionId))
					throw new ArgumentException($"Validation Failed: Intersection ID {dto.IntersectionId} does not exist.");

				if (string.IsNullOrWhiteSpace(dto.Model))
					throw new ArgumentException("Validation Failed: Controller Model is required.");

				if (existingControllers.Any(c => c.IntersectionId == dto.IntersectionId))
					throw new ArgumentException($"Validation Failed: Intersection {dto.IntersectionId} already has a controller.");

				modelsToSave.Add(new Models.Controller { IntersectionId = dto.IntersectionId, Model = dto.Model, FirmwareVersion = dto.FirmwareVersion, InstallDate = dto.InstallDate ?? DateTime.UtcNow, Status = dto.Status });
			}

			var savedModels = await _repository.BulkCreateControllersAsync(modelsToSave);
			return savedModels.Select(item => new ControllerResponseDto { ControllerId = item.ControllerId, IntersectionId = item.IntersectionId, Model = item.Model, FirmwareVersion = item.FirmwareVersion, InstallDate = item.InstallDate, Status = item.Status });
		}

		public async Task<ControllerResponseDto> UpdateControllerAsync(int id, ControllerCreateDto dto)
		{
			var existing = await _repository.GetControllerByIdAsync(id);
			if (existing == null) throw new ArgumentException($"Controller #{id} not found.");

			existing.IntersectionId = dto.IntersectionId; existing.Model = dto.Model; existing.FirmwareVersion = dto.FirmwareVersion; existing.InstallDate = dto.InstallDate ?? existing.InstallDate; existing.Status = dto.Status;
			var updated = await _repository.UpdateControllerAsync(existing);
			return new ControllerResponseDto { ControllerId = updated.ControllerId, IntersectionId = updated.IntersectionId, Model = updated.Model, FirmwareVersion = updated.FirmwareVersion, InstallDate = updated.InstallDate, Status = updated.Status };
		}

		
		public async Task<IEnumerable<DetectorResponseDto>> GetDetectorsAsync()
		{
			var detectors = await _repository.GetDetectorsAsync();
			return detectors.Select(item => new DetectorResponseDto { DetectorId = item.DetectorId, ControllerId = item.ControllerId, Type = item.Type, LocationDesc = item.LocationDesc, Status = item.Status });
		}

		public async Task<IEnumerable<DetectorResponseDto>> BulkCreateDetectorsAsync(IEnumerable<DetectorCreateDto> requestDtos)
		{
			var existingDetectors = await _repository.GetDetectorsAsync();
			var validControllers = await _repository.GetControllersAsync();
			var validControllerIds = validControllers.Select(c => c.ControllerId).ToHashSet();

			var modelsToSave = new List<Detector>();

			foreach (var dto in requestDtos)
			{
				if (!validControllerIds.Contains(dto.ControllerId))
					throw new ArgumentException($"Validation Failed: Controller ID {dto.ControllerId} does not exist.");

				if (string.IsNullOrWhiteSpace(dto.LocationDesc))
					throw new ArgumentException("Validation Failed: Detector Location Description is required.");

				
				if (existingDetectors.Any(d => d.LocationDesc.Equals(dto.LocationDesc, StringComparison.OrdinalIgnoreCase)))
					throw new ArgumentException($"Validation Failed: A detector is already registered at '{dto.LocationDesc}'.");

				modelsToSave.Add(new Detector { ControllerId = dto.ControllerId, Type = dto.Type, LocationDesc = dto.LocationDesc, Status = dto.Status });
			}

			var savedModels = await _repository.BulkCreateDetectorsAsync(modelsToSave);
			return savedModels.Select(item => new DetectorResponseDto { DetectorId = item.DetectorId, ControllerId = item.ControllerId, Type = item.Type, LocationDesc = item.LocationDesc, Status = item.Status });
		}

		public async Task<DetectorResponseDto> UpdateDetectorAsync(int id, DetectorCreateDto dto)
		{
			var existing = await _repository.GetDetectorByIdAsync(id);
			if (existing == null) throw new ArgumentException($"Detector #{id} not found.");

			existing.ControllerId = dto.ControllerId; existing.Type = dto.Type; existing.LocationDesc = dto.LocationDesc; existing.Status = dto.Status;
			var updated = await _repository.UpdateDetectorAsync(existing);
			return new DetectorResponseDto { DetectorId = updated.DetectorId, ControllerId = updated.ControllerId, Type = updated.Type, LocationDesc = updated.LocationDesc, Status = updated.Status };
		}

		
		public async Task<IEnumerable<SignalHeadResponseDto>> GetSignalHeadsAsync()
		{
			var signalHeads = await _repository.GetSignalHeadsAsync();
			return signalHeads.Select(item => new SignalHeadResponseDto { SignalHeadId = item.SignalHeadId, ControllerId = item.ControllerId, Approach = item.Approach, LampType = item.LampType, Status = item.Status });
		}

		public async Task<IEnumerable<SignalHeadResponseDto>> BulkCreateSignalHeadsAsync(IEnumerable<SignalHeadCreateDto> requestDtos)
		{
			var existingSignals = await _repository.GetSignalHeadsAsync();
			var validControllers = await _repository.GetControllersAsync();
			var validControllerIds = validControllers.Select(c => c.ControllerId).ToHashSet();

			var modelsToSave = new List<SignalHead>();

			foreach (var dto in requestDtos)
			{
				if (!validControllerIds.Contains(dto.ControllerId))
					throw new ArgumentException($"Validation Failed: Controller ID {dto.ControllerId} does not exist.");

				if (string.IsNullOrWhiteSpace(dto.Approach))
					throw new ArgumentException("Validation Failed: Signal Head Approach is required.");

				
				bool alreadyExists = existingSignals.Any(s => s.ControllerId == dto.ControllerId && s.Approach.Equals(dto.Approach, StringComparison.OrdinalIgnoreCase));
				if (alreadyExists) throw new ArgumentException($"Validation Failed: Controller {dto.ControllerId} already has a signal head for the {dto.Approach} approach.");

				modelsToSave.Add(new SignalHead { ControllerId = dto.ControllerId, Approach = dto.Approach, LampType = dto.LampType, Status = dto.Status });
			}

			var savedModels = await _repository.BulkCreateSignalHeadsAsync(modelsToSave);
			return savedModels.Select(item => new SignalHeadResponseDto { SignalHeadId = item.SignalHeadId, ControllerId = item.ControllerId, Approach = item.Approach, LampType = item.LampType, Status = item.Status });
		}

		public async Task<SignalHeadResponseDto> UpdateSignalHeadAsync(int id, SignalHeadCreateDto dto)
		{
			var existing = await _repository.GetSignalHeadByIdAsync(id);
			if (existing == null) throw new ArgumentException($"Signal Head #{id} not found.");

			existing.ControllerId = dto.ControllerId; existing.Approach = dto.Approach; existing.LampType = dto.LampType; existing.Status = dto.Status;
			var updated = await _repository.UpdateSignalHeadAsync(existing);
			return new SignalHeadResponseDto { SignalHeadId = updated.SignalHeadId, ControllerId = updated.ControllerId, Approach = updated.Approach, LampType = updated.LampType, Status = updated.Status };
		}

		public async Task<AssetDocument> UploadDocumentAsync(AssetDocumentUploadDto request)
		{
			if (request.File == null || request.File.Length == 0) throw new ArgumentException("No file was uploaded.");
			var uploadsFolder = Path.Combine(_env.WebRootPath ?? _env.ContentRootPath, "uploads", "documents");
			if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
			var uniqueFileName = $"{request.AssetId}_{request.DocType}_{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
			var filePath = Path.Combine(uploadsFolder, uniqueFileName);
			using (var fileStream = new FileStream(filePath, FileMode.Create)) { await request.File.CopyToAsync(fileStream); }

			var newDocument = new AssetDocument { AssetId = request.AssetId, DocType = request.DocType ?? string.Empty, FileUri = $"/uploads/documents/{uniqueFileName}", UploadedAt = DateTime.UtcNow, Status = "Pending Verification" };
			return await _repository.AddAssetDocumentAsync(newDocument);
		}
	}
}