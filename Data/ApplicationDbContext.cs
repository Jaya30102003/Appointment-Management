using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Appointments.Model;
using Notifications.Model;

namespace Applications.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optional: Configure the Appointment entity (you can expand this later)
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(a => a.AppointmentId);
            entity.Property(a => a.PatientId).IsRequired();
            entity.Property(a => a.DoctorId).IsRequired();
            entity.Property(a => a.Reason);
            entity.Property(a => a.Remarks);
            entity.Property(a => a.PaymentStatus);
            entity.Property(a => a.TimeSlot);
        });
            
        modelBuilder.Entity<Notification>()
    .HasOne(n => n.Appointment)
    .WithMany()
    .HasForeignKey(n => n.AppointmentId);


    }
}