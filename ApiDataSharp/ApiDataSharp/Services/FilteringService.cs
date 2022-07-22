using ApiDataSharp.Builders;
using ApiDataSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
namespace ApiDataSharp.Services
{
    public class FilteringService
    {
        private readonly ExpressionBuilderRegistry _registry;

        public FilteringService(ExpressionBuilderRegistry registry)
        {
            _registry = registry;
        }

        public IQueryable<T> Filter<T>(IQueryable<T> queryable, IFilterRequest<T> request)
        {
            if (request.Filter is null)
                throw new ArgumentNullException(nameof(request.Filter));

            var filters = request.Filter.Split(',').Select(a => a.Trim());


            foreach (var filter in filters)
            {
                var match = _registry.FilterPropertyRegex.Match(filter);

                if (!match.Success && match.Groups.Count != 3)
                    throw new ArgumentException(
                        $"Filter by parameter does not match regex {_registry.FilterPropertyRegex}"
                    );

                var opet = match.Groups[1].Value;
                var key = match.Groups[2].Value;
                var value = match.Groups[3].Value;

                var param = Expression.Parameter(typeof(T));
                Expression<Func<T, bool>> whereFunc = default;

                var builder = _registry.GetBuilder(opet);
                whereFunc = builder.Build<T>(key, value);

                queryable = queryable.Where(whereFunc);
            }

            return queryable;
        }

    }
}
