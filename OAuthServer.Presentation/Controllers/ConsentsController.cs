﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAuthServer.Application;
using OAuthServer.Authorization.EntityFramework;

namespace OAuthServer.Presentation.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ConsentsController : Controller
    {
        private readonly AuthUnitOfWork<User> _authUnitOfWork;

        public ConsentsController(AuthUnitOfWork<User> authUnitOfWork)
        {
            _authUnitOfWork = authUnitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var user_id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var consents = await _authUnitOfWork.ConsentRepository.GetUserConsents(user_id);

            return View(consents);
        }
    }
}