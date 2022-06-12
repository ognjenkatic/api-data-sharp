﻿using ApiDataSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiDataSharp.Models
{
    public class PaginateRequest<T> : IPaginateRequest<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
