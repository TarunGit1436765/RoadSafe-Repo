using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class WorkLog
{
    public int LogId { get; set; }

    public int? WorkOrderId { get; set; }

    public int? PerformedBy { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }

    public decimal? LaborHours { get; set; }

    public string? Notes { get; set; }

    public string? PhotoUrisJson { get; set; }

    public virtual User? PerformedByNavigation { get; set; }

    public virtual WorkOrder? WorkOrder { get; set; }
}
