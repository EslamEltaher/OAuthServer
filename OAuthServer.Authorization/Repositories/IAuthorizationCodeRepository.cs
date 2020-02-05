using OAuthServer.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.Repositories
{
    public interface IAuthorizationCodeRepository
    {
        Task<AuthorizationCode> GetAuthorizationCodeByUserId(string client_id, string user_id);
        Task<AuthorizationCode> GetAuthorizationCodeByCode(string code);
        void AddAuthorizationCode(AuthorizationCode code);
        void RemoveRange(IEnumerable<AuthorizationCode> codes);
        void InvalidateCode(AuthorizationCode code);
    }
}
