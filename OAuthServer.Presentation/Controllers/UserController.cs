using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Application;
using OAuthServer.Application.Repository;
using OAuthServer.Persistence;
using OAuthServer.Persistence.Reopsitories;
using OAuthServer.Presentation.Models;
using OAuthServer.Util;

namespace OAuthServer.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly JwtSecurityTokenHelper _tokenHelper;
        private readonly IUserRepository _userRepository;
        private readonly OAuthContext _context;

        public UserController(JwtSecurityTokenHelper tokenHelper, IUserRepository userRepository, OAuthContext context)
        {
            _tokenHelper = tokenHelper;
            _userRepository = userRepository;
            _context = context;
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
            var loginUser = await _userRepository.LogInUser(user.Username, user.Password);

            if (loginUser == null)
                return Unauthorized();

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties() { };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return Redirect(Request.Form["ReturnUrl"]);
        }


        [HttpGet]
        [Route("User/Register")]
        public IActionResult Register(string ReturnUrl)
        {
            if (string.IsNullOrEmpty(ReturnUrl))
                ReturnUrl = "/";

            return View((object)ReturnUrl);     //Request.Query["ReturnUrl"].ToString()
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserForLogin userForRegister)
        {
            var user = new User() { Username = userForRegister.Username };

            user = await _userRepository.RegisterUser(user, userForRegister.Password);

            if (user == null)
            {
                ModelState.AddModelError("Username", "User Exists");

                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return await UserLogin(new UserForLogin() { Username = userForRegister.Username, Password = userForRegister.Password }, "");
        }
    }
}