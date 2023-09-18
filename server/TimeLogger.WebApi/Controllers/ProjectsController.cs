using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeLogger.Application.Features.ProjectFeatures.CreateProject;
using TimeLogger.Persistence.Context;

namespace TimeLogger.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly DataContext _context;
        private readonly IMediator _mediator;

        public ProjectsController(DataContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET api/projects
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Projects);
        }

        // GET api/projects
        [HttpPost]
        public async Task<ActionResult<CreateProjectResponse>> Create(CreateProjectRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}