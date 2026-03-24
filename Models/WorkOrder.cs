using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class WorkOrder
{
    public int WorkOrderId { get; set; }

    public string? SourceType { get; set; }

    public int? SourceId { get; set; }

    public int AssetId { get; set; }

    public string? Description { get; set; }

    public string? Priority { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? AssignedCrewId { get; set; }

    public DateTime? ScheduledStart { get; set; }

    public DateTime? ScheduledEnd { get; set; }

    public string? Status { get; set; }

    public virtual Crew? AssignedCrew { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<MaterialUsage> MaterialUsages { get; set; } = new List<MaterialUsage>();

    public virtual ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
}
