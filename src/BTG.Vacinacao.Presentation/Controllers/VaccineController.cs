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

        /// <summary>
        /// Registers a new vaccine
        /// </summary>
        /// <returns>The registered vaccine</returns>
        [HttpPost]
        [ProducesResponseType(typeof(VaccineDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VaccineDto>> Register([FromBody] RegisterVaccineCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByCode), new { code = result.Code }, result);
        }

        /// <summary>
        /// Gets a vaccine by its code
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(typeof(VaccineDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VaccineDto>> GetByCode(string code)
        {
            var query = new GetVaccineByCodeQuery(code);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets all vaccines
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<VaccineDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<VaccineDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllVaccinesQuery());
            return Ok(result);
        }
    }
}
