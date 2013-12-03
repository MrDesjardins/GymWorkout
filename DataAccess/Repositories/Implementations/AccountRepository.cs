using System;
using System.Data;
using System.Linq;
using WorkoutPlanner.Database;
using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;
using WorkoutPlanner.Repositories.Definitions;

namespace WorkoutPlanner.Repositories.Implementations
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        #region Implementation of IRepository<UserProfile>

        public IQueryable<UserProfile> GetAll()
        {
            return DatabaseContext.Set<UserProfile>().OrderBy(d => d.UserId);
        }

        public UserProfile Get(int id)
        {
            return DatabaseContext.Set<UserProfile>().Single(c => c.UserId == id);
        }

        public int Insert(UserProfile entity)
        {
            DatabaseContext.Set<UserProfile>().Add(entity);

            return DatabaseContext.SaveChanges();
        }

        public int Update(UserProfile entity)
        {
            UserProfile fromDatabase = Get(entity.UserId);
            DatabaseContext.Entry(fromDatabase).CurrentValues.SetValues(entity);
            DatabaseContext.Entry(fromDatabase).State = EntityState.Modified;
            return DatabaseContext.SaveChanges();
        }

        public int Delete(UserProfile entity)
        {
            DatabaseContext.Set<UserProfile>().Remove(entity);
            return DatabaseContext.SaveChanges();
        }

        public UserProfile GetByUserName(string toLower)
        {
            return DatabaseContext.Set<UserProfile>().SingleOrDefault(d => d.UserName.ToLower() == toLower);
        }

        #endregion
    }
}