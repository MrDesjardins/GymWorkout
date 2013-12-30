using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.Base;
using DataAccessLayer.Repositories.Definitions;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;

namespace DataAccessLayer.Repositories.Implementations
{
    
    public class ApplicationApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationApplicationUserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        #region Implementation of IRepository<ApplicationUser>

        public override IQueryable<ApplicationUser> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public override ApplicationUser Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public override int Insert(ApplicationUser entity)
        {
            throw new System.NotImplementedException();
        }

        public override int Update(ApplicationUser entity)
        {
            throw new System.NotImplementedException();
        }


        public override int Delete(ApplicationUser entity)
        {
            throw new System.NotImplementedException();
        }

        public override IQueryable<ApplicationUser> GetAllFromDatabase()
        {
            throw new System.NotImplementedException();
        }

        public UserStore<ApplicationUser> GetUserStore()
        {
            //var db = base.DatabaseContext as DbContext;
            var db = new IdentityDbContext();
            return new UserStore<ApplicationUser>(db);
        }


        #endregion
    }
     
}