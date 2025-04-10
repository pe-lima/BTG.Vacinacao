using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.DTOs;
using BTG.Vacinacao.Application.Queries.PersonQuery;
using BTG.Vacinacao.Application.Queries.VaccinationCardQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Vacinacao.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Register([FromBody] RegisterPersonCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Register), new { id = result.Id }, result);
        }

        [HttpGet("{cpf}/vaccination-card")]
        public async Task<ActionResult<VaccinationCardDto>> GetVaccinationCardByCpf(string cpf)
        {
            var query = new GetVaccinationCardByCpfQuery(cpf);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllPersonsQuery());
            return Ok(result);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<PersonDto>> GetByCpf(string cpf)
        {
            var query = new GetPersonByCpfQuery(cpf);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{cpf}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string cpf)
        {
            var command = new DeletePersonCommand(cpf);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
