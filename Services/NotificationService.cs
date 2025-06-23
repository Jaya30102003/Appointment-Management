using Notifications.Model;
using Notifications.DTO;
using Notifications.Repository;
using Applications.DbContexts;

namespace Notifications.Service;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;
    private readonly ApplicationDbContext _context;

    public NotificationService(ApplicationDbContext context, INotificationRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task CreateForDoctor(Guid appointmentId, string message)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null)
            throw new InvalidOperationException("Appointment not found.");

        var fullMessage = $"{message} Scheduled at: {appointment.TimeSlot:f})";
        await _repository.CreateForDoctor(appointmentId, appointment.DoctorId, fullMessage);
    }

    public async Task CreateForPatient(Guid appointmentId, string message)
    {
        var appointment = await _context.Appointments.FindAsync(appointmentId);
        if (appointment == null)
            throw new InvalidOperationException("Appointment not found.");

        var fullMessage = $"{message} Scheduled at: {appointment.TimeSlot:f}";
        await _repository.CreateForPatient(appointmentId, appointment.PatientId, fullMessage);
    }

    public async Task<IEnumerable<NotificationDTO>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<IEnumerable<NotificationDTO>> GetAllByRecipientAsync(string recipient)
    {
        return await _repository.GetAllByRecipientAsync(recipient);
    }

    public async Task DeleteByAppointmentIdAsync(Guid appointmentId)
    {
        await _repository.DeleteByAppointmentId(appointmentId);
    }

    public async Task DeleteByNotificationIdAsync(Guid notificationId)
    {
        await _repository.DeleteByNotificationId(notificationId);
    }
}
