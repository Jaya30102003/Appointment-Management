using Appointments.DTO;
using Appointments.Model;
using Appointments.Request;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Appointments.Service;
using Applications.DbContexts;

namespace Appointments.Repository;

public class AppointmentRepository : IAppointmentRepository
{

    public ApplicationDbContext _appDbContext;

    public AppointmentRepository(ApplicationDbContext applicationDbContext)
    {
        _appDbContext = applicationDbContext;
    }
    public async Task<Appointment> Add(Appointment appointment)
    {
        await _appDbContext.Appointments.AddAsync(appointment);
        await _appDbContext.SaveChangesAsync();
        return appointment;
    }

    public async Task Delete(Guid id)
{
    var appointment = await _appDbContext.Appointments.FindAsync(id);
    if (appointment == null) return;

    _appDbContext.Appointments.Remove(appointment);
    await _appDbContext.SaveChangesAsync();
}


    public async Task<Appointment> Get(Guid id)
    {
        var appointment = await _appDbContext.Appointments.FindAsync(id);
        return appointment;
    }

    public async Task<IEnumerable<Appointment>> GetAll()
    {
        return await _appDbContext.Appointments.ToListAsync<Appointment>();
    }
}