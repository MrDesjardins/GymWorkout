using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace WorkoutPlanner.Migrations
{
    public static class DbSetExtension
    {

        public static void AddOrUpdate<TEntity>(this IDbSet<TEntity> set, params TEntity[] entities) where TEntity : class
        {
            var set1 = set as DbSet<TEntity>;
            if (set1 != null)
            {
                System.Data.Entity.Migrations.DbSetMigrationsExtensions.AddOrUpdate(set,entities);
            }
            else
            {
                Type type = set.GetType();
                MethodInfo method = type.GetMethod("AddOrUpdate");

                if (method == null)
                    throw new Exception("");
                var data = new object[entities.Length];
                for (int i = 0; i < entities.Length; i++)
                {
                    data[i] = entities[i];
                }
                method.Invoke(set, data);
            }
        }


        
    }
}