using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iSnippets.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace iSnippets
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        using (var client = new MyDbContext()) {
            client.Database.EnsureCreated();
        }

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<MyDbContext>();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // add seed data
            var db = new MyDbContext();
            if (!db.SnippetTable.Any()) {
                var snips = new List<Snippet>() {
                    new Snippet() { Id = 1, snipDesc = "Twilight Zone", snipText = "That = This", snipLang = "Javascript" },
                    new Snippet() { Id = 2, snipDesc = "Zone Twilight", snipText = "This = That", snipLang = "Javascript" }
                };

                db.SnippetTable.AddRange(snips);
                db.SaveChanges();
            }
        }
    }
}
