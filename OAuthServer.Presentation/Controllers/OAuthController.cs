using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Authorization.Models;
using OAuthServer.Authorization.Repositories;
using OAuthServer.Presentation.Models;

namespace OAuthServer.Presentation.Controllers
{
    public class OAuthController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IConsentRepository _consentRepository;
        private readonly IAuthorizationCodeRepository _authorizationCodeRepository;

        public OAuthController(IClientRepository clientRepository, IConsentRepository consentRepository, IAuthorizationCodeRepository authorizationCodeRepository)
        {
            _clientRepository = clientRepository;
            _consentRepository = consentRepository;
            _authorizationCodeRepository = authorizationCodeRepository;
        }

        [HttpGet]
        [Route("OAuth/Authorize")]
        //public IActionResult AuthorizeLogin(string client_id, string response_type, string redirect_uri, string scope = "", string state = "")
        public async Task<IActionResult> AuthorizeLogin([FromQuery]AuthorizeModel model)
        {
            bool valid = ModelState.IsValid;

            var client = await _clientRepository.GetClientById(model.client_id);
            if(client == null)
            {
                return BadRequest("Unrecognized client_id");
            }

            if(client.Redirect_Uri != model.redirect_uri)
            {
                return BadRequest("Invalid redirect URI");
            }

            if(model.response_type != "code")
            {
                //ModelState.AddModelError("")
            }

            bool isConsentRequired = false;

            if (!isConsentRequired)
            {
                return await Authorize(model);
            }

            return View("Authorize", model);
        }

        [HttpPost]
        [Route("OAuth/Authorize")]
        public async Task<IActionResult> Authorize(AuthorizeModel model)
        {
            var username = "user1";

            var consent = await _consentRepository.GetUserConsentByClientId(model.client_id, username);

            if (consent == null)
            {
                consent = consent ?? new Consent()
                {
                    Client_Id = model.client_id,
                    User_Id = username,
                    Scope = model.scope
                };
                _consentRepository.AddConsent(consent);
                //DB.SaveChanges
            }

            var existingcode = await _authorizationCodeRepository.GetAuthorizationCode(model.client_id, username);
            if(existingcode != null)
            {
                //_authorizationCodeRepository.RemoveRange(new List<AuthorizationCode>() { existingcode });
                existingcode.Expired = true;
            }

            var bytes = new byte[16];
            new Random().NextBytes(bytes);
            string hex = BitConverter.ToString(bytes).Replace("-", string.Empty);

            var authCode = new AuthorizationCode(hex, consent, DateTime.Now.AddMinutes(5));
            _authorizationCodeRepository.AddAuthorizationCode(authCode);


            string authorization_code = authCode.Code;

             var redirection_path = model.redirect_uri + "?code=" + authorization_code;
            if (!string.IsNullOrEmpty(model.state))
                redirection_path += "&state=" + model.state;

            return Redirect(redirection_path);
        }

        [HttpPost]
        [Route("OAuth/Token")]
        public async Task<IActionResult> GetAccessToken([FromBody] AccessTokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(model.Grant_Type != "authorization_code")
            {
                ModelState.AddModelError("grant_type", "unsupported grant type");

                return BadRequest(ModelState);
            }

            var response = new AccessTokenResponse()
            {
                Access_Token = "ABASDASDSADSADSAD",
                Scope = "my_scope",
                Token_Type = "bearer"
            };

            return Ok(response);
        }
    }
}