using Microsoft.EntityFrameworkCore;
using OAuthServer.Application;
using OAuthServer.Authorization.EntityFramework;
using OAuthServer.Authorization.Models;
using System;

namespace OAuthServer.Persistence
{
    public class OAuthContext: AuthorizationContext<User>
    {
        //constructor
        public OAuthContext(DbContextOptions<OAuthContext> options) : base(options) { }


        //IAuthorizationContext
        //public DbSet<Consent<User>> Consents { get; set; }
        //public DbSet<Client> Clients { get; set; }
        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasKey(c => c.User_Id);
            //modelBuilder.Entity<Client>().HasKey(c => c.Client_Id);
            //modelBuilder.Entity<Consent<User>>().HasKey(c => new { c.Client_Id, c.User_Id });
            modelBuilder.Entity<User>()
                .HasMany(u => u.DeveloperClients)
                .WithOne()
                .HasForeignKey(c => c.Developer_Id);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
