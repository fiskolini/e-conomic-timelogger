using MediatR;
using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectResponse : BaseEntityResponse, IRequest<UpdateProjectCommand>
    {
        public string Name { get; set; }
    }
}