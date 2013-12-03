using System;
using System.Data.Entity;
using System.Reflection;
using DataAccessLayer.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;


namespace WorkoutPlanner.Tests.Others
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void TestWorkoutAddOrUpdateMethod()
        //{
        //    //Arrange
        //    var workout = new Workout();
        //    var dbSet = new FilteredDbSet<Workout>(new DbContext("DefaultConnection"), null, null);

        //    //Act
        //    AddOrUpdate(dbSet, workout);

        //    //Assert

        //}

        //public void AddOrUpdate<TEntity>( IDbSet<TEntity> set, params TEntity[] entities) where TEntity : class
        //{
        //    var set1 = set as DbSet<TEntity>;
        //    if (set1 != null)
        //    {
        //        System.Data.Entity.Migrations.IDbSetExtensions.AddOrUpdate(set, entities);
        //    }
        //    else
        //    {
        //        Type type = set.GetType();
        //        MethodInfo method = type.GetMethod("AddOrUpdate");

        //        if (method == null)
        //            throw new Exception("");
        //        var data = new object[entities.Length];
        //        for(int i=0;i<entities.Length;i++)
        //        {
        //            data[i] = entities[i];
        //        }
        //        method.Invoke(set, data);
        //    }
        //}

    }
}
