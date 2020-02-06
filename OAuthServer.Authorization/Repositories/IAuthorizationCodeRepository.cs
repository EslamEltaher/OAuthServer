using OAuthServer.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.Repositories
{
    public interface IAuthorizationCodeRepository<TUser> where TUser : IResourceOwner
    {
        Task<AuthorizationCode<TUser>> GetAuthorizationCodeByUserId(string client_id, string user_id);
        Task<AuthorizationCode<TUser>> GetAuthorizationCodeByCode(string code);
        void AddAuthorizationCode(AuthorizationCode<TUser> code);
        void RemoveRange(IEnumerable<AuthorizationCode<TUser>> codes);
        void InvalidateCode(AuthorizationCode<TUser> code);
    }
}
