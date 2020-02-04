using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Authorization.Models
{
    public class AuthorizationCode
    {
        public AuthorizationCode(string code, Consent consent, DateTime expiry)
        {
            Code = code;
            Consent = consent;
            Expiry = expiry;
        }

        public Consent Consent { get; set; }
        public string Code { get; set; }

        public DateTime Expiry { get; set; }
        public bool Expired { get; set; }

    }
}
