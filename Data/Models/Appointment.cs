using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Model;

[Table("Appointments")]
public class Appointment
{
    [Key]
    public Guid AppointmentId { get; set; }

    public Guid PatientId { get; set; }
    public Guid DoctorId{ get; set; }
    
    [MaxLength(120)]
    public string? Remarks { get; set; }
    public string Reason { get; set; }
    public bool PaymentStatus { get; set; }

    public DateTime TimeSlot { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;

    public Appointment(){}
}
