using ApiDataSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDataSharp.Models
{
    public class SortRequest<T> : ISortRequest<T>
    {
        public string SortBy { get; set; }
    }
}
