using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class PurchaseOrder
{
    public int Poid { get; set; }

    public int? SupplierId { get; set; }

    public string? ItemsJson { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<GoodsReceipt> GoodsReceipts { get; set; } = new List<GoodsReceipt>();

    public virtual Supplier? Supplier { get; set; }
}
