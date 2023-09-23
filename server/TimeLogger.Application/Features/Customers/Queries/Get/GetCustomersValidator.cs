using FluentValidation;

namespace TimeLogger.Application.Features.Customers.Queries.Get
{
    public class GetCustomersValidator : AbstractValidator<GetCustomersCommand>
    {
        public GetCustomersValidator()
        {
            RuleFor(x => x.PageSize)
                .LessThanOrEqualTo(500); // well... we have to limit something...
        }
    }
}