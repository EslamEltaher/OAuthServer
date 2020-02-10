using OAuthServer.Authorization.Models;
using System;
using System.Collections.Generic;

namespace OAuthServer.Application
{
    public class User : IResourceOwner
    {
        public string User_Id { get; set; }

        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Fullname { get; set; }
        public bool IsDeveloper { get; set; }

        public ICollection<Client> DeveloperClients { get; set; }
    }
}
