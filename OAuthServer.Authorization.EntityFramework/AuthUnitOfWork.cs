using OAuthServer.Authorization.Models;
using OAuthServer.Authorization.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Authorization.EntityFramework
{
    public class AuthUnitOfWork<TUser> where TUser: class, IResourceOwner
    {
        private readonly IClientRepository _clientRepository;
        private readonly IConsentRepository<TUser> _consentRepository;
        private readonly AuthorizationContext<TUser> _context;

        public IClientRepository ClientRepository { get => _clientRepository; }
        public IConsentRepository<TUser> ConsentRepository { get => _consentRepository; }

        public AuthUnitOfWork(IClientRepository clientRepository,
            IConsentRepository<TUser> consentRepository,
            AuthorizationContext<TUser> context)
        {
            _clientRepository = clientRepository;
            _consentRepository = consentRepository;
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
