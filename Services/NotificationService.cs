using Notifications.Model;
using Notifications.DTO;
using Notifications.Repository;
using Applications.DbContexts;

namespace Notifications.Service;

public class NotificationService : INotificationService
{
    private readonly ApplicationDbContext _context;

    public NotificationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateForDoctor(Guid appointmentId, string doctorName, string message)
    {
        var notification = new Notification
        {
            NotificationId = Guid.NewGuid(),
            AppointmentId = appointmentId,
            NotificationTitle = "Doctor Notification",
            NotificationMessage = message,
            Recipient = RecipientType.Doctor,
            RecipientName = doctorName // or patientEmail

        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
    }

    public async Task CreateForPatient(Guid appointmentId, string patientEmail, string message)
    {
        var notification = new Notification
        {
            NotificationId = Guid.NewGuid(),
            AppointmentId = appointmentId,
            NotificationTitle = "Patient Notification",
            NotificationMessage = message,
            Recipient = RecipientType.Patient,
            RecipientName = patientEmail
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<NotificationDTO>> GetAllByRecipientAsync(string recipient)
    {
        var repo = new NotificationRepository(_context);
        return await repo.GetAllByRecipientAsync(recipient);
    }

    public async Task<IEnumerable<NotificationDTO>> GetAll()
    {
        var repo = new NotificationRepository(_context);
        return await repo.GetAll();
    }

    public async Task DeleteByAppointmentIdAsync(Guid appointmentId)
    {
        var repo = new NotificationRepository(_context);
        await repo.DeleteByAppointmentId(appointmentId);
    }

    public async Task DeleteByNotificationIdAsync(Guid notificationId)
    {
        var repo = new NotificationRepository(_context);
        await repo.DeleteByNotificationId(notificationId);
    }
}
