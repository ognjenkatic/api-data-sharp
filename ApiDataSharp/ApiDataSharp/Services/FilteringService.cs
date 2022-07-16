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
                Expression<Func<T, bool>> whereFunc = default;

                whereFunc = opet switch
                {
                    "gt" => GetGreaterThanExpression<T>(param, key, value),
                    "lt" => GetLessThanExpression<T>(param, key, value),
                    "gte" => GetGreaterThanOrEqualExpression<T>(param, key, value),
                    "lte" => GetLessThanOrEqualExpression<T>(param, key, value),
                    "contains" => GetContainsExpression<T>(param, key, value),
                    "starts_with" => GetStartsWithExpression<T>(param, key, value),
                    "ends_with" => GetEndsWithExpression<T>(param, key, value),
                    "equals" => GetEqualExpression<T>(param, key, value),
                    _ => throw new Exception("Unsupported type")
                };

                queryable = queryable.Where(whereFunc);
            }

            return queryable;
        }

        private static Expression<Func<T,bool>> GetEqualExpression<T>(
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
            var expr = Expression.Equal(prop, vl);
            return Expression.Lambda<Func<T, bool>>(expr, pex);
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

        private static Expression<Func<T,bool>> GetLessThanExpression<T>(
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
            var expr =  Expression.LessThan(prop, nmbr);
            return Expression.Lambda<Func<T, bool>>(expr, pex);
        }

        private static Expression<Func<T,bool>> GetGreaterThanExpression<T>(
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
            var expr = Expression.GreaterThan(prop, nmbr);
            return Expression.Lambda<Func<T, bool>>(expr, pex);
        }

        private static Expression<Func<T, bool>> GetGreaterThanOrEqualExpression<T>(
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
            var expr =  Expression.GreaterThanOrEqual(prop, nmbr);
            return Expression.Lambda<Func<T, bool>>(expr, pex);
        }

        private static Expression<Func<T, bool>> GetLessThanOrEqualExpression<T>(
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
            var expr = Expression.LessThanOrEqual(prop, nmbr);
            return Expression.Lambda<Func<T, bool>>(expr, pex);
        }
    }
}
