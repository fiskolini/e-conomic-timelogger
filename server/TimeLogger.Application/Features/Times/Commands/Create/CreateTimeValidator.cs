using FluentValidation;

namespace TimeLogger.Application.Features.Times.Commands.Create
{
    public class CreateTimeValidator : AbstractValidator<CreateTimeCommand>
    {
        public CreateTimeValidator()
        {
            RuleFor(x => x.Minutes)
                .GreaterThanOrEqualTo(30);
        }
    }
}