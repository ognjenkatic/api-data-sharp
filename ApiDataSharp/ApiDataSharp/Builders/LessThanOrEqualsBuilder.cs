using ApiDataSharp.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ApiDataSharp.Builders
{
    public class LessThanOrEqualsBuilder : BaseBuilder
    {
        public override string GetSupportedOperator() => "lte";

        protected override Expression<Func<T, bool>> BuildExpressionFromProperty<T>(DataSharpProperty property)
        {
            var pex = Expression.Parameter(typeof(T));
            var prop = Expression.Property(pex, property.Name);

            var nmbr = Expression.Constant(property.Value, typeof(int));
            var expr = Expression.LessThanOrEqual(prop, nmbr);

            return Expression.Lambda<Func<T, bool>>(expr, pex);
        }
    }
}
