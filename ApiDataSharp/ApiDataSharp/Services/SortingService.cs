using ApiDataSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ApiDataSharp.Services
{
    public static class SortingService
    {
        public static IQueryable<T> Sort<T>(IQueryable<T> queryable, ISortRequest<T> request)
        {
            if (request.SortBy is null)
                return queryable;

            var sorters = request.SortBy.Split(',').Select(a => a.Trim().ToLowerInvariant());

            var sortDictionary = new Dictionary<string, string>();

            foreach (var sorter in sorters)
            {
                var match = SortConstants.SortOrderRegex.Match(sorter);

                if (!match.Success || match.Groups.Count != 3)
                    throw new ArgumentException(
                        $"Sort by parameter does not match regex {SortConstants.SortOrderRegex}"
                    );

                var order = match.Groups[1].Value;
                var property = match.Groups[2].Value;

                if (sortDictionary.ContainsKey(property))
                    throw new ArgumentException(
                        $"Sort by parameter {property} is defined more than once"
                    );

                sortDictionary.Add(property, order);
            }

            var alreadyUsedSort = false;

            foreach (var sorter in sortDictionary)
            {
                var propertyInfo = typeof(T).GetProperty(
                    sorter.Key,
                    BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
                );

                if (propertyInfo is null)
                    throw new ArgumentException(
                        $"Sort by parameter {sorter.Key} does not match any of {typeof(T).Name} properties"
                    );

                var paramExpr = Expression.Parameter(typeof(T));
                var propAccess = Expression.PropertyOrField(paramExpr, propertyInfo.Name);
                var expr = Expression.Lambda(propAccess, paramExpr);

                var method = typeof(Queryable).GetMethods()
                    .FirstOrDefault(
                        m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2
                    )
                    .MakeGenericMethod(typeof(T), propertyInfo.PropertyType);

                MethodInfo appropriateMethod = default;

                if (sorter.Value == SortConstants.SORT_DESCENDING && !alreadyUsedSort)
                    appropriateMethod = SortConstants.OrderByDescendingMethod;
                else if (sorter.Value == SortConstants.SORT_ASCENDING && !alreadyUsedSort)
                    appropriateMethod = SortConstants.OrderByMethod;
                else if (sorter.Value == SortConstants.SORT_DESCENDING && alreadyUsedSort)
                    appropriateMethod = SortConstants.ThenByDescendingMethod;
                else if (sorter.Value == SortConstants.SORT_ASCENDING && alreadyUsedSort)
                    appropriateMethod = SortConstants.ThenByMethod;

                queryable =
                    (IQueryable<T>)appropriateMethod.MakeGenericMethod(
                            typeof(T),
                            propertyInfo.PropertyType
                        )
                        .Invoke(null, new object[] { queryable, expr });

                alreadyUsedSort = true;
            }

            return queryable;
        }
    }
}
