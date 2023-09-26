using FluentValidation;

namespace TimeLogger.Application.Features.Projects.Queries.GetById
{
    public class GetSingleProjectValidator : AbstractValidator<GetSingleProjectCommand>
    {
        public GetSingleProjectValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}