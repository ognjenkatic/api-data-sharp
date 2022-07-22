using ApiDataSharp.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ApiDataSharp.Builders
{
    public abstract class BaseBuilder: IDataSharpExpressionBuilder
    {
        protected abstract Expression<Func<T,bool>> BuildExpressionFromProperty<T>(DataSharpProperty property);

        public Expression<Func<T, bool>> Build<T>(string propertyName, string propertyValue)
        {
            var propertyInfo = typeof(T).GetProperty(
                propertyName,
                BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

            var prtype = propertyInfo.PropertyType;
            var cverted = Convert.ChangeType(propertyValue, prtype);

            var property = new DataSharpProperty
            {
                Name = propertyInfo.Name,
                Type = propertyInfo.PropertyType,
                Value = cverted
            };

            return BuildExpressionFromProperty<T>(property);
        }
        public abstract string GetSupportedOperator();
    }
}
