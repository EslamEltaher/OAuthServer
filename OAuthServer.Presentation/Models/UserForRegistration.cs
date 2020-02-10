using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthServer.Presentation.Models
{
    public class UserForRegistration
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Fullname { get; set; }
        public bool IsDeveloper { get; set; }
    }
}
