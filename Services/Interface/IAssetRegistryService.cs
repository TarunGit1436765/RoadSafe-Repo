using System.Collections.Generic;
using System.Threading.Tasks;
using RoadSafe.API.Models;

namespace RoadSafe.API.Services.Interfaces
{
    public interface IAssetRegistryService
    {
        Task<IEnumerable<Intersection>> GetIntersectionsAsync();
        Task<IEnumerable<Intersection>> BulkCreateIntersectionsAsync(IEnumerable<Intersection> intersections);
        
        Task<IEnumerable<Models.Controller>> GetControllersAsync();
        Task<IEnumerable<Models.Controller>> BulkCreateControllersAsync(IEnumerable<Models.Controller> controllers);
        
        Task<IEnumerable<Detector>> GetDetectorsAsync();
        Task<IEnumerable<Detector>> BulkCreateDetectorsAsync(IEnumerable<Detector> detectors);
        
        Task<IEnumerable<SignalHead>> GetSignalHeadsAsync();
        Task<IEnumerable<SignalHead>> BulkCreateSignalHeadsAsync(IEnumerable<SignalHead> signalHeads);
        
        Task<AssetDocument> UploadDocumentAsync(AssetDocumentUploadDto request);
    }
}