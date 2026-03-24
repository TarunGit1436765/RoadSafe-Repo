using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class SignalHead
{
    public int SignalHeadId { get; set; }

    public int ControllerId { get; set; }

    public string Approach { get; set; } = null!;

    public string LampType { get; set; } = null!;

    public string Status { get; set; } = null!;
}
