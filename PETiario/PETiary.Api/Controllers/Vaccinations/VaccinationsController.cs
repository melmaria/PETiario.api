
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PETiario.PETiary.Application.Vaccinations.Dtos.Requests;
using PETiario.PETiary.Application.Vaccinations.Dtos.Responses;
using PETiario.PETiary.Application.Vaccinations.Services.Interfaces;

namespace PETiario.PETiary.Api.Controllers.Vaccinations
{
   [Route("vaccinations")]
    public class VaccinationsController(IVaccinationApplication vaccinationApplication) : Controller
    {

        /// <summary>
        /// Create an vaccination
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] VaccionationRequest request, CancellationToken cancellationToken = default)
        {
            await vaccinationApplication.CreateAsync(request, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Update an existing vaccination
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<VaccinationResponse>> UpdateAsync(int id, [FromBody] VaccionationRequest request, CancellationToken cancellationToken = default)
        {
            var updated = await vaccinationApplication.UpdateAsync(id, request, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Delete an existing vaccination
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await vaccinationApplication.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// List all vaccinations
        /// </summary>
        [HttpGet]
        [Authorize]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<IEnumerable<VaccinationResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await vaccinationApplication.GetAllAsync(cancellationToken);
            return Ok(list);
        }

        /// <summary>
        /// Get an vaccination by id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<VaccinationResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var vaccination = await vaccinationApplication.GetByIdAsync(id, cancellationToken);
            return Ok(vaccination);
        }
    }
}