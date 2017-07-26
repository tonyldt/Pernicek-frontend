﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pernicek.Models;

namespace Pernicek
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();


            //ALZA CORE - IDENTITY
            services.AddAlzaCoreIdentity(o => o.connectionString = Configuration.GetSection("ConnectionStrings:AlzaLego.Core.IdentityConnection").Value, Configuration);


            //ALZA MODULES - CATALOG
            services.AddAlzaModuleCatalog(o => o.connectionString = Configuration.GetSection("ConnectionStrings:AlzaLego.Module.CatalogConnection").Value);


            // Add framework services.
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Environment differents
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }




            app.UseStaticFiles();



            //ALZA CORE - IDENTITY
            app.UseAlzaCoreIdentity();





            //Routing
            app.UseMvc(routes =>
            {

                //default route
                routes.MapRoute(
                    "Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index", id = "" }
                );

            });
        }
    }
}
