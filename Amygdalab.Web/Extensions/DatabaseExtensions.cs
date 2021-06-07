using Amygdalab.Data;
using Amygdalab.Web.Extensions.Identity;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amygdalab.Web.Extensions
{
    public static class DatabaseExtensions
    {

        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("DefaultConnection");

            //services
            //    .AddDbContextPool<DataContext>((serviceProvider, optionsBuilder) =>
            //    {
            //        optionsBuilder.UseSqlServer(connectionstring, opt => opt.EnableRetryOnFailure()).EnableSensitiveDataLogging();
            //       // optionsBuilder.UseInternalServiceProvider(serviceProvider);
            //    });

          
            //services.AddTransient<DbContext, DataContext>();
        }



        public static void PopulateIdentityServer(IApplicationBuilder app , IConfiguration configuration)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Database.Migrate();

             if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClient(configuration))
                {
                    var item = context.Clients.SingleOrDefault(c => c.ClientName == client.ClientId);

                    if (item == null)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                }

                foreach (var resource in Config.ApiResources)
                {
                    var item = context.ApiResources.SingleOrDefault(c => c.Name == resource.Name);

                    if (item == null)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                }

                foreach (var scope in Config.ApiScopes)
                {
                    var item = context.ApiScopes.SingleOrDefault(c => c.Name == scope.Name);

                    if (item == null)
                    {
                        context.ApiScopes.Add(scope.ToEntity());
                    }
                }

                context.SaveChanges();
            }
           

        }
    }
}
