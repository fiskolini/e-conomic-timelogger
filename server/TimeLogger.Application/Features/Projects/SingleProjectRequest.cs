using MediatR;

namespace TimeLogger.Application.Features.Projects
{
    public class SingleProjectRequest : IRequest<ProjectResponse>
    {
        public int Id { get; set; }
    }
}