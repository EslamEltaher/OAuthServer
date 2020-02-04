using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Presentation.Models;
using OAuthServer.Util;

namespace OAuthServer.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSecurityTokenHelper _tokenHelper;

        public AuthController(JwtSecurityTokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        [Route("Token")]
        public IActionResult GetToken(UserForLogin user)
        {
            var token = _tokenHelper.GenerateJwtToken(new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
            });

            return Ok(token);
        }
    }
}