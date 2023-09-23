using FluentValidation;
using TimeLogger.Application.Common.Validators;

namespace TimeLogger.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<ProjectRequest<CreateProjectResponse>>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Name)
                .ValidCommonName();

            RuleFor(x => x.Deadline)
                .NotEmpty()
                .WithMessage("Value is mandatory.")
                .MustContainFutureDate()
                .WithMessage("Value has to be a future date.");
        }
    }
}