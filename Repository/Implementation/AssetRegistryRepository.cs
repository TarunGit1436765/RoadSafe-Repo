using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoadSafe.API.Models;
using RoadSafe.API.Repositories.Interfaces;

#pragma warning disable CS8603

namespace RoadSafe.API.Repositories.Implementations
{
	public class AssetRegistryRepository : IAssetRegistryRepository
	{
		private readonly RoadSafeDbContext _context;

		public AssetRegistryRepository(RoadSafeDbContext context)
		{
			_context = context;
		}

		//  INTERSECTIONS 
		public async Task<IEnumerable<Intersection>> GetIntersectionsAsync() => await _context.Intersections.ToListAsync();
		public async Task<Intersection?> GetIntersectionByIdAsync(int id) => await _context.Intersections.FindAsync(id);

		public async Task<IEnumerable<Intersection>> BulkCreateIntersectionsAsync(IEnumerable<Intersection> intersections)
		{
			_context.Intersections.AddRange(intersections);
			await _context.SaveChangesAsync();
			return intersections;
		}

		public async Task<Intersection> UpdateIntersectionAsync(Intersection intersection)
		{
			_context.Intersections.Update(intersection);
			await _context.SaveChangesAsync();
			return intersection;
		}

		//  CONTROLLERS 
		public async Task<IEnumerable<Models.Controller>> GetControllersAsync() => await _context.Controllers.ToListAsync();
		public async Task<Models.Controller?> GetControllerByIdAsync(int id) => await _context.Controllers.FindAsync(id);

		public async Task<IEnumerable<Models.Controller>> BulkCreateControllersAsync(IEnumerable<Models.Controller> controllers)
		{
			_context.Controllers.AddRange(controllers);
			await _context.SaveChangesAsync();
			return controllers;
		}

		public async Task<Models.Controller> UpdateControllerAsync(Models.Controller controller)
		{
			_context.Controllers.Update(controller);
			await _context.SaveChangesAsync();
			return controller;
		}

		//  DETECTORS
		public async Task<IEnumerable<Detector>> GetDetectorsAsync() => await _context.Detectors.ToListAsync();
		public async Task<Detector?> GetDetectorByIdAsync(int id) => await _context.Detectors.FindAsync(id);

		public async Task<IEnumerable<Detector>> BulkCreateDetectorsAsync(IEnumerable<Detector> detectors)
		{
			_context.Detectors.AddRange(detectors);
			await _context.SaveChangesAsync();
			return detectors;
		}

		public async Task<Detector> UpdateDetectorAsync(Detector detector)
		{
			_context.Detectors.Update(detector);
			await _context.SaveChangesAsync();
			return detector;
		}

		// SIGNAL HEADS 
		public async Task<IEnumerable<SignalHead>> GetSignalHeadsAsync() => await _context.SignalHeads.ToListAsync();
		public async Task<SignalHead?> GetSignalHeadByIdAsync(int id) => await _context.SignalHeads.FindAsync(id);

		public async Task<IEnumerable<SignalHead>> BulkCreateSignalHeadsAsync(IEnumerable<SignalHead> signalHeads)
		{
			_context.SignalHeads.AddRange(signalHeads);
			await _context.SaveChangesAsync();
			return signalHeads;
		}

		public async Task<SignalHead> UpdateSignalHeadAsync(SignalHead signalHead)
		{
			_context.SignalHeads.Update(signalHead);
			await _context.SaveChangesAsync();
			return signalHead;
		}

		public async Task<AssetDocument> AddAssetDocumentAsync(AssetDocument document)
		{
			_context.AssetDocuments.Add(document);
			await _context.SaveChangesAsync();
			return document;
		}
	}
}