using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Todo.Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        T Update(T entity);
        T Add(T entity);
        bool Delete(T entity);

        IEnumerable<T> GetAll();
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);

    }
}