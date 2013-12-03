namespace BusinessLogic
{
    public interface IUserProvider
    {
        Model.Definitions.ICurrentUser Account { get; }
    }
}