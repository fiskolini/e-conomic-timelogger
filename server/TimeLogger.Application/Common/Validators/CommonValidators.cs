using System;
using FluentValidation;

namespace TimeLogger.Application.Common.Validators
{
    public static class CommonValidators
    {
        /// <summary>
        /// Validate the given name to not exceed 50 and to have at least 3 chars.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRuleBuilderOptions<T, string> ValidCommonName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);
        }
    }
}