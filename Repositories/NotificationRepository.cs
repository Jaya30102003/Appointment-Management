using Notifications.Model;
using Notifications.DTO;
using Applications.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Notifications.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotificationDTO>> GetAll()
    {
        return await _context.Notifications
            .Select(n => new NotificationDTO
            {
                NotificationId = n.NotificationId,
                AppointmentId = n.AppointmentId,
                NotificationTitle = n.NotificationTitle,
                NotificationMessage = n.NotificationMessage,
                CreatedAt = n.CreatedAt,
                Recipient = n.Recipient
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<NotificationDTO>> GetAllByRecipientAsync(string recipient)
    {
        if (!Enum.TryParse<RecipientType>(recipient, true, out var recipientEnum))
            return new List<NotificationDTO>();

        return await _context.Notifications
            .Where(n => n.Recipient == recipientEnum)
            .Select(n => new NotificationDTO
            {
                NotificationId = n.NotificationId,
                AppointmentId = n.AppointmentId,
                NotificationTitle = n.NotificationTitle,
                NotificationMessage = n.NotificationMessage,
                CreatedAt = n.CreatedAt,
                Recipient = n.Recipient
            })
            .ToListAsync();
    }

    public async Task DeleteByAppointmentId(Guid appointmentId)
    {
        var records = _context.Notifications.Where(n => n.AppointmentId == appointmentId);
        _context.Notifications.RemoveRange(records);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByNotificationId(Guid notificationId)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification is not null)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
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
        CreatedAt = DateTime.UtcNow
    };

    _context.Notifications.Add(notification);
    await _context.SaveChangesAsync();
}

}
