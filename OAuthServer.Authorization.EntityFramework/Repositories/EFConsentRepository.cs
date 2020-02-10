using Microsoft.EntityFrameworkCore;
using OAuthServer.Authorization.Models;
using OAuthServer.Authorization.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.EntityFramework.Repositories
{
    public class EFConsentRepository<TUser, TContext> : IConsentRepository<TUser> 
        where TUser : class, IResourceOwner
        where TContext : AuthorizationContext<TUser>
    {
        private readonly AuthorizationContext<TUser> _authorizationContext;

        public EFConsentRepository(AuthorizationContext<TUser> authorizationContext)
        {
            _authorizationContext = authorizationContext;
        }
        public EFConsentRepository(TContext authorizationContext)
        {
            _authorizationContext = authorizationContext;
        }
        public void AddConsent(Consent<TUser> consent)
        {
            _authorizationContext.Consents.Add(consent);
        }

        public void DeleteConsent(Consent<TUser> consent)
        {
            _authorizationContext.Consents.Remove(consent);
        }

        public async Task<Consent<TUser>> GetUserConsentByClientId(string client_id, string user_id)
        {
            return await _authorizationContext
                .Consents
                .FirstOrDefaultAsync(c => c.Client_Id == client_id && c.User_Id == user_id);
        }

        public async Task<IEnumerable<Consent<TUser>>> GetUserConsents(string user_id)
        {
            return await _authorizationContext
                .Consents
                .Include(c => c.client)
                .Where(c => c.User_Id == user_id)
                .ToListAsync();
        }
    }
}
