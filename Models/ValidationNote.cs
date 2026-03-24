using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class ValidationNote
{
    public int NoteId { get; set; }

    public int? FaultId { get; set; }

    public int? ValidatorId { get; set; }

    public string? NoteText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual FaultReport? Fault { get; set; }

    public virtual User? Validator { get; set; }
}
