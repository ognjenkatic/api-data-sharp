using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDataSharp.Interfaces
{
    public interface IPaginateResponse<T>
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalPages { get; set; }
        int TotalItems { get; set; }
        bool HasNextPage { get; set; }
        bool HasPreviousPage { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
