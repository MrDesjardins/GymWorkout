using System;
using System.Data;
using System.Linq;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.Base;
using DataAccessLayer.Repositories.Definitions;
using Model;

namespace DataAccessLayer.Repositories.Implementations
{
    public class MuscleRepository : BaseRepository<Muscle>, IMuscleRepository
    {
        public MuscleRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
            
        }

        public override IQueryable<Muscle> GetAll()
        {
            return DatabaseContext.Set<Muscle>();
        }

        public override Muscle Get(int id)
        {
            return DatabaseContext.Set<Muscle>().Single(x => x.Id == id);
        }

        public override int Insert(Muscle entity)
        {
            DatabaseContext.Insert(entity);
            return DatabaseContext.SaveChanges();   
        }

        public override int Update(Muscle entity)
        {
            var entityFromDatbaseContext = DatabaseContext.Update(entity);
            int saveValue = DatabaseContext.SaveChanges();
            return saveValue;
        }

        public override int Delete(Muscle entity)
        {
            DatabaseContext.Delete(entity);
            return 1;
        }

        public override IQueryable<Muscle> GetAllFromDatabase()
        {
            return DatabaseContext.Set<Muscle>().AsNoTracking();
        }
    }
}