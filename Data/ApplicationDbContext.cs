using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Appointments.Model;

namespace Applications.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Configure the Appointment entity (you can expand this later)
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.AppointmentId);
                entity.Property(a => a.PatientName).IsRequired();
                entity.Property(a => a.Reason);
                entity.Property(a => a.Remarks);
                entity.Property(a => a.PaymentStatus);
                entity.Property(a => a.TimeSlot);
            });

            // If you're using any List<T> properties in other entities, like List<Category>, you can configure them here
            // Just like the earlier JSON + ValueComparer example
        }
}