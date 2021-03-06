using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Test;
using IdentityModel;
using Platibus.Identity.Handlers;

namespace Platibus.Identity
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
			services.AddMvc();

			services.AddIdentityServer()
					.AddDeveloperSigningCredential()
			        .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
					.AddInMemoryApiResources(IdentityConfig.GetApiResources())
					.AddInMemoryClients(IdentityConfig.GetClients())
			        .AddTestUsers(IdentityConfig.GetUsers());

            services.AddScoped<IUserHandler, UserHandler>();


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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
     
			app.UseMvcWithDefaultRoute();

			app.UseIdentityServer();
        }
    }
}
