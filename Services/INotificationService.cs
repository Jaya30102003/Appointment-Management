using Notifications.DTO;

namespace Notifications.Service;

public interface INotificationService
{
    Task CreateForDoctor(Guid appointmentId, string doctorName, string message);
    Task CreateForPatient(Guid appointmentId, string patientEmail, string message);
    Task<IEnumerable<NotificationDTO>> GetAllByRecipientAsync(string recipient);
    Task<IEnumerable<NotificationDTO>> GetAll();
    Task DeleteByAppointmentIdAsync(Guid appointmentId);
    Task DeleteByNotificationIdAsync(Guid notificationId);
}
