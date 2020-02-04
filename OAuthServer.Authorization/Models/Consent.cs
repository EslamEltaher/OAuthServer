using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Authorization.Models
{
    public class Consent
    {
        public string Client_Id { get; set; }
        public Client client { get; set; }

        public string User_Id { get; set; }
        public string Scope { get; set; }

        //public bool Granted { get; set; }
    }
}
