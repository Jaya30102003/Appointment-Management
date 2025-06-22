using Appointments.DTO;
using Appointments.Model;
using Appointments.Request;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Appointments.Service;
using Applications.DbContexts;

namespace Appointments.Repository;

public interface IAppointmentRepository
{
    public Task<Appointment> Add(Appointment appointment);

    public Task<Appointment> Get(Guid id);

    public Task<IEnumerable<Appointment>> GetAll();

    public void Delete(Guid id);
}