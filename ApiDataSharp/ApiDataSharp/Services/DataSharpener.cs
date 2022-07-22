using ApiDataSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiDataSharp.Services
{
    public class DataSharpener<T> : IDataSharpener<T>
    {
        private readonly FilteringService _filteringService;

        public DataSharpener(FilteringService filteringService)
        {
            _filteringService = filteringService;
        }

        public R Paginate<R>(IQueryable<T> queryable, IPaginateRequest<T> request)
            where R : IPaginateResponse<T>
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Sort(IQueryable<T> queryable, ISortRequest<T> request)
        {
            return SortingService.Sort(queryable, request);
        }

        public IQueryable<T> Filter(IQueryable<T> queryable, IFilterRequest<T> request)
        {
            return _filteringService.Filter(queryable,request);
        }

        public IQueryable<T> Window(IQueryable<T> queryable, IPaginateRequest<T> request)
        {
            throw new NotImplementedException();
        }
    }
}
