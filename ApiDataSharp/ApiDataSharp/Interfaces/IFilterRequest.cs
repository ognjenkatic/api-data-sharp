using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDataSharp.Interfaces
{
    public interface IFilterRequest<T> : IDataSharpRequest
    {
        public string Filter { get; set; }
    }
}
