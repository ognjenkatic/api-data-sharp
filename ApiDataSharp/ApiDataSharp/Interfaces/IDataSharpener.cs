using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiDataSharp.Interfaces
{
    public interface IDataSharpener<T>
    {
        IQueryable<T> Sort(IQueryable<T> queryable, ISortRequest<T> request);
        IQueryable<T> Filter(IQueryable<T> queryable, IFilterRequest<T> request);
        IQueryable<T> Window(IQueryable<T> queryable, IPaginateRequest<T> request);
        R Paginate<R>(IQueryable<T> queryable, IPaginateRequest<T> request)
            where R : IPaginateResponse<T>;
    }
}
