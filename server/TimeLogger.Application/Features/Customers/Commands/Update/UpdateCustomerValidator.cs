using FluentValidation;
using TimeLogger.Application.Common.Validators;

namespace TimeLogger.Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .ValidCommonName();
        }
    }
}