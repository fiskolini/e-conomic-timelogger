using MediatR;

namespace TimeLogger.Application.Features.ProjectFeatures.GetProject
{
    public sealed class GetProjectRequest : PagedRequest, IRequest<PagedResponse<GetProjectResponse>>
    {
    }
}