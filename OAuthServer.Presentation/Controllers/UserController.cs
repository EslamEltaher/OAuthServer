using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Presentation.Models;
using OAuthServer.Util;

namespace OAuthServer.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly JwtSecurityTokenHelper _tokenHelper;

        public UserController(JwtSecurityTokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        [HttpGet]
        [Route("User/Login")]
        public IActionResult Login(string ReturnUrl)
        {
            return View((object)ReturnUrl);     //Request.Query["ReturnUrl"].ToString()
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserForLogin user, string ReturnUrl)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties() { };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return Redirect(Request.Form["ReturnUrl"]);
        }
    }
}