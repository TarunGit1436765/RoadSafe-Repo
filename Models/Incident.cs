using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Incident
{
    public int IncidentId { get; set; }

    public int? IntersectionId { get; set; }

    public int? ReportedBy { get; set; }

    public string? IncidentType { get; set; }

    public string? Description { get; set; }

    public DateTime? ReportedAt { get; set; }

    public int? ResponseAssignedTo { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<IncidentNote> IncidentNotes { get; set; } = new List<IncidentNote>();

    public virtual Intersection? Intersection { get; set; }

    public virtual User? ReportedByNavigation { get; set; }

    public virtual User? ResponseAssignedToNavigation { get; set; }
}
