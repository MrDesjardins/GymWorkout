namespace BusinessLogic.Sessions
{
    public interface ISessionHandler
    {
        void SaveUser(UserSessionDTO dto);
        UserSessionDTO GetUser();
        void RemoveUser();
    }
}