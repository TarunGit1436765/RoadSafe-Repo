using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RoadSafe.API.Models;

public partial class RoadSafeDbContext : DbContext
{
    public RoadSafeDbContext()
    {
    }

    public RoadSafeDbContext(DbContextOptions<RoadSafeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssetDocument> AssetDocuments { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Controller> Controllers { get; set; }

    public virtual DbSet<Crew> Crews { get; set; }

    public virtual DbSet<Detector> Detectors { get; set; }

    public virtual DbSet<FaultReport> FaultReports { get; set; }

    public virtual DbSet<GoodsReceipt> GoodsReceipts { get; set; }

    public virtual DbSet<Incident> Incidents { get; set; }

    public virtual DbSet<IncidentNote> IncidentNotes { get; set; }

    public virtual DbSet<Inspection> Inspections { get; set; }

    public virtual DbSet<InspectionSchedule> InspectionSchedules { get; set; }

    public virtual DbSet<InspectionTemplate> InspectionTemplates { get; set; }

    public virtual DbSet<Intersection> Intersections { get; set; }

    public virtual DbSet<Kpi> Kpis { get; set; }

    public virtual DbSet<MaterialUsage> MaterialUsages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<SignalHead> SignalHeads { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TimingArchive> TimingArchives { get; set; }

    public virtual DbSet<TimingChange> TimingChanges { get; set; }

    public virtual DbSet<TimingPlan> TimingPlans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ValidationNote> ValidationNotes { get; set; }

    public virtual DbSet<WorkLog> WorkLogs { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=LTIN691146\\SQLEXPRESS;Database=RoadSafeDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId);

            entity.ToTable("AssetDocument");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.FileUri).HasColumnName("FileURI");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.AssetDocuments)
                .HasForeignKey(d => d.VerifiedBy)
                .HasConstraintName("FK_AssetDoc_User");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__AuditLog__A17F23B89BC2A39C");

            entity.ToTable("AuditLog");

            entity.Property(e => e.AuditId).HasColumnName("AuditID");
            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.ResourceType).HasMaxLength(50);
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__AuditLog__UserID__08B54D69");
        });

        modelBuilder.Entity<Controller>(entity =>
        {
            entity.ToTable("Controller");

            entity.Property(e => e.ControllerId).HasColumnName("ControllerID");
            entity.Property(e => e.IntersectionId).HasColumnName("IntersectionID");
        });

        modelBuilder.Entity<Crew>(entity =>
        {
            entity.HasKey(e => e.CrewId).HasName("PK__Crew__89BCFC0994E73CE8");

            entity.ToTable("Crew");

            entity.Property(e => e.CrewId).HasColumnName("CrewID");
            entity.Property(e => e.ContactInfo).HasMaxLength(100);
            entity.Property(e => e.MembersJson).HasColumnName("MembersJSON");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
        });

        modelBuilder.Entity<Detector>(entity =>
        {
            entity.ToTable("Detector");

            entity.Property(e => e.DetectorId).HasColumnName("DetectorID");
            entity.Property(e => e.ControllerId).HasColumnName("ControllerID");
        });

        modelBuilder.Entity<FaultReport>(entity =>
        {
            entity.HasKey(e => e.FaultId).HasName("PK__FaultRep__2C2940D371E20CEA");

            entity.ToTable("FaultReport");

            entity.Property(e => e.FaultId).HasColumnName("FaultID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IntersectionId).HasColumnName("IntersectionID");
            entity.Property(e => e.PhotoUri)
                .HasMaxLength(255)
                .HasColumnName("PhotoURI");
            entity.Property(e => e.ReportedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Reported");
            entity.Property(e => e.ValidatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Intersection).WithMany(p => p.FaultReports)
                .HasForeignKey(d => d.IntersectionId)
                .HasConstraintName("FK__FaultRepo__Inter__0D7A0286");

            entity.HasOne(d => d.ReportedByNavigation).WithMany(p => p.FaultReportReportedByNavigations)
                .HasForeignKey(d => d.ReportedBy)
                .HasConstraintName("FK__FaultRepo__Repor__0C85DE4D");

            entity.HasOne(d => d.ValidatedByNavigation).WithMany(p => p.FaultReportValidatedByNavigations)
                .HasForeignKey(d => d.ValidatedBy)
                .HasConstraintName("FK__FaultRepo__Valid__0F624AF8");
        });

        modelBuilder.Entity<GoodsReceipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PK__GoodsRec__CC08C4006E1B610F");

            entity.ToTable("GoodsReceipt");

            entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");
            entity.Property(e => e.InvoiceUri)
                .HasMaxLength(255)
                .HasColumnName("InvoiceURI");
            entity.Property(e => e.ItemsJson).HasColumnName("ItemsJSON");
            entity.Property(e => e.Poid).HasColumnName("POID");
            entity.Property(e => e.ReceivedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Received");

            entity.HasOne(d => d.Po).WithMany(p => p.GoodsReceipts)
                .HasForeignKey(d => d.Poid)
                .HasConstraintName("FK__GoodsRecei__POID__3493CFA7");

            entity.HasOne(d => d.ReceivedByNavigation).WithMany(p => p.GoodsReceipts)
                .HasForeignKey(d => d.ReceivedBy)
                .HasConstraintName("FK__GoodsRece__Recei__3587F3E0");
        });

        modelBuilder.Entity<Incident>(entity =>
        {
            entity.HasKey(e => e.IncidentId).HasName("PK__Incident__3D805392E6E1FF4A");

            entity.ToTable("Incident");

            entity.Property(e => e.IncidentId).HasColumnName("IncidentID");
            entity.Property(e => e.IncidentType).HasMaxLength(50);
            entity.Property(e => e.IntersectionId).HasColumnName("IntersectionID");
            entity.Property(e => e.ReportedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ResolvedAt).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Open");

            entity.HasOne(d => d.Intersection).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.IntersectionId)
                .HasConstraintName("FK__Incident__Inters__4C6B5938");

            entity.HasOne(d => d.ReportedByNavigation).WithMany(p => p.IncidentReportedByNavigations)
                .HasForeignKey(d => d.ReportedBy)
                .HasConstraintName("FK__Incident__Report__4D5F7D71");

            entity.HasOne(d => d.ResponseAssignedToNavigation).WithMany(p => p.IncidentResponseAssignedToNavigations)
                .HasForeignKey(d => d.ResponseAssignedTo)
                .HasConstraintName("FK__Incident__Respon__4F47C5E3");
        });

        modelBuilder.Entity<IncidentNote>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__Incident__EACE357F86006957");

            entity.ToTable("IncidentNote");

            entity.Property(e => e.NoteId).HasColumnName("NoteID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IncidentId).HasColumnName("IncidentID");

            entity.HasOne(d => d.Author).WithMany(p => p.IncidentNotes)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__IncidentN__Autho__540C7B00");

            entity.HasOne(d => d.Incident).WithMany(p => p.IncidentNotes)
                .HasForeignKey(d => d.IncidentId)
                .HasConstraintName("FK__IncidentN__Incid__531856C7");
        });

        modelBuilder.Entity<Inspection>(entity =>
        {
            entity.HasKey(e => e.InspectionId).HasName("PK__Inspecti__30B2DC287A634C34");

            entity.ToTable("Inspection");

            entity.Property(e => e.InspectionId).HasColumnName("InspectionID");
            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.ConditionScoresJson).HasColumnName("ConditionScoresJSON");
            entity.Property(e => e.InspectorId).HasColumnName("InspectorID");
            entity.Property(e => e.PerformedAt).HasColumnType("datetime");
            entity.Property(e => e.PhotoUrisJson).HasColumnName("PhotoURIsJSON");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Scheduled");
            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

            entity.HasOne(d => d.Inspector).WithMany(p => p.Inspections)
                .HasForeignKey(d => d.InspectorId)
                .HasConstraintName("FK__Inspectio__Inspe__1DB06A4F");

            entity.HasOne(d => d.Template).WithMany(p => p.Inspections)
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("FK__Inspectio__Templ__1CBC4616");
        });

        modelBuilder.Entity<InspectionSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Inspecti__9C8A5B691651F265");

            entity.ToTable("InspectionSchedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.AssetFilterJson).HasColumnName("AssetFilterJSON");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

            entity.HasOne(d => d.Owner).WithMany(p => p.InspectionSchedules)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Inspectio__Owner__18EBB532");

            entity.HasOne(d => d.Template).WithMany(p => p.InspectionSchedules)
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("FK__Inspectio__Templ__17F790F9");
        });

        modelBuilder.Entity<InspectionTemplate>(entity =>
        {
            entity.HasKey(e => e.TemplateId).HasName("PK__Inspecti__F87ADD079272F51F");

            entity.ToTable("InspectionTemplate");

            entity.Property(e => e.TemplateId).HasColumnName("TemplateID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.AssetType).HasMaxLength(50);
            entity.Property(e => e.ChecklistJson).HasColumnName("ChecklistJSON");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Intersection>(entity =>
        {
            entity.ToTable("Intersection");

            entity.Property(e => e.IntersectionId).HasColumnName("IntersectionID");
        });

        modelBuilder.Entity<Kpi>(entity =>
        {
            entity.HasKey(e => e.Kpiid).HasName("PK__KPI__72E6928177F90547");

            entity.ToTable("KPI");

            entity.Property(e => e.Kpiid).HasColumnName("KPIID");
            entity.Property(e => e.CurrentValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Definition).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ReportingPeriod).HasMaxLength(50);
            entity.Property(e => e.Target).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<MaterialUsage>(entity =>
        {
            entity.HasKey(e => e.UsageId).HasName("PK__Material__29B197C08DBCF5F8");

            entity.ToTable("MaterialUsage");

            entity.Property(e => e.UsageId).HasColumnName("UsageID");
            entity.Property(e => e.PartId).HasColumnName("PartID");
            entity.Property(e => e.TotalCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

            entity.HasOne(d => d.Part).WithMany(p => p.MaterialUsages)
                .HasForeignKey(d => d.PartId)
                .HasConstraintName("FK__MaterialU__PartI__2BFE89A6");

            entity.HasOne(d => d.WorkOrder).WithMany(p => p.MaterialUsages)
                .HasForeignKey(d => d.WorkOrderId)
                .HasConstraintName("FK__MaterialU__WorkO__2B0A656D");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E3208FE9C54");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EntityId).HasColumnName("EntityID");
            entity.Property(e => e.Message).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Unread");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__5BAD9CC8");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.PartId).HasName("PK__Part__7C3F0D30A9F0F268");

            entity.ToTable("Part");

            entity.HasIndex(e => e.PartNumber, "UQ__Part__025D30D94555ADD7").IsUnique();

            entity.Property(e => e.PartId).HasColumnName("PartID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.LocationId)
                .HasMaxLength(50)
                .HasColumnName("LocationID");
            entity.Property(e => e.PartNumber).HasMaxLength(50);
            entity.Property(e => e.QuantityOnHand).HasDefaultValue(0);
            entity.Property(e => e.ReorderLevel).HasDefaultValue(5);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.UnitCost).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.Poid).HasName("PK__Purchase__5F02A2F48E72DA7B");

            entity.ToTable("PurchaseOrder");

            entity.Property(e => e.Poid).HasColumnName("POID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemsJson).HasColumnName("ItemsJSON");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Draft");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__PurchaseO__Creat__2FCF1A8A");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__PurchaseO__Suppl__2EDAF651");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Report__D5BD48E5FFC2ED30");

            entity.ToTable("Report");

            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.GeneratedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MetricsJson).HasColumnName("MetricsJSON");
            entity.Property(e => e.ParametersJson).HasColumnName("ParametersJSON");
            entity.Property(e => e.ReportUri)
                .HasMaxLength(255)
                .HasColumnName("ReportURI");
            entity.Property(e => e.Scope).HasMaxLength(50);

            entity.HasOne(d => d.GeneratedByNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.GeneratedBy)
                .HasConstraintName("FK__Report__Generate__57DD0BE4");
        });

        modelBuilder.Entity<SignalHead>(entity =>
        {
            entity.ToTable("SignalHead");

            entity.Property(e => e.SignalHeadId).HasColumnName("SignalHeadID");
            entity.Property(e => e.ControllerId).HasColumnName("ControllerID");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666948C0D4792");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.ContactInfo).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.TaxId)
                .HasMaxLength(50)
                .HasColumnName("TaxID");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Task__7C6949D104B7A75A");

            entity.ToTable("Task");

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.RelatedEntityId).HasColumnName("RelatedEntityID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__Task__AssignedTo__607251E5");
        });

        modelBuilder.Entity<TimingArchive>(entity =>
        {
            entity.HasKey(e => e.ArchiveId).HasName("PK__TimingAr__33A73E77CD901D57");

            entity.ToTable("TimingArchive");

            entity.Property(e => e.ArchiveId).HasColumnName("ArchiveID");
            entity.Property(e => e.ArchiveUri)
                .HasMaxLength(255)
                .HasColumnName("ArchiveURI");
            entity.Property(e => e.ArchivedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ControllerId).HasColumnName("ControllerID");
            entity.Property(e => e.PlanId).HasColumnName("PlanID");

            entity.HasOne(d => d.Controller).WithMany(p => p.TimingArchives)
                .HasForeignKey(d => d.ControllerId)
                .HasConstraintName("FK__TimingArc__Contr__47A6A41B");

            entity.HasOne(d => d.Plan).WithMany(p => p.TimingArchives)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__TimingArc__PlanI__489AC854");
        });

        modelBuilder.Entity<TimingChange>(entity =>
        {
            entity.HasKey(e => e.ChangeId).HasName("PK__TimingCh__0E05C5B7E916EC05");

            entity.ToTable("TimingChange");

            entity.Property(e => e.ChangeId).HasColumnName("ChangeID");
            entity.Property(e => e.AppliedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ControllerId).HasColumnName("ControllerID");
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.RollbackPlanId).HasColumnName("RollbackPlanID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Applied");

            entity.HasOne(d => d.AppliedByNavigation).WithMany(p => p.TimingChanges)
                .HasForeignKey(d => d.AppliedBy)
                .HasConstraintName("FK__TimingCha__Appli__41EDCAC5");

            entity.HasOne(d => d.Controller).WithMany(p => p.TimingChanges)
                .HasForeignKey(d => d.ControllerId)
                .HasConstraintName("FK__TimingCha__Contr__40058253");

            entity.HasOne(d => d.Plan).WithMany(p => p.TimingChangePlans)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__TimingCha__PlanI__40F9A68C");

            entity.HasOne(d => d.RollbackPlan).WithMany(p => p.TimingChangeRollbackPlans)
                .HasForeignKey(d => d.RollbackPlanId)
                .HasConstraintName("FK__TimingCha__Rollb__43D61337");
        });

        modelBuilder.Entity<TimingPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__TimingPl__755C22D7535E81E4");

            entity.ToTable("TimingPlan");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.ControllerId).HasColumnName("ControllerID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EffectiveFrom).HasColumnType("datetime");
            entity.Property(e => e.EffectiveTo).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OffsetsJson).HasColumnName("OffsetsJSON");
            entity.Property(e => e.PhasesJson).HasColumnName("PhasesJSON");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft");

            entity.HasOne(d => d.Controller).WithMany(p => p.TimingPlans)
                .HasForeignKey(d => d.ControllerId)
                .HasConstraintName("FK__TimingPla__Contr__3A4CA8FD");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TimingPlans)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__TimingPla__Creat__3B40CD36");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACD28578A6");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D1053400CEAF9A").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ValidationNote>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__Validati__EACE357FD6BDA44B");

            entity.ToTable("ValidationNote");

            entity.Property(e => e.NoteId).HasColumnName("NoteID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FaultId).HasColumnName("FaultID");
            entity.Property(e => e.NoteText).HasMaxLength(500);
            entity.Property(e => e.ValidatorId).HasColumnName("ValidatorID");

            entity.HasOne(d => d.Fault).WithMany(p => p.ValidationNotes)
                .HasForeignKey(d => d.FaultId)
                .HasConstraintName("FK__Validatio__Fault__1332DBDC");

            entity.HasOne(d => d.Validator).WithMany(p => p.ValidationNotes)
                .HasForeignKey(d => d.ValidatorId)
                .HasConstraintName("FK__Validatio__Valid__14270015");
        });

        modelBuilder.Entity<WorkLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__WorkLog__5E5499A8C671F7A4");

            entity.ToTable("WorkLog");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.EndAt).HasColumnType("datetime");
            entity.Property(e => e.LaborHours).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PhotoUrisJson).HasColumnName("PhotoURIsJSON");
            entity.Property(e => e.StartAt).HasColumnType("datetime");
            entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

            entity.HasOne(d => d.PerformedByNavigation).WithMany(p => p.WorkLogs)
                .HasForeignKey(d => d.PerformedBy)
                .HasConstraintName("FK__WorkLog__Perform__282DF8C2");

            entity.HasOne(d => d.WorkOrder).WithMany(p => p.WorkLogs)
                .HasForeignKey(d => d.WorkOrderId)
                .HasConstraintName("FK__WorkLog__WorkOrd__2739D489");
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => e.WorkOrderId).HasName("PK__WorkOrde__AE75517556EDC960");

            entity.ToTable("WorkOrder");

            entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");
            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.AssignedCrewId).HasColumnName("AssignedCrewID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Priority).HasMaxLength(20);
            entity.Property(e => e.ScheduledEnd).HasColumnType("datetime");
            entity.Property(e => e.ScheduledStart).HasColumnType("datetime");
            entity.Property(e => e.SourceId).HasColumnName("SourceID");
            entity.Property(e => e.SourceType).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Open");

            entity.HasOne(d => d.AssignedCrew).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.AssignedCrewId)
                .HasConstraintName("FK__WorkOrder__Assig__236943A5");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WorkOrders)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__WorkOrder__Creat__2180FB33");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
