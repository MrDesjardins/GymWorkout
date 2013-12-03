namespace Model.Definitions
{
    public interface ICurrentUser
    {
        int UserId { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string Language { get; set; }
    }
}