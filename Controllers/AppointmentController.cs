using Microsoft.AspNetCore.Mvc;
using Appointments.Request;
using Appointments.Api.Data.Responses;
using Appointments.Service;
using Appointments.DTO;

namespace Appointments.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    // Appointment Creation 
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

    //Appointment Retrieval through appointment Id
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

    //Appointment Retrieval Completely
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _appointmentService.GetAll();
        return Ok(new ApiResponse<IEnumerable<AppointmentDTO>>(true, "Appointments List Fetched Sucessfully", result));
    }

    // Appointment Deletion through Retrival using Appointment Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _appointmentService.Delete(id); // 👈 Await here too
        return NoContent();
    }

    // Doctor Priviledges For Appointment Approval
    [HttpPut("{id}/approve")]
    public async Task<IActionResult> Approve(Guid id)
    {
        await _appointmentService.Approve(id);
        return Ok("Appointment approved and notifications sent.");
    }

    // Doctor and Patient Privildegs for Appointment Cancellation
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _appointmentService.Cancel(id);
        return Ok("Appointment cancelled and notifications sent.");
    }

}
