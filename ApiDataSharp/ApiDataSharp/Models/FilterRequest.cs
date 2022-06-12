using ApiDataSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDataSharp.Models
{
    public class FilterRequest<T> : IFilterRequest<T>
    {
        public string Filter { get; set; }
    }
}
