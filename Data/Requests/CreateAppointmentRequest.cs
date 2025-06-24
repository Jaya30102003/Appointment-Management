using Appointments.Model;

namespace Appointments.Request;

public class AppointmentCreateRequest
{
    public string PatientId { get; set; }
    public string DoctorId{ get; set; }
    public string Reason { get; set; } 
    public string? Remarks { get; set; }
    public DateTime TimeSlot { get; set; }
}