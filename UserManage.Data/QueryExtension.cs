using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UserManage.Base;

namespace UserManage.Data
{
    public static class QueryExtension
    {
        /// <summary>
        /// q.Where(a => a.IsValid == false)
        /// </summary>
        /// <typeparam name="TEntity">必须含有 IsValid 属性</typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> FilterDeleted<TEntity>(this IQueryable<TEntity> q)
        {
            Type entityType = typeof(TEntity);

            PropertyInfo prop_IsDeleted = entityType.GetProperty("IsValid");

            if (prop_IsDeleted == null)
                throw new ArgumentException("实体类型未定义 IsValid 属性");

            ParameterExpression parameter = Expression.Parameter(entityType, "a");
            Expression prop = Expression.Property(parameter, prop_IsDeleted);

            Expression falseValue = Expression.Constant(false, prop_IsDeleted.PropertyType);

            Expression lambdaBody = Expression.Equal(prop, falseValue);

            Expression<Func<TEntity, bool>> predicate = Expression.Lambda<Func<TEntity, bool>>(lambdaBody, parameter);

            return q.Where(predicate);
        }
        /// <summary>
        /// q.Where(a => a.IsEnabled == true)
        /// </summary>
        /// <typeparam name="TEntity">必须含有 IsEnabled 属性</typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> FilterDisabled<TEntity>(this IQueryable<TEntity> q)
        {
            Type entityType = typeof(TEntity);

            PropertyInfo prop_IsEnabled = entityType.GetProperty("IsEnabled");

            if (prop_IsEnabled == null)
                throw new ArgumentException("实体类型未定义 IsEnabled 属性");

            ParameterExpression parameter = Expression.Parameter(entityType, "a");
            Expression prop = Expression.Property(parameter, prop_IsEnabled);

            Expression trueValue = Expression.Constant(true, prop_IsEnabled.PropertyType);

            Expression lambdaBody = Expression.Equal(prop, trueValue);

            Expression<Func<TEntity, bool>> predicate = Expression.Lambda<Func<TEntity, bool>>(lambdaBody, parameter);

            return q.Where(predicate);
        }
        /// <summary>
        /// q.Where(a => a.IsValid == false &amp;&amp; a.IsEnabled == true)
        /// </summary>
        /// <typeparam name="T">必须含有 IsValid 和 IsEnabled 属性</typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> FilterDeletedAndDisabled<TEntity>(this IQueryable<TEntity> q)
        {
            return q.FilterDeleted().FilterDisabled();
        }

        public static PagedData<T> TakePageData<T>(this IQueryable<T> q, Pagination page)
        {
            PagedData<T> pageData = page.ToPagedData<T>();

            pageData.DataList = q.TakePage(page.Page, page.PageSize).ToList();
            pageData.TotalCount = q.Count();

            return pageData;
        }
    }
}
