using Microsoft.AspNetCore.Mvc;
using Appointments.Request;
using Appointments.Api.Data.Responses;
using Appointments.Service;
using Appointments.DTO;

namespace Appointments.Controller;


[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AppointmentCreateRequest request)
    {
        var result = await _appointmentService.Create(request);
        if (result == null)
        {
            return BadRequest(new ApiResponse<AppointmentDTO>(false, "Appointment Creation Failed", null));
        }
        return CreatedAtAction(nameof(GetById), new { id = result.AppointmentId }, new ApiResponse<AppointmentDTO>(true, "Product Created Successfully", result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _appointmentService.GetById(id);
        if (result == null)
        {
            return NotFound(new ApiResponse<AppointmentDTO>(false, "Appointment Not Found", null));
        }
        return Ok(new ApiResponse<AppointmentDTO>(true, "Appointment Found", result));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _appointmentService.GetAll();
        return Ok(new ApiResponse<IEnumerable<AppointmentDTO>>(true, "Appointments List Fetched Sucessfully", result));
    }

    [HttpDelete("{id}")]
public async Task<IActionResult> Delete(Guid id)
{
    await _appointmentService.Delete(id); // 👈 Await here too
    return NoContent();
}


}
