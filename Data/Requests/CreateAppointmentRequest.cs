using Appointments.Model;

namespace Appointments.Request;

public class AppointmentCreateRequest
{
    // public Guid PatientId { get; set; }
    // public Guid DoctorId{ get; set; }
    public string PatientName { get; set; }
    public string Reason { get; set; } 
    public string? Remarks { get; set; }
    public DateTime TimeSlot { get; set; }
}