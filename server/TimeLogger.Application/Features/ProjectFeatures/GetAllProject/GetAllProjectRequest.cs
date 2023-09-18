using System.Collections.Generic;
using MediatR;

namespace TimeLogger.Application.Features.ProjectFeatures.GetAllProject
{
    public sealed class GetAllProjectRequest : IRequest<List<GetAllProjectResponse>>
    {
    }
}