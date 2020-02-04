using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OAuthServer.Util
{
    public class JwtSecurityTokenHelper
    {
        private readonly SecurityKey _securityKey;
        private readonly JwtTokenConfigurations _config;

        public JwtSecurityTokenHelper(SecurityKey securityKey, JwtTokenConfigurations config)
        {
            _securityKey = securityKey;
            _config = config;
        }


        public string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var signingCredentials = new SigningCredentials(_securityKey, _config.SigningAlgorithm);

            var claimsIdentity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor() {
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                

                //old params
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddSeconds(_config.TokenDuration),
                SigningCredentials = signingCredentials,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
