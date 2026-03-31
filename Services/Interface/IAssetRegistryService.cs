using System.Collections.Generic;
using System.Threading.Tasks;
using RoadSafe.API.DTOs;
using RoadSafe.API.Models;

namespace RoadSafe.API.Services.Interfaces
{
	public interface IAssetRegistryService
	{
		Task<IEnumerable<IntersectionResponseDto>> GetIntersectionsAsync();
		Task<IEnumerable<IntersectionResponseDto>> BulkCreateIntersectionsAsync(IEnumerable<IntersectionCreateDto> requestDtos);
		Task<IntersectionResponseDto> UpdateIntersectionAsync(int id, IntersectionCreateDto dto);
		
		Task<IEnumerable<ControllerResponseDto>> GetControllersAsync();
		Task<IEnumerable<ControllerResponseDto>> BulkCreateControllersAsync(IEnumerable<ControllerCreateDto> requestDtos);
		Task<ControllerResponseDto> UpdateControllerAsync(int id, ControllerCreateDto dto);
		

		Task<IEnumerable<DetectorResponseDto>> GetDetectorsAsync();
		Task<IEnumerable<DetectorResponseDto>> BulkCreateDetectorsAsync(IEnumerable<DetectorCreateDto> requestDtos);
		Task<DetectorResponseDto> UpdateDetectorAsync(int id, DetectorCreateDto dto);
		

		Task<IEnumerable<SignalHeadResponseDto>> GetSignalHeadsAsync();
		Task<IEnumerable<SignalHeadResponseDto>> BulkCreateSignalHeadsAsync(IEnumerable<SignalHeadCreateDto> requestDtos);
		Task<SignalHeadResponseDto> UpdateSignalHeadAsync(int id, SignalHeadCreateDto dto);
		

		Task<AssetDocument> UploadDocumentAsync(AssetDocumentUploadDto request);
	}
}