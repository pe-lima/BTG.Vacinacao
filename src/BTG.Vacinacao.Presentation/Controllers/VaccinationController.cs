using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using BTG.Vacinacao.Application.DTOs.Vaccination;
using BTG.Vacinacao.Application.Queries.VaccinationCardQuery;
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

        /// <summary>
        /// Registers a new vaccination for a person
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(VaccinationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VaccinationDto>> Register([FromBody] RegisterVaccinationCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, result);
        }

        /// <summary>
        /// Deletes a vaccination record by its ID
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteVaccinationCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Gets a vaccination card by person CPF
        /// </summary>
        [HttpGet("{cpf}")]
        [ProducesResponseType(typeof(List<VaccinationRecordDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<VaccinationRecordDto>>> GetByCpf(string cpf)
        {
            var query = new GetVaccinationCardByCpfQuery(cpf);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
