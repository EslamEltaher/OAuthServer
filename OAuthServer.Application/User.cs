using OAuthServer.Authorization.Models;
using System;

namespace OAuthServer.Application
{
    public class User : IResourceOwner
    {
        public string User_Id { get; set; }

        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public bool IsDeveloper { get; set; }
    }
}
