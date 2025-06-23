using Notifications.DTO;

namespace Notifications.Repository
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationDTO>> GetAll();
        Task<IEnumerable<NotificationDTO>> GetAllByRecipientAsync(string recipient);
        Task CreateForDoctor(Guid appointmentId, Guid doctorId, string message);
        Task CreateForPatient(Guid appointmentId, Guid patientId, string message);
        Task DeleteByAppointmentId(Guid appointmentId);
        Task DeleteByNotificationId(Guid notificationId);
    }
}
