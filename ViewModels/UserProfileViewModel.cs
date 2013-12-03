using System.Collections.Generic;
using ViewModels.Selectors.Implementations;

namespace ViewModels
{
    public class UserProfileViewModel   
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Language { set; get; }
        public IEnumerable<LanguageSelector> Languages { get; set; }
    }
}