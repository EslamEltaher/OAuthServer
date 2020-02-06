using Microsoft.EntityFrameworkCore;
using OAuthServer.Authorization.Models;
using OAuthServer.Authorization.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.EntityFramework.Repositories
{
    public class EFConsentRepository : IConsentRepository
    {
        private readonly IAuthorizationContext _authorizationContext;

        public EFConsentRepository(IAuthorizationContext authorizationContext)
        {
            _authorizationContext = authorizationContext;
        }
        public void AddConsent(Consent consent)
        {
            _authorizationContext.Consents.Add(consent);
        }

        public async Task<Consent> GetUserConsentByClientId(string client_id, string user_id)
        {
            return await _authorizationContext
                .Consents
                .FirstOrDefaultAsync(c => c.Client_Id == client_id && c.User_Id == user_id);
        }
    }
}
