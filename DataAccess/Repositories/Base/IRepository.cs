using System.Linq;

namespace WorkoutPlanner.Repositories.Base
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(int id);
        int Insert(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}