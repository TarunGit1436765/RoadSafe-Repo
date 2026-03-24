using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class InspectionTemplate
{
    public int TemplateId { get; set; }

    public string Name { get; set; } = null!;

    public string? AssetType { get; set; }

    public string ChecklistJson { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<InspectionSchedule> InspectionSchedules { get; set; } = new List<InspectionSchedule>();

    public virtual ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
}
