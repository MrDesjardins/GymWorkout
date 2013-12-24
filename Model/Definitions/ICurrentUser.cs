using System;

namespace Model.Definitions
{
    public interface ICurrentUser
    {
        string UserId { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string Language { get; set; }
    }
}