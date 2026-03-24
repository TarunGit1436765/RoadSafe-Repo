using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class TimingArchive
{
    public int ArchiveId { get; set; }

    public int? ControllerId { get; set; }

    public int? PlanId { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public string? ArchiveUri { get; set; }

    public string? Notes { get; set; }

    public virtual Controller? Controller { get; set; }

    public virtual TimingPlan? Plan { get; set; }
}
