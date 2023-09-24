using FluentValidation;
using TimeLogger.Application.Common.Validators;

namespace TimeLogger.Application.Features.Projects.Queries.Get
{
    public class GetProjectsValidator : AbstractValidator<GetProjectsCommand>
    {
        public GetProjectsValidator()
        {
            RuleFor(x => x.PageSize)
                .ValidItemsLimit();
        }
    }
}