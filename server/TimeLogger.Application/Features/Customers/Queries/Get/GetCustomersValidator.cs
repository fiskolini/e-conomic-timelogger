using FluentValidation;
using TimeLogger.Application.Common.Validators;

namespace TimeLogger.Application.Features.Customers.Queries.Get
{
    public class GetCustomersValidator : AbstractValidator<GetCustomersCommand>
    {
        public GetCustomersValidator()
        {
            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo500();
        }
    }
}