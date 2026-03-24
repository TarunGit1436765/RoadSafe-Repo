using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public int? AssignedTo { get; set; }

    public int? RelatedEntityId { get; set; }

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Status { get; set; }

    public virtual User? AssignedToNavigation { get; set; }
}
