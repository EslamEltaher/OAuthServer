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
    public class EFClientRepository<TUser, TContext> : IClientRepository/*<TUser>*/ 
        where TUser : class, IResourceOwner
        where TContext : AuthorizationContext<TUser>
    {
        private readonly AuthorizationContext<TUser> _authorizationContext;

        public EFClientRepository(AuthorizationContext<TUser> authorizationContext)
        {
            _authorizationContext = authorizationContext;
        }
        public EFClientRepository(TContext authorizationContext)
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

        public async Task<IEnumerable<Client>> GetClientsForDeveloper(string developer_user_id)
        {
            return await _authorizationContext
                .Clients
                .Where(c => c.Developer_Id == developer_user_id)
                .ToListAsync();
        }
    }
}
