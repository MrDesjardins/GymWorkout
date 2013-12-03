using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;

namespace WorkoutPlanner.Repositories.Definitions
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        UserProfile GetByUserName(string toLower);
    }
}