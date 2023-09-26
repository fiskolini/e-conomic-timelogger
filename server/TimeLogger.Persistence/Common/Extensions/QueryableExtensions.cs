using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
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
            where T : ISoftDeletedEntity
        {
            if (considerDeleted) return query;

            return query.Where(x => x.DateDeleted == null);
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
        /// Plug search by name or id results
        /// </summary>
        public static IQueryable<T> WhereContainsNameOrId<T>(this IQueryable<T> query, string needle)
            where T : INamedEntity, IIdentifiableEntity
        {
            if (needle == null) return query;


            return query.Where(
                x => x.Name.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0 ||
                     x.Id.ToString().IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0
            );
        }

        /// <summary>
        /// Plug sorting from request, such as orderBy=name%20asc || orderBy=id%20desc.
        /// </summary>
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string orderByQueryString)
        {
            // Check if the query is empty or if no sorting parameters are provided
            if (!query.Any() || string.IsNullOrWhiteSpace(orderByQueryString))
                return query;

            // Split the orderByQueryString into individual sorting parameters
            var orderParams = orderByQueryString.Trim().Split(',');

            // Get the property information for the type T
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Initialize a StringBuilder to build the sorting query
            var orderQueryBuilder = new StringBuilder();

            // Iterate through each sorting parameter
            foreach (var param in orderParams)
            {
                // Skip empty or invalid parameters
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                // Extract the property name from the parameter
                var propertyFromQueryName = param.Split(" ")[0];

                // Find the corresponding property in the type T
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                // Skip properties that do not exist in the type T
                if (objectProperty == null)
                    continue;

                // Determine the sorting order (ascending or descending) based on the parameter
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                // Append the property name and sorting order to the sorting query
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            // Remove the trailing comma and space from the sorting query
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            // Apply the sorting to the IQueryable collection
            return query.OrderBy(orderQuery);
        }
    }
}