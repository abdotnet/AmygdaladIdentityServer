using Amygdalab.Core.Identity;
using Amygdalab.Domain.IdentityServices;
using Amygdalab.Domain.Interfaces;
using Amygdalab.Domain.Interfaces.Managers;
using Amygdalab.Domain.Interfaces.Repositories;
using Amygdalab.Domain.Managers;
using Amygdalab.Domain.Repository;
using Amygdalab.Domain.Uow;
using Amygdalab.Web.Extensions.Identity;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amygdalab.Web.Extensions
{
    public static class ServicesExtensions
    {
        public static void ServiceConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            //managers
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Repository
            services.AddScoped<IProductHistoryRepository, ProductHistoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsPrincipalFactory>();
            services.AddTransient<IProfileService, ProfileClaimService>();
        }

        public static void AddCorsOrigin(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("*")
                        .WithHeaders("*")
                        .WithMethods("*")
                        .WithExposedHeaders("*"));
            });
        }


        public static void AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Amygdalab Product Catalogue",
                    Version = "v1",
                    Description = "A Simple Product Catalogue System",

                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });


            });

        }
    }
}
