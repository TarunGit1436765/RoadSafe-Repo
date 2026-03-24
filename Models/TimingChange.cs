using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class TimingChange
{
    public int ChangeId { get; set; }

    public int? ControllerId { get; set; }

    public int? PlanId { get; set; }

    public int? AppliedBy { get; set; }

    public DateTime? AppliedAt { get; set; }

    public string? Reason { get; set; }

    public int? RollbackPlanId { get; set; }

    public string? Status { get; set; }

    public virtual User? AppliedByNavigation { get; set; }

    public virtual Controller? Controller { get; set; }

    public virtual TimingPlan? Plan { get; set; }

    public virtual TimingPlan? RollbackPlan { get; set; }
}
