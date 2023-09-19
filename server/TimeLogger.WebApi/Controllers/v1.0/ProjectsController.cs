using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeLogger.Application.Features;
using TimeLogger.Application.Features.ProjectFeatures.CreateProject;
using TimeLogger.Application.Features.ProjectFeatures.GetProject;

namespace TimeLogger.Api.Controllers.v1._0
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/projects
        [HttpGet]
        public async Task<ActionResult<PagedResponse<GetProjectResponse>>> Get(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            // TODO to remove
            // await Task.Delay(3000);
            var request = new GetProjectRequest()
            {
                PageNumber = pagedRequest.PageNumber,
                PageSize = pagedRequest.PageSize
            };

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        // POST api/projects
        [HttpPost]
        public async Task<ActionResult<CreateProjectResponse>> Create(CreateProjectRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}