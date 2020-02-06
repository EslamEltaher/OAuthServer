using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Application.Repository
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user, string password);
        Task<User> LogInUser(string username, string password);
        Task<bool> UserExists(string username);

        Task<User> GetUserById(string user_id);
    }
}
