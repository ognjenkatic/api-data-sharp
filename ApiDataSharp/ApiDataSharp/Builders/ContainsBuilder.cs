using ApiDataSharp.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ApiDataSharp.Builders
{
    public class ContainsBuilder : BaseBuilder
    {
        public override string GetSupportedOperator() => "contains";

        protected override Expression<Func<T, bool>> BuildExpressionFromProperty<T>(DataSharpProperty property)
        {
            var pex = Expression.Parameter(typeof(T));
            var prop = Expression.Property(pex, property.Name);

            var vl = Expression.Constant(property.Value, typeof(string));
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            var call = Expression.Call(prop, method, vl);
            return Expression.Lambda<Func<T, bool>>(call, pex);
        }
    }
}
