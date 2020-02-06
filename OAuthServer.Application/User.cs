using OAuthServer.Authorization.Models;
using System;

namespace OAuthServer.Application
{
    public class User : IResourceOwner
    {
        public string User_Id { get; set; }
        public bool IsDeveloper { get; set; }
    }
}
