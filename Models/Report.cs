using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public string? Scope { get; set; }

    public string? ParametersJson { get; set; }

    public string? MetricsJson { get; set; }

    public int? GeneratedBy { get; set; }

    public DateTime? GeneratedAt { get; set; }

    public string? ReportUri { get; set; }

    public virtual User? GeneratedByNavigation { get; set; }
}
