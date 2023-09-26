using FluentValidation;
using TimeLogger.Application.Common.Validators;


namespace TimeLogger.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(x => x.Name)
                .ValidCommonName()
                .When(x => x.Name != null);

            // RuleFor(x => x.Deadline)
            //     .CanContainValidFutureDate()
            //     .WithMessage("Value has to be a future date.");
        }
    }
}