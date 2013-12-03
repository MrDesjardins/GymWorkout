using System;

namespace BusinessLogic.Sessions
{
    [Serializable]
    public class UserSessionDTO
    {
        public int UserId { get; set; }
        public string Language { get; set; }
    }
}