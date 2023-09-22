using System;
using FluentValidation;

namespace TimeLogger.Application.Common.Validators
{
    public static class DateTimeValidator
    {
        public static IRuleBuilderOptions<T, string> MustContainValidDate<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x => x != null && DateTime.TryParse(x.ToString(), out _));
        }
        
        public static IRuleBuilderOptions<T, string> MustContainFutureDate<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(x =>
            {
                if (!DateTime.TryParse(x, out DateTime date)) return false;
                return date > DateTime.Now;
            });
        }
    }
}