using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCircus.web.Models
{
    public class UserModel
    {
     
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public UserModel(string email, string name, string password)
        {
            UserName = name;
            Password = password;
            Email = email;
        }
    }
}
