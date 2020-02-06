using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAuthServer.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedResourceController : ControllerBase
    {
        //resource can be accessed with cookie-based Authentication or JwtBasedAuthentication
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Data()
        {
            return Ok("1,2,3");
        }
    }
}