using Amygdalab.Core.Validation;
using Amygdalab.Data;
using Amygdalab.Domain.Mappings;
using Amygdalab.Web.Extensions;
using Amygdalab.Web.Filters;
using Amygdalab.Web.Middleware;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amygdalab.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        public IConfiguration configuration { get; }
        public IWebHostEnvironment env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

           // IdentityModelEventSource.ShowPII = true;
            services.AddAutoMapper(typeof(AutoMapping));
            services.AddCorsOrigin();

            //MEMORY CACHING
           // services.AddDistributedMemoryCache();
            services.AddIdentityConfiguration(configuration, env);
            services.AddDatabaseConfiguration(configuration);
            // register manager and repositories
            services.ServiceConfigurations(configuration);

         
            // API VERSIONING 
            services.AddApiVersioning(
              config =>
              {
                  // Specify the default API Version as 1.0
                  config.DefaultApiVersion = new ApiVersion(1, 0);
                  // If the client hasn't specified the API version in the request, use the default API version number 
                  config.AssumeDefaultVersionWhenUnspecified = true;
                  // Advertise the API versions supported for the particular endpoint
                  config.ReportApiVersions = true;
              }
              );

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddMvc(config =>
            {
                 config.Filters.Add<CustomActionFilter>();

                //var policy = new AuthorizationPolicyBuilder()
                //                   .RequireAuthenticatedUser()
                //                   .Build();

                //config.Filters.Add(new AuthorizeFilter(policy));
            })
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.WriteIndented = true;
                 options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                 options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
             })
           .AddFluentValidation(opt =>
           {
               opt.RegisterValidatorsFromAssemblyContaining<ProductValidator>();
           });

            services.AddSwaggerDoc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }


            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
         

            //    context.Database.Migrate();

           // DatabaseExtensions.PopulateIdentityServer(app);

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                    async context =>
                    {
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        var exception = error.Error;

                       Log.Error(exception?.Message);

                        var (ApiResponse, statusCode) = GlobalExceptionFilter.GetStatusCode<object>(exception);
                        context.Response.StatusCode = (int)statusCode;
                        context.Response.ContentType = "application/json";

                        var responseJson = JsonConvert.SerializeObject(ApiResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        await context.Response.WriteAsync(responseJson);
                    });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Home");
            });

          
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amygdalab Product Catalogue v1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
              //  c.RoutePrefix = string.Empty;

            });

        }
    }
}
