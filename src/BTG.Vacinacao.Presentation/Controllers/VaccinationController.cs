using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Vacinacao.Presentation.Controllers
{
    [ApiController]
    [Tags("Vaccinations")]
    [Route("api/[controller]")]
    [Authorize]
    public class VaccinationController: ControllerBase
    {
        private readonly IMediator _mediator;
        public VaccinationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterVaccination([FromBody] RegisterVaccinationCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(RegisterVaccination), new { id = result.Id }, result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteVaccinationCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
