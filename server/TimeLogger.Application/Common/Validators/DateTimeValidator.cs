using System;
using FluentValidation;

namespace TimeLogger.Application.Common.Validators
{
    public static class DateTimeValidator
    {
        /// <summary>
        /// Validate given date and try to parse with valid one
        /// </summary>
        public static IRuleBuilderOptions<T, string> MustContainValidDate<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => x != null && DateTime.TryParse(x.ToString(), out _));
        }

        /// <summary>
        /// Validate future date
        /// </summary>
        public static IRuleBuilderOptions<T, string> CanContainValidFutureDate<T>(
            this IRuleBuilder<T, string> ruleBuilder, bool mandatory = false)
        {
            return ruleBuilder.Must(x =>
            {
                if (!mandatory && x == null)
                {
                    return true;
                }

                if (!DateTime.TryParse(x, out DateTime date)) return false;

                var today = DateTime.Now;
                
                return date >= today;
            });
        }
    }
}