using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using RoadSafe.API.Models;
using RoadSafe.API.Repositories.Interfaces;

namespace RoadSafe.API.Repositories.Implementations
{
    public class AssetRegistryRepository : IAssetRegistryRepository
    {
        private readonly RoadSafeDbContext _context;

        public AssetRegistryRepository(RoadSafeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Intersection>> GetIntersectionsAsync() => await _context.Intersections.ToListAsync();
        
        public async Task<IEnumerable<Intersection>> BulkCreateIntersectionsAsync(IEnumerable<Intersection> intersections)
        {
            _context.Intersections.AddRange(intersections);
            await _context.SaveChangesAsync();
            return intersections;
        }

        public async Task<IEnumerable<Models.Controller>> GetControllersAsync() => await _context.Controllers.ToListAsync();
        
        public async Task<IEnumerable<Models.Controller>> BulkCreateControllersAsync(IEnumerable<Models.Controller> controllers)
        {
            _context.Controllers.AddRange(controllers);
            await _context.SaveChangesAsync();
            return controllers;
        }

        public async Task<IEnumerable<Detector>> GetDetectorsAsync() => await _context.Detectors.ToListAsync();
        
        public async Task<IEnumerable<Detector>> BulkCreateDetectorsAsync(IEnumerable<Detector> detectors)
        {
            _context.Detectors.AddRange(detectors);
            await _context.SaveChangesAsync();
            return detectors;
        }

        public async Task<IEnumerable<SignalHead>> GetSignalHeadsAsync() => await _context.SignalHeads.ToListAsync();
        
        public async Task<IEnumerable<SignalHead>> BulkCreateSignalHeadsAsync(IEnumerable<SignalHead> signalHeads)
        {
            _context.SignalHeads.AddRange(signalHeads);
            await _context.SaveChangesAsync();
            return signalHeads;
        }

        public async Task<AssetDocument> AddAssetDocumentAsync(AssetDocument document)
        {
            _context.AssetDocuments.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }
    }
}