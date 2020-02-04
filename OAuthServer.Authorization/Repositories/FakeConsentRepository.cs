using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuthServer.Authorization.Models;

namespace OAuthServer.Authorization.Repositories
{
    public class FakeConsentRepository : IConsentRepository
    {

        public static List<Consent> Consents { get; }
        static FakeConsentRepository()
        {
            Consents = new List<Consent>();
        }

        public void AddConsent(Consent consent)
        {
            Consents.Add(consent);
        }

        public async Task<Consent> GetUserConsentByClientId(string client_id, string user_id)
        {
            var consent = Consents.FirstOrDefault(c => c.Client_Id == client_id && c.User_Id == user_id);
            return await Task.FromResult(consent);
        }
    }
}
