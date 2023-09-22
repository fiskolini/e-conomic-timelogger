using FluentValidation;
using TimeLogger.Application.Common.Validators;

namespace TimeLogger.Application.Features.Projects.Commands.CreateProject
{
    public class GetProjectCommandValidator : AbstractValidator<ProjectRequest<CreateProjectResponse>>
    {
        public GetProjectCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Deadline)
                .NotEmpty()
                .WithMessage("Value is mandatory.")
                .MustContainFutureDate()
                .WithMessage("Value has to be a future date.");
        }
    }
}