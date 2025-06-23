using Microsoft.AspNetCore.Mvc;
using Notifications.DTO;
using Notifications.Repository;
using Notifications.Request;
using Notifications.Service;

namespace Notifications.Controller;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _service;

    public NotificationController(INotificationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("recipient/{recipient}")]
    public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetByRecipient(string recipient)
    {
        return Ok(await _service.GetAllByRecipientAsync(recipient));
    }

    [HttpPost("doctor")]
   public async Task<IActionResult> CreateForDoctor([FromBody] DoctorNotificationRequest request)
  {
    await _service.CreateForDoctor(request.AppointmentId, request.DoctorName, request.Message);
    return Ok("Doctor notification created.");
   }

    [HttpPost("patient")]
public async Task<IActionResult> CreateForPatient([FromBody] PatientNotificationRequest request)
{
    await _service.CreateForPatient(request.AppointmentId, request.PatientEmail, request.Message);
    return Ok("Patient notification created.");
}


    [HttpDelete("appointment/{appointmentId}")]
    public async Task<IActionResult> DeleteByAppointmentId(Guid appointmentId)
    {
        await _service.DeleteByAppointmentIdAsync(appointmentId);
        return NoContent();
    }

    [HttpDelete("{notificationId}")]
    public async Task<IActionResult> DeleteByNotificationId(Guid notificationId)
    {
        await _service.DeleteByNotificationIdAsync(notificationId);
        return NoContent();
    }
}
