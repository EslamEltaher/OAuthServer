using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Authorization.Models
{
    public class Client
    {
        public string Client_Id { get; set; }
        public string Client_Secret { get; set; }

        public string Redirect_Uri { get; set; }
    }
}
