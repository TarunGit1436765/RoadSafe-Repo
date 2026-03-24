using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class IncidentNote
{
    public int NoteId { get; set; }

    public int? IncidentId { get; set; }

    public int? AuthorId { get; set; }

    public string? NoteText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? Author { get; set; }

    public virtual Incident? Incident { get; set; }
}
