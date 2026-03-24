using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class Controller
{
    public int ControllerId { get; set; }

    public int IntersectionId { get; set; }

    public string Model { get; set; } = null!;

    public string FirmwareVersion { get; set; } = null!;

    public DateTime InstallDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<TimingArchive> TimingArchives { get; set; } = new List<TimingArchive>();

    public virtual ICollection<TimingChange> TimingChanges { get; set; } = new List<TimingChange>();

    public virtual ICollection<TimingPlan> TimingPlans { get; set; } = new List<TimingPlan>();
}
