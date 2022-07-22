using ApiDataSharp.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ApiDataSharp.Builders
{
    public class EqualsBuilder : BaseBuilder
    {
        public override string GetSupportedOperator() => "equals";

        protected override Expression<Func<T, bool>> BuildExpressionFromProperty<T>(DataSharpProperty property)
        {
            var pex = Expression.Parameter(typeof(T));
            var prop = Expression.Property(pex, property.Name);

            var vl = Expression.Constant(property.Value, typeof(string));
            var expr = Expression.Equal(prop, vl);
            return Expression.Lambda<Func<T, bool>>(expr, pex);
        }
    }
}
