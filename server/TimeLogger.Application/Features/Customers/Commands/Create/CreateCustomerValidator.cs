using FluentValidation;
using TimeLogger.Application.Common.Validators;

namespace TimeLogger.Application.Features.Customers.Commands.Create
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .ValidCommonName();
        }
    }
}