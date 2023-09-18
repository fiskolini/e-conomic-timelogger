using FluentValidation;

namespace TimeLogger.Application.Features.ProjectFeatures.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectValidator()
        {
            // name property
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);
        }
    }
}