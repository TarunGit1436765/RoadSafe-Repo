using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RoadSafe.API.Models;
using RoadSafe.TimingPlanModule.Constants;
using Task = System.Threading.Tasks.Task;

namespace RoadSafe.TimingPlanModule.Repositories
{
	public class TimingPlanRepository : ITimingPlanRepository
	{
		private readonly RoadSafeDbContext _context;

		public TimingPlanRepository(RoadSafeDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TimingPlan>> GetAllAsync()
			=> await _context.TimingPlans.ToListAsync();

		public async Task<TimingPlan> GetByIdAsync(int id)
			=> await _context.TimingPlans.FindAsync(id);

		public async Task<IEnumerable<TimingPlan>> GetByControllerAsync(int controllerId)
			=> await _context.TimingPlans.Where(p => p.ControllerId == controllerId).ToListAsync();

		public async Task AddAsync(TimingPlan plan) => await _context.TimingPlans.AddAsync(plan);

		public void Update(TimingPlan plan) => _context.TimingPlans.Update(plan);

		public void Delete(TimingPlan plan) => _context.TimingPlans.Remove(plan);

		public async Task AddTimingChangeAsync(TimingChange change) => await _context.TimingChanges.AddAsync(change);

		public async Task ArchivePlanAsync(TimingArchive archive) => await _context.TimingArchives.AddAsync(archive);

		public async Task DeactivateExistingPlansAsync(int controllerId)
		{
			var activePlans = await _context.TimingPlans
				.Where(p => p.ControllerId == controllerId && p.Status == TimingPlanStatus.Active)
				.ToListAsync();

			foreach (var plan in activePlans)
				plan.Status = TimingPlanStatus.Archived;
		}

		public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
	}
}