using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuthServer.Authorization.Models;

namespace OAuthServer.Authorization.Repositories
{
    public class FakeConsentRepository<TUser> : IConsentRepository<TUser> where TUser : IResourceOwner
    {

        public static List<Consent<TUser>> Consents { get; }
        static FakeConsentRepository()
        {
            Consents = new List<Consent<TUser>>();
        }

        public void AddConsent(Consent<TUser> consent)
        {
            Consents.Add(consent);
        }

        public async Task<Consent<TUser>> GetUserConsentByClientId(string client_id, string user_id)
        {
            var consent = Consents.FirstOrDefault(c => c.Client_Id == client_id && c.User_Id == user_id);
            return await Task.FromResult(consent);
        }

        public async Task<IEnumerable<Consent<TUser>>> GetUserConsents(string user_id)
        {
            var consents = Consents.Where(c => c.User_Id == user_id).ToList();

            return await Task.FromResult(consents);
        }

        public async Task<Consent<TUser>> GetConsentByRefreshToken(string refresh_token)
        {
            var consent = Consents.FirstOrDefault(c => c.RefreshToken == refresh_token);
            return await Task.FromResult(consent);
        }

        public void DeleteConsent(Consent<TUser> consent)
        {
            Consents.Remove(consent);
        }

        public void UpdateConsent(Consent<TUser> consent)
        {
        }
    }
}
