using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiDataSharp
{
    public static class SortConstants
    {
        public const string SORT_ASCENDING = "asc";
        public const string SORT_DESCENDING = "desc";

        public static MethodInfo OrderByDescendingMethod = typeof(Queryable).GetMethods()
            .FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);

        public static MethodInfo OrderByMethod = typeof(Queryable).GetMethods()
            .FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);

        public static MethodInfo ThenByDescendingMethod = typeof(Queryable).GetMethods()
            .FirstOrDefault(m => m.Name == "ThenByDescending" && m.GetParameters().Length == 2);

        public static MethodInfo ThenByMethod = typeof(Queryable).GetMethods()
            .FirstOrDefault(m => m.Name == "ThenBy" && m.GetParameters().Length == 2);

        public static Regex SortOrderRegex = new Regex(
            @$"({SORT_DESCENDING}|{SORT_ASCENDING})\(([A-z0-9_]+)\)"
        );
    }
}
