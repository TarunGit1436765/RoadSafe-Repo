using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Intersection
{
    public int IntersectionId { get; set; }

    public string Name { get; set; } = null!;

    public string Coordinates { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<FaultReport> FaultReports { get; set; } = new List<FaultReport>();

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}
