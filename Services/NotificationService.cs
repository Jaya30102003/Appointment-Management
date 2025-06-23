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

    public async Task CreateForDoctor(Guid appointmentId, string message)
{
    var appointment = await _context.Appointments.FindAsync(appointmentId);
    if (appointment == null)
        throw new InvalidOperationException("Appointment not found.");

    var notification = new Notification
    {
        NotificationId = Guid.NewGuid(),
        AppointmentId = appointmentId,
        RecipientId = appointment.DoctorId,
        Recipient = RecipientType.Doctor,
        NotificationTitle = "Appointment Scheduled",
        NotificationMessage = message,
        CreatedAt = DateTime.UtcNow
    };

    _context.Notifications.Add(notification);
    await _context.SaveChangesAsync();
}


    public async Task CreateForPatient(Guid appointmentId, string message)
{
    var appointment = await _context.Appointments.FindAsync(appointmentId);
    if (appointment == null)
        throw new InvalidOperationException("Appointment not found.");

    var notification = new Notification
    {
        NotificationId = Guid.NewGuid(),
        AppointmentId = appointmentId,
        RecipientId = appointment.PatientId, 
        Recipient = RecipientType.Patient,   
        NotificationTitle = "Appointment Confirmed",
        NotificationMessage = message,
        CreatedAt = DateTime.UtcNow
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
