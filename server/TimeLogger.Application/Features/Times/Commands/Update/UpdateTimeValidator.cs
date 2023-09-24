using FluentValidation;

namespace TimeLogger.Application.Features.Times.Commands.Update
{
    public class UpdateTimeValidator : AbstractValidator<UpdateTimeCommand>
    {
        public UpdateTimeValidator()
        {
            RuleFor(x => x.Minutes)
                .GreaterThanOrEqualTo(30);
        }
    }
}