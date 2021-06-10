using Amygdalab.Core.Identity;
using Amygdalab.Data;
using Amygdalab.Domain.IdentityServices;
using Amygdalab.Domain.Managers;
using Amygdalab.Domain.Services;
using Amygdalab.Web.Extensions.Identity;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
//using IdentityServer4.Models;

namespace Amygdalab.Web.Extensions
{
    public static class SecurityExtensions
    {
        public static void AddIdentityConfiguration(this IServiceCollection services,
           IConfiguration configuration, IWebHostEnvironment _environment)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Log.Information(connectionString);
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                .AddUserManager<CustomUserManager>()
                // .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();


            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;

            });
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = false;
            });

            services.AddIdentityServer(
                options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                }
                )
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryClients(Config.GetClient(configuration))
                  .AddAspNetIdentity<User>();

            //.AddConfigurationStore(options =>
            //{
            //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
            //})
            //.AddOperationalStore(options =>
            //{
            //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
            //    options.EnableTokenCleanup = true;
            //});


            services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            // base-address of your identityserver

            options.Authority = "https://localhost:44395";// configuration["AppSettings:Authority"];
                                                         // name of the API resource
            options.Audience = "ro.angular"; //configuration["AppSettings:Audience"];
            options.RequireHttpsMetadata = false;
            //Convert.ToBoolean(configuration["AppSettings:RequireHttpsMetadata"]);
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = false
            };

        });

            //  services.AddAuthentication(
            //      options =>
            //  {
            //      options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //      options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            //  })
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = "http://localhost:63393";
            //    options.ApiName = "api1";
            //});
            //.AddJwtBearer(opt => opt.Audience = "ro.angular");
        }
    }
}
