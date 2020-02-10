using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Application;
using OAuthServer.Authorization.EntityFramework;
using OAuthServer.Authorization.Models;
using OAuthServer.Authorization.Repositories;
using OAuthServer.Presentation.Models;
using OAuthServer.Util;

namespace OAuthServer.Presentation.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Developer")]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly AuthUnitOfWork<User> _authUnitOfWork;

        public ClientController(IClientRepository clientRepository, AuthUnitOfWork<User> authUnitOfWork)
        {
            _clientRepository = clientRepository;
            _authUnitOfWork = authUnitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var user_id = User.Claims.FirstOrDefault(C => C.Type == ClaimTypes.NameIdentifier).Value;
            var clients = await _clientRepository.GetClientsForDeveloper(user_id);

            return View(clients);
        }


        [HttpGet]
        public IActionResult NewClient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(AddClientModel client)
        {
            var user_id = User.Claims.FirstOrDefault(C => C.Type == ClaimTypes.NameIdentifier).Value;
            var addedClient = new Client() {
                Client_Id = RandomStringGenerator.GenerateHex(16),
                Client_Secret = RandomStringGenerator.GenerateHex(32),
                Developer_Id = user_id,
                Redirect_Uri = client.Redirect_Uri,
                Client_Name = client.Client_Name
            };

            _authUnitOfWork.ClientRepository.AddClient(addedClient);
            await _authUnitOfWork.SaveAsync();

            return RedirectToAction("Index");
        }
    }
}