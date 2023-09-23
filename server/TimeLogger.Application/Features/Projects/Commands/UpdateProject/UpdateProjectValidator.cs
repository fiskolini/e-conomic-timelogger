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

            RuleFor(x => x.Deadline)
                .MustContainFutureDate()
                .WithMessage("Value has to be a future date.")
                .When(x => x.Deadline != null);

            RuleFor(x => x.TimeAllocated)
                /*.Must((x, timeAllocated) => x.CompletedAt != null || timeAllocated == x.TimeAllocated)
                .WithMessage("Once a project is complete it can no longer receive new registrations.")*/
                .Must(x => x == null || x == 0 || x >= 30)
                .WithMessage("Individual time registrations should be 30 minutes or longer");
        }
    }
}