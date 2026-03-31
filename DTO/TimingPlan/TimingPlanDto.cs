using System;

namespace RoadSafe.TimingPlanModule.DTOs
{
	public class TimingPlanDto
	{
		public int PlanID { get; set; }
		public int ControllerID { get; set; }
		public string Name { get; set; }
		public int CycleTimeSec { get; set; }
		public string PhasesJSON { get; set; }
		public string OffsetsJSON { get; set; }
		public DateTime EffectiveFrom { get; set; }
		public DateTime? EffectiveTo { get; set; }
		public string Status { get; set; }
	}

	public class CreateTimingPlanDto
	{
		public int ControllerID { get; set; }
		public string Name { get; set; }
		public int CycleTimeSec { get; set; }
		public string PhasesJSON { get; set; }
		public string OffsetsJSON { get; set; }
	}

	public class TimingChangeDto
	{
		public int ChangeID { get; set; }
		public int ControllerID { get; set; }
		public int PlanID { get; set; }
		public string AppliedBy { get; set; }
		public DateTime AppliedAt { get; set; }
		public string Reason { get; set; }
		public int? RollbackPlanID { get; set; }
	}

	public class ApproveTimingPlanDto
	{
		public string ApprovedBy { get; set; }
	}

	public class RollbackDto
	{
		public int RollbackPlanID { get; set; }
		public string AppliedBy { get; set; }
		public string Reason { get; set; }
	}

	public class TimingPlanDiffDto
	{
		public string FieldName { get; set; }
		public string OldValue { get; set; }
		public string NewValue { get; set; }
		public bool IsDifferent { get; set; }
	}

	public class TimingPlanImportResultDto
	{
		public int TotalProcessed { get; set; }
		public int SuccessCount { get; set; }
		public int FailureCount { get; set; }
		public List<string> Errors { get; set; } = new List<string>();
	}
}