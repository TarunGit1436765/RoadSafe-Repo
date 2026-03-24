using System;
using System.Collections.Generic;

namespace RoadSafe.API.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AssetDocument> AssetDocuments { get; set; } = new List<AssetDocument>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<FaultReport> FaultReportReportedByNavigations { get; set; } = new List<FaultReport>();

    public virtual ICollection<FaultReport> FaultReportValidatedByNavigations { get; set; } = new List<FaultReport>();

    public virtual ICollection<GoodsReceipt> GoodsReceipts { get; set; } = new List<GoodsReceipt>();

    public virtual ICollection<IncidentNote> IncidentNotes { get; set; } = new List<IncidentNote>();

    public virtual ICollection<Incident> IncidentReportedByNavigations { get; set; } = new List<Incident>();

    public virtual ICollection<Incident> IncidentResponseAssignedToNavigations { get; set; } = new List<Incident>();

    public virtual ICollection<InspectionSchedule> InspectionSchedules { get; set; } = new List<InspectionSchedule>();

    public virtual ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<TimingChange> TimingChanges { get; set; } = new List<TimingChange>();

    public virtual ICollection<TimingPlan> TimingPlans { get; set; } = new List<TimingPlan>();

    public virtual ICollection<ValidationNote> ValidationNotes { get; set; } = new List<ValidationNote>();

    public virtual ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
