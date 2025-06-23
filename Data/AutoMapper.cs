using System;
using AutoMapper;
using Appointments.Controller;
using Appointments.DTO;
using Appointments.Model;
using Appointments.Request;
using Appointments.Api.Data.Responses;
using Notifications.Model;
using Notifications.DTO;

namespace Appointments.AutoMapperProfile;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Appointment, AppointmentDTO>().ReverseMap();
        CreateMap<AppointmentCreateRequest, Appointment>();
        CreateMap<Notification, NotificationDTO>().ReverseMap();
    }
}