using System.Collections.Generic;
using System.Threading.Tasks;
using RoadSafe.API.Models;

namespace RoadSafe.API.Repositories.Interfaces
{
	public interface IAssetRegistryRepository
	{
		Task<IEnumerable<Intersection>> GetIntersectionsAsync();
		Task<Intersection?> GetIntersectionByIdAsync(int id);
		Task<IEnumerable<Intersection>> BulkCreateIntersectionsAsync(IEnumerable<Intersection> intersections);
		Task<Intersection> UpdateIntersectionAsync(Intersection intersection);

		Task<IEnumerable<Models.Controller>> GetControllersAsync();
		Task<Models.Controller?> GetControllerByIdAsync(int id);
		Task<IEnumerable<Models.Controller>> BulkCreateControllersAsync(IEnumerable<Models.Controller> controllers);
		Task<Models.Controller> UpdateControllerAsync(Models.Controller controller);

		Task<IEnumerable<Detector>> GetDetectorsAsync();
		Task<Detector?> GetDetectorByIdAsync(int id);
		Task<IEnumerable<Detector>> BulkCreateDetectorsAsync(IEnumerable<Detector> detectors);
		Task<Detector> UpdateDetectorAsync(Detector detector);

		Task<IEnumerable<SignalHead>> GetSignalHeadsAsync();
		Task<SignalHead?> GetSignalHeadByIdAsync(int id);
		Task<IEnumerable<SignalHead>> BulkCreateSignalHeadsAsync(IEnumerable<SignalHead> signalHeads);
		Task<SignalHead> UpdateSignalHeadAsync(SignalHead signalHead);

		Task<AssetDocument> AddAssetDocumentAsync(AssetDocument document);
	}
}