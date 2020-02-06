using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Authorization.Models
{
    public interface IResourceOwner
    {
        string User_Id { get; set; }
    }
}
