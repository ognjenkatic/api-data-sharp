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
    public class EqualsOperation
    {
        public EqualsOperation Parent { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class LessThanOrEquals
    {
    }
    public static class FilteringService
    {
        //api/users?filter=equals(name:brojac),lte(id:20),
        //operators: equals, lte, gte, contains, starts_with, ends_with,
        public static IQueryable<T> Filter<T>(IQueryable<T> queryable, IFilterRequest<T> request)
        {
            if (request.Filter is null)
                throw new ArgumentNullException(nameof(request.Filter));

            var filters = request.Filter.Split(',').Select(a => a.Trim());

            var parent = new EqualsOperation();

            foreach (var filter in filters)
            {
                var match = FilterConstants.FilterPropertyRegex.Match(filter);

                if (!match.Success && match.Groups.Count != 3)
                    throw new ArgumentException(
                        $"Filter by parameter does not match regex {FilterConstants.FilterPropertyRegex}"
                    );

                var opet = match.Groups[1].Value;
                var key = match.Groups[2].Value;
                var value = match.Groups[3].Value;

                var param = Expression.Parameter(typeof(T));

                if (opet == "lt")
                {
                    var expr = GetLessThanExpression<T>(param, key, value);
                    var lmbd = Expression.Lambda<Func<T, bool>>(expr, param);

                    queryable = queryable.Where(lmbd);
                    continue;
                }

                else if (opet == "gt")
                {
                    var expr = GetGreaterThanExpression<T>(param, key, value);
                    var lmbd = Expression.Lambda<Func<T, bool>>(expr, param);

                    queryable = queryable.Where(lmbd);
                    continue;
                }

                if (opet == "lte")
                {
                    var expr = GetLessThanOrEqualExpression<T>(param, key, value);
                    var lmbd = Expression.Lambda<Func<T, bool>>(expr, param);

                    queryable = queryable.Where(lmbd);
                    continue;
                }

                else if (opet == "gte")
                {
                    var expr = GetGreaterThanOrEqualExpression<T>(param, key, value);
                    var lmbd = Expression.Lambda<Func<T, bool>>(expr, param);

                    queryable = queryable.Where(lmbd);
                    continue;
                }

                else if (opet == "contains")
                {
                    var expr = GetContainsExpression<T>(param, key, value);
                    queryable = queryable.Where(expr);
                    continue;
                }

                else if (opet == "starts_with")
                {
                    var expr = GetStartsWithExpression<T>(param, key, value);
                    queryable = queryable.Where(expr);
                    continue;
                }

                else if (opet == "ends_with")
                {
                    var expr = GetEndsWithExpression<T>(param, key, value);
                    queryable = queryable.Where(expr);
                    continue;
                }

                var expression = GetEqualExpression<T>(param, key, value);
                var lambda = Expression.Lambda<Func<T, bool>>(expression, param);

                queryable = queryable.Where(lambda);
            }

            return queryable;
        }

        private static BinaryExpression GetEqualExpression<T>(
            ParameterExpression pex,
            string key,
            string value
        ) {
            var propertyInfo = typeof(T).GetProperty(
                key,
                BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

            var prop = Expression.Property(pex, propertyInfo.Name);

            var prtype = propertyInfo.PropertyType;

            var cverted = Convert.ChangeType(value, prtype);
            var vl = Expression.Constant(cverted);
            return Expression.Equal(prop, vl);
        }

        // .Where(a => a.b.Contains(const)
        private static Expression<Func<T, bool>> GetContainsExpression<T>(
            ParameterExpression pex,
            string key,
            string value
        ) {
            var propertyInfo = typeof(T).GetProperty(
                key,
                BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

            var prop = Expression.Property(pex, propertyInfo.Name);

            var prtype = propertyInfo.PropertyType;
            var cverted = Convert.ChangeType(value, prtype);
            var vl = Expression.Constant(cverted, typeof(string));
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            var call = Expression.Call(prop, method, vl);
            return Expression.Lambda<Func<T, bool>>(call, pex);
        }

        private static Expression<Func<T, bool>> GetStartsWithExpression<T>(
            ParameterExpression pex,
            string key,
            string value
        )
        {
            var propertyInfo = typeof(T).GetProperty(
                key,
                BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

            var prop = Expression.Property(pex, propertyInfo.Name);

            var prtype = propertyInfo.PropertyType;
            var cverted = Convert.ChangeType(value, prtype);
            var vl = Expression.Constant(cverted, typeof(string));
            var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

            var call = Expression.Call(prop, method, vl);
            return Expression.Lambda<Func<T, bool>>(call, pex);
        }

        private static Expression<Func<T, bool>> GetEndsWithExpression<T>(
            ParameterExpression pex,
            string key,
            string value
        )
        {
            var propertyInfo = typeof(T).GetProperty(
                key,
                BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

            var prop = Expression.Property(pex, propertyInfo.Name);

            var prtype = propertyInfo.PropertyType;
            var cverted = Convert.ChangeType(value, prtype);
            var vl = Expression.Constant(cverted, typeof(string));
            var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

            var call = Expression.Call(prop, method, vl);
            return Expression.Lambda<Func<T, bool>>(call, pex);
        }

        private static BinaryExpression GetLessThanExpression<T>(
            ParameterExpression pex,
            string key,
            string value
        ) {
            var propertyInfo = typeof(T).GetProperty(
                key,
                BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

            var prop = Expression.Property(pex, propertyInfo.Name);
            var prtype = propertyInfo.PropertyType;


            var cverted = Convert.ChangeType(value, prtype);

            var nmbr = Expression.Constant(cverted, typeof(int));
            return Expression.LessThan(prop, nmbr);
        }

        private static BinaryExpression GetGreaterThanExpression<T>(
            ParameterExpression pex,
            string key,
            string value
        )
        {
            var propertyInfo = typeof(T).GetProperty(
                key,
                BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

            var prop = Expression.Property(pex, propertyInfo.Name);
            var prtype = propertyInfo.PropertyType;
            var cverted = Convert.ChangeType(value, prtype);

            var nmbr = Expression.Constant(cverted, typeof(int));
            return Expression.GreaterThan(prop, nmbr);
        }

        private static BinaryExpression GetGreaterThanOrEqualExpression<T>(
            ParameterExpression pex,
            string key,
            string value
        )
        {
            var propertyInfo = typeof(T).GetProperty(
                key,
                BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

            var prop = Expression.Property(pex, propertyInfo.Name);
            var prtype = propertyInfo.PropertyType;
            var cverted = Convert.ChangeType(value, prtype);

            var nmbr = Expression.Constant(cverted, typeof(int));
            return Expression.GreaterThanOrEqual(prop, nmbr);
        }

        private static BinaryExpression GetLessThanOrEqualExpression<T>(
            ParameterExpression pex,
            string key,
            string value
        )
        {
            var propertyInfo = typeof(T).GetProperty(
                key,
                BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance
            );

            var prop = Expression.Property(pex, propertyInfo.Name);
            var prtype = propertyInfo.PropertyType;
            var cverted = Convert.ChangeType(value, prtype);

            var nmbr = Expression.Constant(cverted, typeof(int));
            return Expression.LessThanOrEqual(prop, nmbr);
        }
    }
}
