using ApiDataSharp.Interfaces;
using ApiDataSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiDataSharp.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereDataSharp<T>(
            this IQueryable<T> queryable,
            IDataSharpRequest request
        ) {
            if (request is IFilterRequest<T> filterRequest && filterRequest.Filter != null)
            {
                queryable = FilteringService.Filter(queryable, filterRequest);
            }

            if (request is ISortRequest<T> sortRequest && sortRequest.SortBy != null)
            {
                queryable = SortingService.Sort(queryable, sortRequest);
            }

            return queryable;
        }
    }
}
