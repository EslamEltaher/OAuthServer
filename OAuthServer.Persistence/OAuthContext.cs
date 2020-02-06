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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(c => c.Client_Id);
            modelBuilder.Entity<Consent>().HasKey(c => new { c.Client_Id, c.User_Id });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
