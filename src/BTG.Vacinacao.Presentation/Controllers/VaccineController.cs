using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Vacinacao.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccineController :  ControllerBase
    {
        private readonly IMediator _mediator;
        public VaccineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<VaccineDto>> Register([FromBody] RegisterVaccineCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Register), new { id = result.Id }, result);
        }
    }
}
