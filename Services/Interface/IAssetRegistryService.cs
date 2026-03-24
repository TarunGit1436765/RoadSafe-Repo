using System.Collections.Generic;
using System.Threading.Tasks;
using RoadSafe.API.Models;
using RoadSafe.API.DTOs; // <-- This fixes the CS0246 errors!

namespace RoadSafe.API.Services.Interfaces
{
    public interface IAssetRegistryService
    {
        Task<IEnumerable<IntersectionResponseDto>> GetIntersectionsAsync();
        Task<IEnumerable<IntersectionResponseDto>> BulkCreateIntersectionsAsync(IEnumerable<IntersectionCreateDto> requestDtos);

        Task<IEnumerable<ControllerResponseDto>> GetControllersAsync();
        Task<IEnumerable<ControllerResponseDto>> BulkCreateControllersAsync(IEnumerable<ControllerCreateDto> requestDtos);

        Task<IEnumerable<DetectorResponseDto>> GetDetectorsAsync();
        Task<IEnumerable<DetectorResponseDto>> BulkCreateDetectorsAsync(IEnumerable<DetectorCreateDto> requestDtos);

        Task<IEnumerable<SignalHeadResponseDto>> GetSignalHeadsAsync();
        Task<IEnumerable<SignalHeadResponseDto>> BulkCreateSignalHeadsAsync(IEnumerable<SignalHeadCreateDto> requestDtos);

        Task<AssetDocument> UploadDocumentAsync(AssetDocumentUploadDto request);
    }
}