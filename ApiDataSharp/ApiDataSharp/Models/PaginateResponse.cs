using ApiDataSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDataSharp.Models
{
    public class PaginateResponse<T> : IPaginateResponse<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
