using Microsoft.EntityFrameworkCore;
using OAuthServer.Authorization.Models;
using OAuthServer.Authorization.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.EntityFramework.Repositories
{
    public class EFConsentRepository<TUser> : IConsentRepository<TUser> where TUser : class, IResourceOwner
    {
        private readonly AuthorizationContext<TUser> _authorizationContext;

        public EFConsentRepository(AuthorizationContext<TUser> authorizationContext)
        {
            _authorizationContext = authorizationContext;
        }
        public void AddConsent(Consent<TUser> consent)
        {
            _authorizationContext.Consents.Add(consent);
        }

        public async Task<Consent<TUser>> GetUserConsentByClientId(string client_id, string user_id)
        {
            return await _authorizationContext
                .Consents
                .FirstOrDefaultAsync(c => c.Client_Id == client_id && c.User_Id == user_id);
        }
    }
}
