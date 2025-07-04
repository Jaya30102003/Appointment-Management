using Appointments.DTO;
using Appointments.Model;
using Appointments.Request;
using Notifications.DTO;

namespace Appointments.Service;

public interface IAppointmentService
{
    public Task<AppointmentDTO> Create(AppointmentCreateRequest request);

    public Task<AppointmentDTO> GetById(Guid guid);

    public Task<IEnumerable<AppointmentDTO>> GetAll();

    public Task Delete(Guid id);

    public Task Approve(Guid appointmentId);

    public Task Cancel(Guid appointmentId);
}