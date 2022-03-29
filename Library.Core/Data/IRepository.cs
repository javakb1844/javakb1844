using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Data
{
    /// <summary>
    /// Generic Repository Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        long GetNextId();

        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);

        T GetById(long id);
        T Get(Expression<Func<T, bool>> filter = null, string includingProperties = null);

        int Count();
        int Count(Expression<Func<T, bool>> predeciate);


        //T GetById(int id);
        IQueryable<T> GetDbSet();
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderyby = null, string includingProperties = null);
        IQueryable<T> Search(Expression<Func<T, bool>> predicate);



        int BatchDelete(Expression<Func<T, bool>> wherePredicate);
        void BatchDelete(List<long> ids);
        int BatchUpdate(Expression<Func<T, bool>> wherePredicate, Expression<Func<T, T>> updateExpression);

        IQueryable<T> GetFirstPage();
        IQueryable<T> GetNextPage(int currentPageNo, out int newPageNo);
        IQueryable<T> GetPreviousPage(int currentPageNo, out int newPageNo);
        IQueryable<T> GetLastPage(out int lastPageNo);

        IQueryable<T> GetFirstPage(string sortPropertyName, bool ascending = true);
        IQueryable<T> GetNextPage(int currentPageNo, out int newPageNo, string sortPropertyName, bool ascending = true);
        IQueryable<T> GetPreviousPage(int currentPageNo, out int newPageNo, string sortPropertyName, bool ascending = true);
        IQueryable<T> GetLastPage(out int lastPageNo, string sortPropertyName, bool ascending = true);
    }
}
