namespace BusinessLogic.Validations
{
    public interface IBaseError
    {
        string PropertyName { get; }
        string PropertyExceptionMessage { get; }
    }
}