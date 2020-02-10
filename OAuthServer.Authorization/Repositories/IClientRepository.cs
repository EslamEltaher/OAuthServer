using OAuthServer.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.Repositories
{
    public interface IClientRepository
    {
        //IEnumerable<Client> GetAllClients();

        Task<Client> GetClientById(string client_id);
        Task<IEnumerable<Client>> GetClientsForDeveloper(string developer_user_id);

        void AddClient(Client client);
    }
}
