using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Detector
{
    public int DetectorId { get; set; }

    public int ControllerId { get; set; }

    public string Type { get; set; } = null!;

    public string LocationDesc { get; set; } = null!;

    public string Status { get; set; } = null!;
}
