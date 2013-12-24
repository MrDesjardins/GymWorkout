using System;

namespace BusinessLogic.Sessions
{
    [Serializable]
    public class UserSessionDTO
    {
        public string UserId { get; set; }
        public string Language { get; set; }
    }
}