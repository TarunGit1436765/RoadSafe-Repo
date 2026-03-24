using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class FaultReport
{
    public int FaultId { get; set; }

    public int? ReportedBy { get; set; }

    public int? IntersectionId { get; set; }

    public string? Description { get; set; }

    public string? PhotoUri { get; set; }

    public DateTime? ReportedAt { get; set; }

    public int? ValidatedBy { get; set; }

    public DateTime? ValidatedAt { get; set; }

    public string? Status { get; set; }

    public virtual Intersection? Intersection { get; set; }

    public virtual User? ReportedByNavigation { get; set; }

    public virtual User? ValidatedByNavigation { get; set; }

    public virtual ICollection<ValidationNote> ValidationNotes { get; set; } = new List<ValidationNote>();
}
