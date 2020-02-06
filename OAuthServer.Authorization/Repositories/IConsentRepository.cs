using OAuthServer.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.Repositories
{
    public interface IConsentRepository<TUser> where TUser : IResourceOwner
    {
        Task<Consent<TUser>> GetUserConsentByClientId(string client_id, string user_id);
        void AddConsent(Consent<TUser> consent);
    }
}
