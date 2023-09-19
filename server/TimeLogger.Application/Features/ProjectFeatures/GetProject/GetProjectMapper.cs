using System;
using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.ProjectFeatures.GetProject
{
    public class GetProjectMapper : Profile
    {
        public GetProjectMapper()
        {
            CreateMap<Project, GetProjectResponse>();
            CreateMap<PagedResponse<Project>, PagedResponse<GetProjectResponse>>();
        }
    }
}