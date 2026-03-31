using System.Collections.Generic;
using RoadSafe.API.Models;
using Task = System.Threading.Tasks.Task;

namespace RoadSafe.TimingPlanModule.Repositories
{
	public interface ITimingPlanRepository
	{
		Task<IEnumerable<TimingPlan>> GetAllAsync();
		Task<TimingPlan> GetByIdAsync(int id);
		Task<IEnumerable<TimingPlan>> GetByControllerAsync(int controllerId);

		Task AddAsync(TimingPlan plan);
		void Update(TimingPlan plan);
		void Delete(TimingPlan plan);

		Task AddTimingChangeAsync(TimingChange change);
		Task ArchivePlanAsync(TimingArchive archive);
		Task DeactivateExistingPlansAsync(int controllerId);

		Task<int> SaveChangesAsync();
	}
}