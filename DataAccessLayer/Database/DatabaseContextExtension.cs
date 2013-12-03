using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DataAccessLayer.Database
{
    public static class DatabaseContextExtension
    {

        public static void AddRange<TEntity>(this DbSet<TEntity> dbset, IEnumerable<TEntity> list) where TEntity : class
        {
            foreach (var item in list)
            {
                dbset.Add(item);
            }

        }

        public static void AddOrUpdateRange<TEntity>(this DbSet<TEntity> dbset, IEnumerable<TEntity> list) where TEntity : class
        {
            foreach (var item in list)
            {
                dbset.AddOrUpdate(item);
            }

        }

    }
}