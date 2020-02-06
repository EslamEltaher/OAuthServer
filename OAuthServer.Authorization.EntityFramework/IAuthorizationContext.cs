using Microsoft.EntityFrameworkCore;
using OAuthServer.Authorization.Models;
using System;

namespace OAuthServer.Authorization.EntityFramework
{
    public interface IAuthorizationContext//<TUser> where TUser : class, IResourceOwner
    {
        //DbSet<TUser> Users { get; set; }

        //DbSet<Consent<TUser>> Consents { get; set; }
        DbSet<Consent> Consents { get; set; }

        DbSet<Client> Clients { get; set; }
    }
}
