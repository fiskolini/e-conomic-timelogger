using System;
using System.Linq;
using System.Linq.Expressions;
using TimeLogger.Domain.Common;
using TimeLogger.Domain.Contracts;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence.Common.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Plugs Soft Delete validation to consider or not items soft deleted.
        /// To ignore by default, so soft deleted items won't be considered.
        /// </summary>
        public static IQueryable<T> WhereCanBeSoftDeleted<T>(this IQueryable<T> query, bool considerDeleted)
        {
            if (considerDeleted) return query;

            // Define a parameter for the entity type
            var parameter = Expression.Parameter(typeof(T), "entity");

            // Create an expression representing the condition: entity.DateDeleted == null
            Expression condition = Expression.Equal(
                Expression.Property(parameter, "DateDeleted"),
                Expression.Constant(null, typeof(DateTimeOffset?))
            );

            // Create a lambda expression and use it in the Where method
            var lambda = Expression.Lambda(condition, parameter);
            return query.Where((Expression<Func<T, bool>>)lambda);
        }

        /// <summary>
        /// Plug paged results based on given PagedRequest
        /// </summary>
        public static IQueryable<T> WithPagedResults<T>(this IQueryable<T> query, PagedRequest request)
        {
            var skipCount = (request.PageNumber - 1) * request.PageSize;
            return query.Skip(skipCount).Take(request.PageSize);
        }

        /// <summary>
        /// Plug search by named results
        /// </summary>
        public static IQueryable<T> WhereNameContains<T>(this IQueryable<T> query, string needle)
            where T : INamedEntity, IIdentifiableEntity
        {
            if (needle == null)
            {
                return query;
            }

            var unifiedNeedle = needle.ToLower();

            return query.Where(
                x => x.Name.ToLower().Contains(unifiedNeedle) || x.Id.ToString().ToLower().Contains(unifiedNeedle)
            );
        }
    }
}