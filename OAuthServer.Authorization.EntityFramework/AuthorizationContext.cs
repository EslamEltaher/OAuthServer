﻿using Microsoft.EntityFrameworkCore;
using OAuthServer.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Authorization.EntityFramework
{
    //public class AuthorizationContext<TUser> : DbContext, IAuthorizationContext<TUser> where TUser : class, IResourceOwner
    public class AuthorizationContext : DbContext, IAuthorizationContext// where TUser : class, IResourceOwner
    {
        //constructor
        public AuthorizationContext(DbContextOptions options):base(options) { }

        //entities
        //public DbSet<TUser> Users { get ; set; }
        //public DbSet<Consent<kTUser>> Consents { get; set; }
        public DbSet<Consent> Consents { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<AuthorizationCode> AuthorizationCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(c => c.Client_Id);
            modelBuilder.Entity<Consent>().HasKey(c => new { c.Client_Id, c.User_Id });

            modelBuilder.Ignore<AuthorizationCode>();

            base.OnModelCreating(modelBuilder);
        }

    }
}