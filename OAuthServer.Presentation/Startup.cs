using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OAuthServer.Application;
using OAuthServer.Application.Repository;
using OAuthServer.Authorization.EntityFramework;
using OAuthServer.Authorization.EntityFramework.Repositories;
using OAuthServer.Authorization.Repositories;
using OAuthServer.Persistence;
using OAuthServer.Persistence.Reopsitories;
using OAuthServer.Util;

namespace OAuthServer.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<IAuthorizationContext<User>, AuthorizationContext<User>>(options => {
                options.UseSqlServer(
                     Configuration.GetConnectionString("Default")
                 );
                //options.UseApplicationServiceProvider()
             });

            services.AddDbContext<OAuthContext>(options => {
                options.UseSqlServer(
                     Configuration.GetConnectionString("Default")
                 );
                //options.UseApplicationServiceProvider()
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton(new JwtTokenConfigurations() {
                SigningAlgorithm = SecurityAlgorithms.HmacSha512Signature,
                TokenDuration = 3600
            });

            services.AddScoped<JwtSecurityTokenHelper>();

            services.AddScoped<IClientRepository, EFClientRepository<User>>();
            services.AddScoped<IConsentRepository<User>, EFConsentRepository<User>>();
            services.AddScoped<AuthorizationContext<User>>();
            services.AddScoped<AuthUnitOfWork<User>>();
            //services.AddScoped<IClientRepository, FakeClientRepository>();
            //services.AddScoped<IConsentRepository, FakeConsentRepository>();
            services.AddScoped<IAuthorizationCodeRepository<User>, FakeAuthorizationCodeRepository<User>>();

            services.AddScoped<IUserRepository, UserRepository>();

            //new FakeClientRepository().AddClient(new Authorization.Models.Client()
            //{
            //    Client_Id = "client_1",
            //    Client_Secret = "password_1",
            //    Redirect_Uri = "http://tryingOAuth.com/cb"
            //});

            var keyInConfiguration = Configuration.GetSection("SecurityConfig:SigningKey").Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyInConfiguration));

            services.AddSingleton<SecurityKey>(securityKey);

            #region Authentication
            services.AddAuthentication(options => {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
                options.LoginPath = "/User/Login";
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    IssuerSigningKey = securityKey,
                    ValidateIssuerSigningKey = true,


                };
            });

            #region Trials
            //services.AddAuthentication(options => {

                //options.AddScheme("cookieBased", builder => {
                //    builder.HandlerType = typeof(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationHandler);
                //});

                //    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
                //    options.DefaultSignInScheme= Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
                //    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            //})

            #endregion
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
