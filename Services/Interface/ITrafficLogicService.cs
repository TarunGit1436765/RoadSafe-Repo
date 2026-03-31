using RoadSafe.TimingPlanModule.DTOs;

namespace RoadSafe.TimingPlanModule.Services
{
	public interface ITrafficLogicService
	{
		void ValidatePhases(CreateTimingPlanDto dto);
	}
}