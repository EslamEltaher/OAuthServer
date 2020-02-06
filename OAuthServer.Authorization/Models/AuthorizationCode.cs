using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Authorization.Models
{
    public class AuthorizationCode<TUser> where TUser : IResourceOwner
    {
        public AuthorizationCode(string code, Consent<TUser> consent, DateTime expiry)
        {
            Code = code;
            Consent = consent;
            Expiry = expiry;
        }

        public Consent<TUser> Consent { get; set; }
        public string Code { get; set; }

        public DateTime Expiry { get; set; }
        public bool Expired { get; set; }

    }
}
