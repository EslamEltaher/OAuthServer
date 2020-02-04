using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Util
{
    public class JwtTokenConfigurations
    {
        public double TokenDuration { get; set; }
        public string SigningAlgorithm { get; set; }
    }
}
