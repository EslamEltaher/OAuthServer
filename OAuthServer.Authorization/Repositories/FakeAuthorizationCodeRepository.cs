using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuthServer.Authorization.Models;

namespace OAuthServer.Authorization.Repositories
{
    public class FakeAuthorizationCodeRepository : IAuthorizationCodeRepository
    {
        public static List<AuthorizationCode> AuthorizationCodes { get; }
        static FakeAuthorizationCodeRepository()
        {
            AuthorizationCodes = new List<AuthorizationCode>();
        }


        public void AddAuthorizationCode(AuthorizationCode code)
        {
            AuthorizationCodes.Add(code);
        }

        public async Task<AuthorizationCode> GetAuthorizationCode(string client_id, string user_id)
        {
            var code = AuthorizationCodes.FirstOrDefault(c => c.Consent.Client_Id == client_id 
                && c.Consent.User_Id == user_id
                && c.Expired == false
                && c.Expiry >= DateTime.Now);

            return await Task.FromResult(code);
        }

        public void RemoveRange(IEnumerable<AuthorizationCode> codes)
        {
            foreach (var code in codes)
            {
                AuthorizationCodes.Remove(code);
            }
        }
    }
}
