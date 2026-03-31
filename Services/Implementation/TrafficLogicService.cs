using System;
using System.Text.Json;
using RoadSafe.TimingPlanModule.DTOs;

namespace RoadSafe.TimingPlanModule.Services
{
	public class TrafficLogicService : ITrafficLogicService
	{
		public void ValidatePhases(CreateTimingPlanDto dto)
		{
			try
			{
				using var phasesDoc = JsonDocument.Parse(dto.PhasesJSON);
				int totalPhaseTime = 0;

				foreach (var phase in phasesDoc.RootElement.EnumerateArray())
				{
					if (phase.TryGetProperty("durationSec", out var durationElement))
					{
						totalPhaseTime += durationElement.GetInt32();
					}
				}

				if (totalPhaseTime > dto.CycleTimeSec)
				{
					throw new ArgumentException($"Total phase duration ({totalPhaseTime}s) exceeds Cycle Time ({dto.CycleTimeSec}s).");
				}
			}
			catch (JsonException)
			{
				throw new ArgumentException("PhasesJSON is not valid JSON.");
			}
		}
	}
}