using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Util
{
    public static class RandomStringGenerator
    {
        public static string GenerateHex(int length)
        {
            var bytes = new byte[length];
            new Random().NextBytes(bytes);
            string hex = BitConverter.ToString(bytes).Replace("-", string.Empty);

            return hex;
        }
    }
}
