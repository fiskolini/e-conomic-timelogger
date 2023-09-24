using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeLogger.Application.Features.Projects.Queries.GetById;
using TimeLogger.Application.Features.Times.Commands.Create;
using TimeLogger.Application.Features.Times.Commands.Delete;
using TimeLogger.Application.Features.Times.Commands.Update;
using TimeLogger.Application.Features.Times.Queries.Get;
using TimeLogger.Application.Features.Times.Queries.GetById;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Api.Controllers
{
    [Route("api/customers/{customerId:int}/projects/{projectId:int}/times")]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetSingleTimeResponse>> GetSingle(
            int id,
            GetSingleTimeCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateTimeResponse>> Create(
            [FromBody] CreateTimeCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateTimeResponse>> Update(
            int id,
            [FromBody] UpdateTimeCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeleteTimeResponse>> Delete(DeleteTimeCommand command,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}