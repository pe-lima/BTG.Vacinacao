using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.DTOs.Person;
using BTG.Vacinacao.Application.Queries.PersonQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Vacinacao.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new person
        /// </summary>
        /// <returns>The created person</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PersonDto>> Register([FromBody] RegisterPersonCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByCpf), new { cpf = result.Cpf }, result);
        }

        /// <summary>
        /// Gets a person by CPF
        /// </summary>
        [HttpGet("{cpf}")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PersonDto>> GetByCpf(string cpf)
        {
            var query = new GetPersonByCpfQuery(cpf);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets all registered persons
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<PersonDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<PersonDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllPersonsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Deletes a person by CPF
        /// </summary>
        [HttpDelete("{cpf}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(string cpf)
        {
            var command = new DeletePersonCommand(cpf);
            await _mediator.Send(command);
            return NoContent();
        }
    }

}

