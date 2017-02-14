using System;
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
using IdentityServerWithAspNetIdentity.Data;
using IdentityServerWithAspNetIdentity.Models;
using IdentityServerWithAspNetIdentity.Services;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace IdentityServerWithAspNetIdentity
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // Adds IdentityServer
            //services.AddIdentityServer()
            //    .AddTemporarySigningCredential()
            //    .AddInMemoryIdentityResources(Config.GetIdentityResources())
            //    .AddInMemoryApiResources(Config.GetApiResources())
            //    .AddInMemoryClients(Config.GetClients())
            //    .AddAspNetIdentity<ApplicationUser>();

            
            var connectionString = Configuration.GetConnectionString("DefaultConnection"); // @"server=(localdb)\mssqllocaldb;database=IdentityServer4.Quickstart;trusted_connection=yes";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // configure identity server with in-memory users, but EF stores for clients and resources
            //services.AddIdentityServer()
            //    .AddTemporarySigningCredential()
            //    .AddTestUsers(Config.GetUsers())
            //    .AddConfigurationStore(builder =>
            //        builder.UseSqlServer(connectionString, options =>
            //            options.MigrationsAssembly(migrationsAssembly)))
            //    .AddOperationalStore(builder =>
            //        builder.UseSqlServer(connectionString, options =>
            //            options.MigrationsAssembly(migrationsAssembly)));

            // configure identity server with in-memory users, but EF stores for clients and resources
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddAspNetIdentity<ApplicationUser>() //.AddTestUsers(Config.GetUsers())
                .AddConfigurationStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder =>
                    builder.UseSqlServer(connectionString, options =>
                        options.MigrationsAssembly(migrationsAssembly)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            // this will do the initial DB population
            InitializeDatabase(app);

            // Adds IdentityServer
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                //if (!context.Clients.Any())
                //{
                //foreach (var client in Config.GetClients())
                //{
                //    context.Clients.Add(client.ToEntity());
                //}
                //context.SaveChanges();
                //}

                foreach (var client in Config.GetClients())
                {
                    var existingClient = context.Clients.FirstOrDefault(c => c.ClientId == client.ClientId);
                    if (existingClient != null)
                        context.Clients.Remove(existingClient);
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();

                //if (!context.IdentityResources.Any())
                //{
                //    foreach (var resource in Config.GetIdentityResources())
                //    {
                //        context.IdentityResources.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}

                
                foreach (var resource in Config.GetIdentityResources())
                {
                    var existingIdentityResource = context.IdentityResources.FirstOrDefault(c => c.Name == resource.Name);
                    if (existingIdentityResource != null) 
                        context.IdentityResources.Remove(existingIdentityResource);
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
                

                //if (!context.ApiResources.Any())
                //{
                //    foreach (var resource in Config.GetApiResources())
                //    {
                //        context.ApiResources.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}

                
                foreach (var resource in Config.GetApiResources())
                {
                    var existingApiResource = context.ApiResources.FirstOrDefault(c => c.Name == resource.Name);
                    if (existingApiResource != null) 
                        context.ApiResources.Remove(existingApiResource);
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
