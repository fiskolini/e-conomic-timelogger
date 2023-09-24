using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeLogger.Application.Features.Projects.Commands.CreateProject;
using TimeLogger.Application.Features.Projects.Commands.DeleteProject;
using TimeLogger.Application.Features.Projects.Commands.UpdateProject;
using TimeLogger.Application.Features.Projects.Queries.Get;
using TimeLogger.Application.Features.Projects.Queries.GetById;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Api.Controllers
{
    [Route("api/customers/{customerId:int}/projects/")]
    public class ProjectsController : Controller
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResults<GetProjectsResponse>>> GetAll(GetProjectsCommand pagedCommand,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(pagedCommand, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetSingleProjectResponse>> GetSingle(
            int id,
            GetSingleProjectCommand project,
            CancellationToken cancellationToken)
        {
            project.Id = id;

            var response = await _mediator.Send(project, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateProjectResponse>> Create(
            [FromBody] CreateProjectCommand request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(GetSingle), new { id = response.Id }, response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateProjectResponse>> Update(
            int id,
            [FromBody] UpdateProjectCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeleteProjectResponse>> Delete(DeleteProjectCommand command,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}