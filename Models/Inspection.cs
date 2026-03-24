using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Inspection
{
    public int InspectionId { get; set; }

    public int AssetId { get; set; }

    public int? TemplateId { get; set; }

    public int? InspectorId { get; set; }

    public DateTime? PerformedAt { get; set; }

    public string? ConditionScoresJson { get; set; }

    public string? Findings { get; set; }

    public string? PhotoUrisJson { get; set; }

    public string? Status { get; set; }

    public virtual User? Inspector { get; set; }

    public virtual InspectionTemplate? Template { get; set; }
}
