using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PETiario.PETiary.Application.Appointements.Dtos.Requests;
using PETiario.PETiary.Application.Appointements.Dtos.Responses;
using PETiario.PETiary.Application.Appointements.Services.Interfaces;

namespace PETiario.PETiary.Api.Controllers.Appointments
{
    [ApiController]
    [Route("appointments")]
    public class AppointmentController(IAppointmentApplication appointmentApplication) : ControllerBase
    {

        /// <summary>
        /// Create an appointment
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] AppointmentRequest request, CancellationToken cancellationToken = default)
        {
            await appointmentApplication.CreateAsync(request, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Update an existing appointment
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<AppointmentResponse>> UpdateAsync(int id, [FromBody] AppointmentRequest request, CancellationToken cancellationToken = default)
        {
            var updated = await appointmentApplication.UpdateAsync(id, request, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Delete an existing appointment
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await appointmentApplication.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// List all appointments
        /// </summary>
        [HttpGet]
        [Authorize]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await appointmentApplication.GetAllAsync(cancellationToken);
            return Ok(list);
        }

        /// <summary>
        /// Get an appointment by id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppointmentResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var appointment = await appointmentApplication.GetByIdAsync(id, cancellationToken);
            return Ok(appointment);
        }
    }
}
