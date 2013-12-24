namespace DataAccessLayer
{
    public interface IUserProvider
    {
        Model.Definitions.ICurrentUser Account { get; }
    }
}