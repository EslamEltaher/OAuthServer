using Microsoft.EntityFrameworkCore;
using OAuthServer.Authorization.Models;
using OAuthServer.Authorization.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.EntityFramework.Repositories
{
    public class EFClientRepository<TUser> : IClientRepository/*<TUser>*/ where TUser : class, IResourceOwner
    {
        private readonly AuthorizationContext<TUser> _authorizationContext;

        public EFClientRepository(AuthorizationContext<TUser> authorizationContext)
        {
            _authorizationContext = authorizationContext;
        }
        public void AddClient(Client client)
        {
            _authorizationContext.Clients.Add(client);
        }

        public async Task<Client> GetClientById(string client_id)
        {
            return await _authorizationContext
                .Clients
                .FirstOrDefaultAsync(c => c.Client_Id == client_id);
        }
    }
}
