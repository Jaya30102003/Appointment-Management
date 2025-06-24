using Appointments.DTO;
using Appointments.Model;
using Appointments.Request;
using AutoMapper;
using Appointments.AutoMapperProfile;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Appointments.Repository;
using Notifications.Service;

namespace Appointments.Service;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, INotificationService notificationService)
    {
        _appointmentRepository = appointmentRepository;
        _mapper = mapper;
        _notificationService = notificationService;
    }
    public async Task<AppointmentDTO> Create(AppointmentCreateRequest request)
    {
        var appointment = _mapper.Map<Appointment>(request);
        await _appointmentRepository.Add(appointment);
        var appointmentDto = _mapper.Map<AppointmentDTO>(appointment);
        // 🔔 Notification triggers for Doctor and Patient
        await _notificationService.CreateForDoctor(appointment.AppointmentId, "Appointment Requested.");
        await _notificationService.CreateForPatient(appointment.AppointmentId, "Appointment Request Send.");
        return appointmentDto;
    }

    public async Task Delete(Guid id)
    {
        var appointment = await _appointmentRepository.Get(id);
        if (appointment == null)
            throw new Exception("Appointment not found.");

        await _appointmentRepository.Delete(id);
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

    public async Task Approve(Guid appointmentId)
    {
        var appointment = await _appointmentRepository.Get(appointmentId);
        if (appointment == null) throw new Exception("Appointment not found");

        appointment.PaymentStatus = true;
        await _appointmentRepository.Update(appointment);

        await _notificationService.CreateForDoctor(appointment.AppointmentId, "Appointment Scheduled.");
        await _notificationService.CreateForPatient(appointment.AppointmentId, "Appointment Schedule Confirmed by Doctor.");
    }       

public async Task Cancel(Guid appointmentId)
    {
        var appointment = await _appointmentRepository.Get(appointmentId);
        if (appointment == null) throw new Exception("Appointment not found");

        appointment.IsCancelled = true;
        await _appointmentRepository.Update(appointment);

        await _notificationService.CreateForDoctor(appointment.AppointmentId, "Appointment cancelled.");
        await _notificationService.CreateForPatient(appointment.AppointmentId, "Appointment cancelled.");
    }

}