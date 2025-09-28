
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PETiario.PETiary.Application.Owners.Dtos.Requests;
using PETiario.PETiary.Application.Owners.Dtos.Responses;
using PETiario.PETiary.Application.Owners.Services.Interfaces;

namespace PETiario.PETiary.Api.Controllers.Owners
{
    [Route("owners")]
    public class OwnersController(IOwnerApplication ownerApplication) : Controller
    {

        /// <summary>
        /// Create an owner
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] OwnerRequest request, CancellationToken cancellationToken = default)
        {
            await ownerApplication.CreateAsync(request, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Update an existing owner
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<OwnerResponse>> UpdateAsync(int id, [FromBody] OwnerRequest request, CancellationToken cancellationToken = default)
        {
            var updated = await ownerApplication.UpdateAsync(id, request, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Delete an existing owner
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await ownerApplication.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// List all owners
        /// </summary>
        [HttpGet]
        [Authorize]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await ownerApplication.GetAllAsync(cancellationToken);
            return Ok(list);
        }

        /// <summary>
        /// Get an owner by id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OwnerResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var owner = await ownerApplication.GetByIdAsync(id, cancellationToken);
            return Ok(owner);
        }
    }
}