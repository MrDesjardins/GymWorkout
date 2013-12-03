using System.Data;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.Base;
using DataAccessLayer.Repositories.Definitions;
using Model;

namespace DataAccessLayer.Repositories.Implementations
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        public override Exercise Get(int id)
        {
            return DatabaseContext.Set<Exercise>().Include(d=>d.Muscle).Single(x => x.Id == id);
        }

        public override int Insert(Exercise entity)
        {

            if (entity.Muscle != null)
            {
                if(entity.Muscle.Id == Model.BaseModel.NOT_INITIALIZED)//[P]:2013-10-10 : Should be the Constant -1
                {
                    entity.Muscle = null;
                }
                else
                {
                    var localExercise = DatabaseContext.Set<Muscle>().Local.SingleOrDefault(e => e.Id == entity.Muscle.Id);
                    if (localExercise == null)
                    {
                        DatabaseContext.Set<Muscle>().Attach(entity.Muscle);
                    }
                    entity.Muscle = DatabaseContext.Set<Muscle>().Local.SingleOrDefault(e => e.Id == entity.Muscle.Id); 
                }
              
                
            }
            DatabaseContext.Insert(entity);
            return DatabaseContext.SaveChanges();   
        }

        public override int Update(Exercise entity)
        {
            this.DatabaseContext.Update(entity);
            return DatabaseContext.SaveChanges();
        }

        public override int Delete(Exercise entity)
        {
            DatabaseContext.Delete(entity);
            return DatabaseContext.SaveChanges();
        }

        public override IQueryable<Exercise> GetAllFromDatabase()
        {
            var entities = DatabaseContext.Set<Exercise>()
                                            .Include(d=>d.Muscle)
                                            .AsNoTracking();

            return entities;
        }

        public override IQueryable<Exercise>GetAll()
        {
            var allExercise = DatabaseContext.Set<Exercise>();
            return allExercise;
        }

    }
}