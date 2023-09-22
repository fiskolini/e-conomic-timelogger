using System.Collections.Generic;
using TimeLogger.Domain.Common;

namespace TimeLogger.Domain.Validators
{
    public interface IValidator<in T, in TOut> where T : BaseEntity
    {
        Dictionary<string, string[]> Validate(T entity, TOut other);
    }
}