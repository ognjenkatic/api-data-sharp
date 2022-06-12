using ApiDataSharp.Interfaces;
using ApiDataSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiDataSharp.Services
{
    public static class PaginationService
    {
        public static IQueryable<T> Paginate<T>(
            IQueryable<T> queryable,
            IPaginateRequest<T> request
        ) {
            if (request.PageSize < 1)
                throw new ArgumentException($"Invalid page size {request.PageSize}");

            if (request.PageIndex < 1)
                throw new ArgumentException($"Invalid page index {request.PageIndex}");

            var totalCount = queryable.Count();
            return null;
        }
    }
}
