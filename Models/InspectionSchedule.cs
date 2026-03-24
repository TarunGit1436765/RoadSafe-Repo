using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class InspectionSchedule
{
    public int ScheduleId { get; set; }

    public string? AssetFilterJson { get; set; }

    public int? TemplateId { get; set; }

    public int? FrequencyDays { get; set; }

    public DateOnly? NextDueDate { get; set; }

    public int? OwnerId { get; set; }

    public string? Status { get; set; }

    public virtual User? Owner { get; set; }

    public virtual InspectionTemplate? Template { get; set; }
}
