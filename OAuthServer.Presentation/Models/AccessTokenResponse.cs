using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthServer.Presentation.Models
{
    public class AccessTokenResponse
    {
        public string Access_Token { get; set; }
        public string Scope { get; set; }
        public string Token_Type { get; set; }
    }
}
