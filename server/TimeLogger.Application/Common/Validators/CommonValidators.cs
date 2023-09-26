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

        /// <summary>
        /// Validate the given items limit withing the range (< 500)
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRuleBuilderOptions<T, int> LessThanOrEqualTo500<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .LessThanOrEqualTo(500); // well... we have to limit something...
        }
    }
}