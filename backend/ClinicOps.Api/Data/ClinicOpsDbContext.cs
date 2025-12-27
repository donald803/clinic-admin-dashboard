using Microsoft.EntityFrameworkCore;
using ClinicOps.Api.Models;

namespace ClinicOps.Api.Data;

public class ClinicOpsDbContext : DbContext
{
    public ClinicOpsDbContext(DbContextOptions<ClinicOpsDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<AppointmentRequest> AppointmentRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.Property(e => e.Priority).HasConversion<string>();
        });

        modelBuilder.Entity<AppointmentRequest>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PatientName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PreferredTime).HasMaxLength(50);
            entity.Property(e => e.Reason).HasMaxLength(1000);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.Property(e => e.AdminNotes).HasMaxLength(1000);
        });
    }
}
