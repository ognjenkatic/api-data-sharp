using ApiDataSharp.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApiDataSharp.Builders
{
    public interface IDataSharpExpressionBuilder
    {
        string GetSupportedOperator();
        Expression<Func<T, bool>> Build<T>(string propertyName, string propertyValue);
    }
}
