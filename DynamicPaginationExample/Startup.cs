using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DynamicPaginationExample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace DynamicPaginationExample
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
            //Auto DI https://www.thereformedprogrammer.net/asp-net-core-fast-and-automatic-dependency-injection-setup/
            var assemblyToScan = Assembly.GetExecutingAssembly(); //..or whatever assembly you need
            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.AddDbContext<AppDbContext>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            Seed(serviceProvider).Wait();
        }

        private static async Task Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<AppDbContext>();

            if (!context.Products.Any())
            {
                for (var i = 1; i <= 2000; i++)
                {
                    var randomNumber = new Random().Next(10, 150);

                   await context.Products.AddAsync(
                        new Product { 
                            Description = $"Item {i}", 
                            Price = Math.Round(randomNumber.ToDouble() + 
                            Convert.ToDouble(new Random(randomNumber).NextDouble()), 2)}
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
