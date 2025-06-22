using Appointments.DTO;
using Appointments.Model;
using Appointments.Request;
using AutoMapper;
using Appointments.AutoMapperProfile;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Appointments.Repository;

namespace Appointments.Service;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;
    public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
    }
    public async Task<AppointmentDTO> Create(AppointmentCreateRequest request)
    {
        var appointment = _mapper.Map<Appointment>(request);
        await _appointmentRepository.Add(appointment);
        var appointmentDto = _mapper.Map<AppointmentDTO>(appointment);
        return appointmentDto;
    }

    public async Task Delete(Guid id)
    {
        Appointment appointment = await _appointmentRepository.Get(id);
        if (appointment == null)
        {
            throw new Exception("Product Not Found");
        }
        _appointmentRepository.Delete(id);
    }

    public async Task<IEnumerable<AppointmentDTO>> GetAll()
    {
        var products = await _appointmentRepository.GetAll();
        var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDTO>>(products);
        return appointmentDtos;
    }

    public async Task<AppointmentDTO> GetById(Guid id)
    {
        var appointment = await _appointmentRepository.Get(id);
        var appointmentDto = _mapper.Map<AppointmentDTO>(appointment);
        return appointmentDto;
    }

}