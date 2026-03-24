using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Kpi
{
    public int Kpiid { get; set; }

    public string Name { get; set; } = null!;

    public string? Definition { get; set; }

    public decimal? Target { get; set; }

    public decimal? CurrentValue { get; set; }

    public string? ReportingPeriod { get; set; }
}
