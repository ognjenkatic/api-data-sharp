using ApiDataSharp.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ApiDataSharp.Builders
{
    public class StartsWithBuilder : BaseBuilder
    {
        public override string GetSupportedOperator() => "starts_with";

        protected override Expression<Func<T, bool>> BuildExpressionFromProperty<T>(DataSharpProperty property)
        {
            var pex = Expression.Parameter(typeof(T));
            var prop = Expression.Property(pex, property.Name);

            var vl = Expression.Constant(property.Value, typeof(string));
            var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

            var call = Expression.Call(prop, method, vl);
            return Expression.Lambda<Func<T, bool>>(call, pex);
        }
    }
}
