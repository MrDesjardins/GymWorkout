using WorkoutPlanner.BusinessLogic;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.Definitions;

namespace WorkoutPlanner.Database
{
    public class ImpersonateUserProvider : IUserProvider
    {
        private readonly ICurrentUser _currentUser;

        public ImpersonateUserProvider(ICurrentUser userProfile)
        {
            _currentUser = userProfile;
        }

        #region Implementation of IUserProvider

        public ICurrentUser Account
        {
            get { return _currentUser; }
            private set { }
        }

        #endregion
    }
}