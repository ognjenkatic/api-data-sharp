using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDataSharp.Interfaces
{
    public interface ISortRequest<T> : IDataSharpRequest
    {
        public string SortBy { get; set; }
    }
}
