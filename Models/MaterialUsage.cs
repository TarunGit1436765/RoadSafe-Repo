using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class MaterialUsage
{
    public int UsageId { get; set; }

    public int? WorkOrderId { get; set; }

    public int? PartId { get; set; }

    public int? QuantityUsed { get; set; }

    public decimal? UnitCost { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual Part? Part { get; set; }

    public virtual WorkOrder? WorkOrder { get; set; }
}
