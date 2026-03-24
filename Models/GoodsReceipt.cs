using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class GoodsReceipt
{
    public int ReceiptId { get; set; }

    public int? Poid { get; set; }

    public int? ReceivedBy { get; set; }

    public DateTime? ReceivedAt { get; set; }

    public string? ItemsJson { get; set; }

    public string? InvoiceUri { get; set; }

    public string? Status { get; set; }

    public virtual PurchaseOrder? Po { get; set; }

    public virtual User? ReceivedByNavigation { get; set; }
}
