using MediatR;

namespace TimeLogger.Application.Features.Projects
{
    public sealed class ProjectRequest<T> : IRequest<T>
    {
        public string Name { get; set; }
        public string Deadline { get; set; }
    }
}