﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OAuthServer.Persistence;

namespace OAuthServer.Persistence.Migrations
{
    [DbContext(typeof(OAuthContext))]
    [Migration("20200206143143_InitialModel")]
    partial class InitialModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OAuthServer.Application.User", b =>
                {
                    b.Property<string>("User_Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeveloper");

                    b.HasKey("User_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OAuthServer.Authorization.Models.Client", b =>
                {
                    b.Property<string>("Client_Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Client_Secret");

                    b.Property<string>("Redirect_Uri");

                    b.HasKey("Client_Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("OAuthServer.Authorization.Models.Consent<OAuthServer.Application.User>", b =>
                {
                    b.Property<string>("Client_Id");

                    b.Property<string>("User_Id");

                    b.Property<string>("Scope");

                    b.HasKey("Client_Id", "User_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Consents");
                });

            modelBuilder.Entity("OAuthServer.Authorization.Models.Consent<OAuthServer.Application.User>", b =>
                {
                    b.HasOne("OAuthServer.Authorization.Models.Client", "client")
                        .WithMany()
                        .HasForeignKey("Client_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OAuthServer.Application.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
