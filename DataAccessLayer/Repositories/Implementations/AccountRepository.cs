using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.Base;
using DataAccessLayer.Repositories.Definitions;
using Model;

namespace DataAccessLayer.Repositories.Implementations
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        #region Implementation of IRepository<UserProfile>

        public override IQueryable<UserProfile> GetAll()
        {
            return DatabaseContext.Set<UserProfile>().OrderBy(d => d.UserId);
        }

        public override UserProfile Get(int id)
        {
            return DatabaseContext.Set<UserProfile>().Single(c => c.UserId == id);
        }

        public override int Insert(UserProfile entity)
        {
            DatabaseContext.Set<UserProfile>().Add(entity);

            return DatabaseContext.SaveChanges();
        }

        public override int Update(UserProfile entity)
        {
            UserProfile fromDatabase = Get(entity.UserId);
            DatabaseContext.Entry(fromDatabase).CurrentValues.SetValues(entity);
            DatabaseContext.Entry(fromDatabase).State = System.Data.Entity.EntityState.Modified;
            return DatabaseContext.SaveChanges();
        }


        public override int Delete(UserProfile entity)
        {
            DatabaseContext.Set<UserProfile>().Remove(entity);
            return DatabaseContext.SaveChanges();
        }

        public override IQueryable<UserProfile> GetAllFromDatabase()
        {
            throw new System.NotImplementedException();
        }

        public UserProfile GetByUserName(string toLower)
        {
            return DatabaseContext.Set<UserProfile>().SingleOrDefault(d => d.UserName.ToLower() == toLower);
        }

        #endregion
    }
}