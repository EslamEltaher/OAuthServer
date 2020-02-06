using Microsoft.EntityFrameworkCore;
using OAuthServer.Authorization.Models;
using OAuthServer.Authorization.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.EntityFramework.Repositories
{
    public class EFClientRepository : IClientRepository
    {
        private readonly IAuthorizationContext _authorizationContext;

        public EFClientRepository(IAuthorizationContext authorizationContext)
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
