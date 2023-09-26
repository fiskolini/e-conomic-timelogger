using FluentValidation;
using TimeLogger.Application.Common.Validators;

namespace TimeLogger.Application.Features.Times.Queries.Get
{
    public class GetTimesValidator : AbstractValidator<GetTimesCommand>
    {
        public GetTimesValidator()
        {
            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo500();
        }
    }
}