using BTG.Vacinacao.Application.Commands.PersonCommand;
using BTG.Vacinacao.Application.DTOs;
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
    }
}
