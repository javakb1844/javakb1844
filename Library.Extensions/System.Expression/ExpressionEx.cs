﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Library.Extensions
{
    public static class ExpressionEx
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod =
        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod =
        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
        private static MethodInfo listContainsMethod = typeof(List<object>).GetMethod("Contains");
        #region NavigationProperty
        public static Expression<Func<T, bool>> GetExpression<T, U>(IList<GenericFilter> filters, params string[] nestedProperty)
        {
            if (filters == null || filters.Count == 0) return null;
            ParameterExpression param = Expression.Parameter(typeof(T), "t");

            Expression exp = null;
            if (filters.Count == 1)
            {
                exp = GetExpression<T, U>(param, filters[0], nestedProperty);
            }
            else if (filters.Count == 2)
            {
                exp = GetExpression<T, U>(param, filters[0], filters[1], nestedProperty);
            }
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];
                    if (exp == null)
                    {
                        exp = GetExpression<T, U>(param, filters[0], filters[1], nestedProperty);
                    }
                    else
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T, U>(param, filters[0], filters[1], nestedProperty));
                    }

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T, U>(param, filters[0], nestedProperty));
                        filters.RemoveAt(0);
                    }
                }
            }
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }
        private static BinaryExpression GetExpression<T, U>(ParameterExpression param, GenericFilter filter1, GenericFilter filter2, params string[] nestedProperty)
        {
            Expression bin1 = GetExpression<T, U>(param, filter1, nestedProperty);
            Expression bin2 = GetExpression<T, U>(param, filter2, nestedProperty);

            return Expression.AndAlso(bin1, bin2);
        }

        private static Expression GetExpression<T, U>(ParameterExpression param, GenericFilter filter, params string[] nestedPropety)
        {

            PropertyInfo pro = null;
            Type propertyType = null;
            Expression left = null;
            MemberExpression member = null;
            //ParameterExpression navParam = null;
            var nested = nestedPropety.Where(aa => aa == filter.PropertyName).FirstOrDefault();

            if (nested != null)
            {
                //navParam = Expression.Parameter(typeof(U), "ex");
                left = Expression.Property(param, typeof(T).GetProperty(typeof(U).Name));
                left = Expression.Property(left, typeof(U).GetProperty(nested));
                pro = typeof(U).GetProperty(nested);
                propertyType = pro.PropertyType;
                // member = Expression.MakeMemberAccess(navParam, pro);

            }
            else
            {

                pro = typeof(T).GetProperty(filter.PropertyName);
                propertyType = pro.PropertyType;
                member = Expression.MakeMemberAccess(param, pro);
            }




            var targetType = TypeX.IsNullableType(pro.PropertyType) ? Nullable.GetUnderlyingType(pro.PropertyType) : pro.PropertyType;
            var propertyVal = Convert.ChangeType(filter.Value, targetType);

            ConstantExpression constant = Expression.Constant(propertyVal, targetType);
            if (nested != null)
            {
                switch (filter.Operation)
                {
                    case Op.Equals:
                        return Expression.Equal(left, constant);

                    case Op.GreaterThan:
                        return Expression.GreaterThan(left, constant);

                    case Op.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(left, constant);

                    case Op.LessThan:
                        return Expression.LessThan(left, constant);

                    case Op.LessThanOrEqual:
                        return Expression.LessThanOrEqual(left, constant);

                    case Op.Contains:
                        return Expression.Call(left, containsMethod, constant);

                    case Op.StartsWith:
                        return Expression.Call(left, startsWithMethod, constant);

                    case Op.EndsWith:
                        return Expression.Call(left, endsWithMethod, constant);
                    case Op.NotEqual:
                        return Expression.NotEqual(left, constant);

                }
            }
            else
            {
                switch (filter.Operation)
                {
                    case Op.Equals:
                        return Expression.Equal(member, constant);

                    case Op.GreaterThan:
                        return Expression.GreaterThan(member, constant);

                    case Op.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(member, constant);

                    case Op.LessThan:
                        return Expression.LessThan(member, constant);

                    case Op.LessThanOrEqual:
                        return Expression.LessThanOrEqual(member, constant);

                    case Op.Contains:
                        return Expression.Call(member, containsMethod, constant);

                    case Op.StartsWith:
                        return Expression.Call(member, startsWithMethod, constant);

                    case Op.EndsWith:
                        return Expression.Call(member, endsWithMethod, constant);
                    case Op.NotEqual:
                        return Expression.NotEqual(member, constant);
                }
            }


            return null;

        }
        #endregion
        #region WithoutNavigationProperty
        public static Expression<Func<T, bool>> GetExpression<T>(IList<GenericFilter> filters)
        {
            if (filters == null || filters.Count == 0) return null;
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;
            if (filters.Count == 1)
            {
                exp = GetExpression<T>(param, filters[0]);
            }
            else if (filters.Count == 2)
            {
                exp = GetExpression<T>(param, filters[0], filters[1]);
            }
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];
                    if (exp == null)
                    {
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    }
                    else
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));
                    }

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }
        private static BinaryExpression GetExpression<T>(ParameterExpression param, GenericFilter filter1, GenericFilter filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);
            return Expression.AndAlso(bin1, bin2);
        }

        private static Expression GetExpression<T>(ParameterExpression param, GenericFilter filter)
        {
            var pro = typeof(T).GetProperty(filter.PropertyName);
            var propertyType = pro.PropertyType;
            MemberExpression member = Expression.MakeMemberAccess(param, pro);

            var targetType = TypeX.IsNullableType(pro.PropertyType) ? Nullable.GetUnderlyingType(pro.PropertyType) : pro.PropertyType;
            var propertyVal = Convert.ChangeType(filter.Value, targetType);
            //MemberExpression member = Expression.Property(param, filter.PropertyName);

            ConstantExpression constant = Expression.Constant(propertyVal);
            #region Switches
            switch (filter.Operation)
            {
                case Op.Equals:
                    return Expression.Equal(member, constant);

                case Op.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case Op.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Op.LessThan:
                    return Expression.LessThan(member, constant);

                case Op.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case Op.Contains:
                    return Expression.Call(member, containsMethod, constant);

                case Op.StartsWith:
                    return Expression.Call(member, startsWithMethod, constant);

                case Op.EndsWith:
                    return Expression.Call(member, endsWithMethod, constant);
                case Op.NotEqual:
                    return Expression.NotEqual(member, constant);

            }
            #endregion


            return null;

        }

        #endregion
        #region ListWithoutNavigation

        public static Expression<Func<T, bool>> In<T, TValue>(string PropertyName, List<TValue> values)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            var pro = typeof(T).GetProperty(PropertyName);
            var propertyType = pro.PropertyType;
            MemberExpression property = Expression.MakeMemberAccess(param, pro);
            var micontain = typeof(List<TValue>).GetMethod("Contains");
            var mc = Expression.Call(Expression.Constant(values), micontain, property);
            return Expression.Lambda<Func<T, bool>>(mc, param);
        }

        #endregion
    }





    #region FilterClass
    public class GenericFilter
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public Op Operation { get; set; }
    }



    public enum Op
    {
        Equals = 1,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith,
        NotEqual
    }
    #endregion
    
}
