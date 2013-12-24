using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Model.Definitions;

namespace Model
{
    public class ApplicationUser : IdentityUser, ICurrentUser
    {
        public ApplicationUser()
        {
            Email = "";
            Language = "";
        }
        public string UserId {
            get { return base.Id; }
            set{} 
        }
        public string Email { get; set; } 
        public string Language { get; set; } 
        
    }
}
