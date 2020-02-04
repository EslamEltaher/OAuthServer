using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuthServer.Authorization.Models;

namespace OAuthServer.Authorization.Repositories
{
    public class FakeClientRepository : IClientRepository
    {
        private static readonly List<Client> clients = new List<Client>();

        public void AddClient(Client client)
        {
            clients.Add(client);
        }

        public async Task<Client> GetClientById(string client_id)
        {
            return await Task.FromResult(clients.FirstOrDefault(c => c.Client_Id == client_id));
        }
    }
}
