using FluentValidation;
using TimeLogger.Application.Common.Validators;

namespace TimeLogger.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.Name)
                .ValidCommonName();

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Valid CustomerId is required to create a project.");

            RuleFor(x => x.Deadline)
                .CanContainValidFutureDate()
                .WithMessage("Has to be a valid future date.");
        }
    }
}