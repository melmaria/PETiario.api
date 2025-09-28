
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PETiario.PETiary.Application.Walks.Dtos.Requests;
using PETiario.PETiary.Application.Walks.Dtos.Responses;
using PETiario.PETiary.Application.Walks.Services.Interfaces;

namespace PETiario.PETiary.Api.Controllers.Walks
{
   [Route("walks")]
    public class WalksController(IWalkApplication walkApplication) : Controller
    {

        /// <summary>
        /// Create an walk
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] WalkRequest request, CancellationToken cancellationToken = default)
        {
            await walkApplication.CreateAsync(request, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Update an existing walk
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<WalkResponse>> UpdateAsync(int id, [FromBody] WalkRequest request, CancellationToken cancellationToken = default)
        {
            var updated = await walkApplication.UpdateAsync(id, request, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Delete an existing walk
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await walkApplication.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// List all walks
        /// </summary>
        [HttpGet]
        [Authorize]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<IEnumerable<WalkResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var list = await walkApplication.GetAllAsync(cancellationToken);
            return Ok(list);
        }

        /// <summary>
        /// Get an walk by id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<WalkResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var walk = await walkApplication.GetByIdAsync(id, cancellationToken);
            return Ok(walk);
        }
    }
}