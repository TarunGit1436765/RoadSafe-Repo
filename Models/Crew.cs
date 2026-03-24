using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Crew
{
    public int CrewId { get; set; }

    public string Name { get; set; } = null!;

    public string? MembersJson { get; set; }

    public string? ContactInfo { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
