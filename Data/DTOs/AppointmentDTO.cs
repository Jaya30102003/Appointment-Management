using System;
using Appointments.Model;

namespace Appointments.DTO;
public class AppointmentDTO
{
    public Guid AppointmentId { get; set; }

    public Guid PatientId { get; set; }
    public Guid DoctorId{ get; set; }
    public string Reason { get; set; }
    public string? Remarks { get; set; }

    public bool PaymentStatus { get; set; }

    public DateTime TimeSlot { get; set; }
}