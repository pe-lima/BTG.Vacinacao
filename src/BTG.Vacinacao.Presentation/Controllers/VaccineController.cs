using BTG.Vacinacao.Application.Commands.VaccineCommand;
using BTG.Vacinacao.Application.DTOs.Vaccine;
using BTG.Vacinacao.Application.Queries.VaccineQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Vacinacao.Presentation.Controllers
{
    [ApiController]
    [Tags("Vaccines")]
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpGet("{code}")]
        public async Task<ActionResult<VaccineDto>> GetByCode(string code)
        {
            var query = new GetVaccineByCodeQuery(code);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<VaccineDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllVaccinesQuery());
            return Ok(result);
        }
    }
}
