using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiDataSharp
{
    public static class FilterConstants
    {
        public static Regex FilterPropertyRegex = new Regex(
            @"(lt|gt|contains|equals|lte|gte|starts_with|ends_with)\(([A-z0-9_]+):(.+)\)"
        );

        public static MethodInfo WhereMethod = typeof(Queryable).GetMethods()
            .FirstOrDefault(m => m.Name == "Where" && m.GetParameters().Length == 2);
    }
}
