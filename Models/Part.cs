using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Part
{
    public int PartId { get; set; }

    public string PartNumber { get; set; } = null!;

    public string? Description { get; set; }

    public int? QuantityOnHand { get; set; }

    public int? ReorderLevel { get; set; }

    public string? LocationId { get; set; }

    public decimal? UnitCost { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<MaterialUsage> MaterialUsages { get; set; } = new List<MaterialUsage>();
}
