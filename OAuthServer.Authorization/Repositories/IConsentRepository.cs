using OAuthServer.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.Repositories
{
    public interface IConsentRepository
    {
        Task<Consent> GetUserConsentByClientId(string client_id, string user_id);
        void AddConsent(Consent consent);
    }
}
