using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Database;
using Model.Definitions;
using Shared;
using System.Data.Entity;

namespace DataAccessLayer.Repositories.Base
{
    public abstract class BaseRepository<T>:IRepository<T> where T : class
    {
        protected IDatabaseContext DatabaseContext { get; private set; }

        protected BaseRepository(IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public abstract IQueryable<T> GetAll();
        public abstract T Get(int id);
        public abstract int Insert(T entity);
        public abstract int Update(T entity);
        public int Update(T entity, Expression<Func<T, object>>[] properties)
        {
            DatabaseContext.Entry(entity).State = System.Data.Entity.EntityState.Unchanged;
            foreach (var property in properties)
            {
                var propertyName = LambdaUtilities.GetExpressionText(property);
                DatabaseContext.Entry(entity).Property(propertyName).IsModified = true;
            }
            return DatabaseContext.SaveChangesWithoutValidation();
        }

        public abstract int Delete(T entity);
        public abstract IQueryable<T> GetAllFromDatabase();
    }
}