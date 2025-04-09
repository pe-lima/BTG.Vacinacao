using BTG.Vacinacao.Application.Commands.VaccinationCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Vacinacao.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController: ControllerBase
    {
        public IMediator _mediator;
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
    }
}
