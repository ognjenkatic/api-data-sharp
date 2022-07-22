using ApiDataSharp.Builders;
using ApiDataSharp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiDataSharp
{
    public class ExpressionBuilderRegistry
    {
        private readonly IEnumerable<IDataSharpExpressionBuilder> _expressionBuilders;
        public readonly Regex FilterPropertyRegex;
        public ExpressionBuilderRegistry(IEnumerable<IDataSharpExpressionBuilder> builders)
        {
            _expressionBuilders = builders;
            var operators = builders.Select(a => a.GetSupportedOperator());

            var operatorFilterString = string.Join("|", operators);
            FilterPropertyRegex = new Regex(@$"({operatorFilterString})\(([A-z0-9_]+):(.+)\)");
        }

        public IDataSharpExpressionBuilder GetBuilder(string optor)
        {
            var builder = _expressionBuilders.Where(a => a.GetSupportedOperator() == optor).FirstOrDefault();
            if (builder == null)
                throw new UnsupportedOperatorException(optor);

            return builder;
        }
    }
}
