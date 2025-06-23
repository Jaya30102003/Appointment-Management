using Notifications.DTO;

namespace Notifications.Repository;

public interface INotificationRepository
{
    Task CreateForPatient(Guid appointmentId, string patientEmail, string message);
    Task<IEnumerable<NotificationDTO>> GetAll();
    Task<IEnumerable<NotificationDTO>> GetAllByRecipientAsync(string recipient);
    Task DeleteByAppointmentId(Guid appointmentId);
    Task DeleteByNotificationId(Guid notificationId);
}
