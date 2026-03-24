using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class TimingPlan
{
    public int PlanId { get; set; }

    public int? ControllerId { get; set; }

    public string? Name { get; set; }

    public int? CycleTimeSec { get; set; }

    public string? PhasesJson { get; set; }

    public string? OffsetsJson { get; set; }

    public DateTime? EffectiveFrom { get; set; }

    public DateTime? EffectiveTo { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public virtual Controller? Controller { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<TimingArchive> TimingArchives { get; set; } = new List<TimingArchive>();

    public virtual ICollection<TimingChange> TimingChangePlans { get; set; } = new List<TimingChange>();

    public virtual ICollection<TimingChange> TimingChangeRollbackPlans { get; set; } = new List<TimingChange>();
}
