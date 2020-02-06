using Microsoft.EntityFrameworkCore;
using OAuthServer.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Authorization.EntityFramework
{
    //public class AuthorizationContext<TUser> : DbContext, IAuthorizationContext<TUser> where TUser : class, IResourceOwner
    public class AuthorizationContext<TUser> : DbContext, IAuthorizationContext<TUser> where TUser : class, IResourceOwner
    {
        //constructor
        public AuthorizationContext(DbContextOptions<AuthorizationContext<TUser>> options):base(options) { }

        //entities
        public DbSet<TUser> Users { get ; set; }
        //public DbSet<Consent<kTUser>> Consents { get; set; }
        public DbSet<Consent<TUser>> Consents { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<AuthorizationCode<TUser>> AuthorizationCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TUser>().HasKey(c => c.User_Id);
            modelBuilder.Entity<Client>().HasKey(c => c.Client_Id);
            modelBuilder.Entity<Consent<TUser>>().HasKey(c => new { c.Client_Id, c.User_Id });

            modelBuilder.Ignore<AuthorizationCode<TUser>>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
