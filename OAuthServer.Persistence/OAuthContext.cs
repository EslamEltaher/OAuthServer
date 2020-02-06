using Microsoft.EntityFrameworkCore;
using OAuthServer.Authorization.EntityFramework;
using OAuthServer.Authorization.Models;
using System;

namespace OAuthServer.Persistence
{
    public class OAuthContext : DbContext, IAuthorizationContext
    {
        //constructor
        public OAuthContext(DbContextOptions options) : base(options) { }


        //IAuthorizationContext
        public DbSet<Consent> Consents { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
