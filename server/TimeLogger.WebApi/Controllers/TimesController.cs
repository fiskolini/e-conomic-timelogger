using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeLogger.Application.Features.Times.Commands.Create;
using TimeLogger.Application.Features.Times.Commands.Delete;
using TimeLogger.Application.Features.Times.Commands.Update;
using TimeLogger.Application.Features.Times.Queries.Get;
using TimeLogger.Application.Features.Times.Queries.GetById;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Api.Controllers
{
    [Route("api/[controller]")]
    public class TimesController : Controller
    {
        private readonly IMediator _mediator;

        public TimesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResults<GetTimesResponse>>> GetAll(GetTimesCommand pagedCommand,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(pagedCommand, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<GetSingleTimeResponse>> GetSingle(
            GetSingleTimeCommand command,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateTimeResponse>> Create(
            [FromBody] CreateTimeCommand command,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetSingle), new { id = response.Id }, response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateTimeResponse>> Update(
            int id,
            [FromBody] UpdateTimeCommand command,
            CancellationToken cancellationToken)
        {
            // Prevent someone to send mismatched data through payload
            command.Id = id;

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<NoContentResult> Delete(DeleteTimeCommand request,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }
    }
}