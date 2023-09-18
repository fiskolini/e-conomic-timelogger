using MediatR;

namespace TimeLogger.Application.Features.ProjectFeatures.CreateProject
{
    public sealed class CreateProjectRequest : IRequest<CreateProjectResponse>
    {
        public string Name { get; set; }
    }
}