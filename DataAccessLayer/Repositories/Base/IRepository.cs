using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories.Base
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(int id);
        int Insert(T entity);
        int Update(T entity);
        int Update(T entity, Expression<Func<T, object>>[] properties);
        int Delete(T entity);
        IQueryable<T> GetAllFromDatabase();
    }
}