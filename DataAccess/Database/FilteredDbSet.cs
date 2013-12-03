using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace WorkoutPlanner.Database
{
    public class FilteredDbSet<TEntity> : IDbSet<TEntity>, IOrderedQueryable<TEntity>, IListSource
        where TEntity : class
    {
        private readonly DbSet<TEntity> _set;
        private readonly Action<TEntity> _initializeEntity;
        private readonly Expression<Func<TEntity, bool>> _filter;

        public FilteredDbSet(DbContext context, Expression<Func<TEntity, bool>> filter, Action<TEntity> initializeEntity)
            : this(context.Set<TEntity>(), filter, initializeEntity)
        {
        }


        public IQueryable<TEntity> Include(string path)
        {
            return _set.Include(path).Where(_filter).AsQueryable();
        }

        private FilteredDbSet(DbSet<TEntity> set, Expression<Func<TEntity, bool>> filter, Action<TEntity> initializeEntity)
        {
            _set = set;
            _filter = filter;
            _initializeEntity = initializeEntity;
        }



        /// <summary>
        /// Return the original set without any Include, Where, etc.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Unfiltered()
        {
            return _set;
        }

        public TEntity Add(TEntity entity)
        {
            DoInitializeEntity(entity);
            return _set.Add(entity);
        }

        public TEntity Attach(TEntity entity)
        {
            DoInitializeEntity(entity);
            return _set.Attach(entity);
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            var entity = _set.Create<TDerivedEntity>();
            DoInitializeEntity(entity);
            return entity;
        }

        public TEntity Create()
        {
            var entity = _set.Create();
            DoInitializeEntity(entity);
            return entity;
        }

        public TEntity Find(params object[] keyValues)
        {
            var entity = _set.Find(keyValues);
            if (entity == null)
                return null;

            
            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            return _set.Remove(entity);
        }


        public ObservableCollection<TEntity> Local
        {
            get { return _set.Local; }
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _set.Where(_filter).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _set.Where(_filter).GetEnumerator();
        }

        Type IQueryable.ElementType
        {
            get { return typeof(TEntity); }
        }

        Expression IQueryable.Expression
        {
            get
            {
                return _set.Where(_filter).Expression;
            }
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return _set.AsQueryable().Provider;
            }
        }

        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        IList IListSource.GetList()
        {
            throw new InvalidOperationException();
        }

        void DoInitializeEntity(TEntity entity)
        {
            if (_initializeEntity != null)
                _initializeEntity(entity);
        }

        public DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return _set.SqlQuery(sql, parameters);
        }
    }
}