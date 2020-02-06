using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuthServer.Authorization.Models;

namespace OAuthServer.Authorization.Repositories
{
    public class FakeAuthorizationCodeRepository<TUser> : IAuthorizationCodeRepository<TUser> where TUser : IResourceOwner
    {
        public static List<AuthorizationCode<TUser>> AuthorizationCodes { get; }
        static FakeAuthorizationCodeRepository()
        {
            AuthorizationCodes = new List<AuthorizationCode<TUser>>();
        }


        public void AddAuthorizationCode(AuthorizationCode<TUser> code)
        {
            AuthorizationCodes.Add(code);
        }

        public async Task<AuthorizationCode<TUser>> GetAuthorizationCodeByUserId(string client_id, string user_id)
        {
            var code = AuthorizationCodes.FirstOrDefault(c => c.Consent.Client_Id == client_id 
                && c.Consent.User_Id == user_id
                && c.Expired == false
                && c.Expiry >= DateTime.Now);

            return await Task.FromResult(code);
        }

        public void RemoveRange(IEnumerable<AuthorizationCode<TUser>> codes)
        {
            foreach (var code in codes)
            {
                AuthorizationCodes.Remove(code);
            }
        }

        public async Task<AuthorizationCode<TUser>> GetAuthorizationCodeByCode(string code)
        {
            var authorization_code = AuthorizationCodes.FirstOrDefault(c => c.Code == code);
            return await Task.FromResult(authorization_code);
        }

        public void InvalidateCode(AuthorizationCode<TUser> code)
        {
            code.Expired = true;
            //update code
        }
    }
}
