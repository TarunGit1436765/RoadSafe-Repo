using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class AssetDocument
{
    public int DocumentId { get; set; }

    public int AssetId { get; set; }

    public string DocType { get; set; } = null!;

    public string FileUri { get; set; } = null!;

    public DateTime UploadedAt { get; set; }

    public int? VerifiedBy { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual User? VerifiedByNavigation { get; set; }
}
