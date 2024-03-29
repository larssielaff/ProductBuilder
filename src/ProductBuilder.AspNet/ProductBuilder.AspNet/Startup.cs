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
using ProductBuilder.AspNet.Data;
using ProductBuilder.AspNet.Models;
using ProductBuilder.AspNet.Services;
using ProductBuilder.Infra.CrossCutting.IoC;
using Asd.Infra.CrossCutting.Bus;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading;

namespace ProductBuilder.AspNet
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
            var identityConnectionString = Environment.GetEnvironmentVariable("IdentityConnectionString");
            var eventStoreConnectionString = Environment.GetEnvironmentVariable("EventStoreConnectionString");
            var dataConnectionString = Environment.GetEnvironmentVariable("DataConnectionString");

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(identityConnectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddProductBuilderDDD(dataConnectionString, eventStoreConnectionString);

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            AsdInMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;

            if(true)
            {
                new Thread(() => 
                {
                    Thread.CurrentThread.IsBackground = true;
                    using (var p = new Process())
                    {
                        p.StartInfo = new ProcessStartInfo("cmd.exe")
                        {
                            RedirectStandardInput = true,
                            UseShellExecute = false,
                        };

                        p.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                        {
                            throw new NotImplementedException();
                        };

                        p.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                        {
                            throw new NotImplementedException();
                        };

                        p.Start();
                        p.StandardInput.Write("lt --port 8080 --subdomain asdaas --open \n");
                        p.WaitForExit();

                        var t = this;
                    }
                }).Start();
            }
        }
    }
}
