using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _configuration;

        // Notice we are using Dependency Injection here
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //To be able to return data in XML format, Xml Serializer Formatter is added.This way application respects 
            //content negotiation. It looks at the Request Accept Header and if it is set to application/xml, then XML data is 
            //returned. If the Accept header is set to application/json, then JSON data is returned.
            services.AddMvc(option => option.EnableEndpointRouting = false).AddXmlSerializerFormatters();

            services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {               
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //There are 2 routing techniques in ASP.NET Core MVC. Conventional Routing and Attribute Routing.
            //Conventional Routing : UseMvcWithDefaultRoute() method adds MVC with the following default route to our 
            //application's request processing pipeline. {controller=Home}/{action=Index}/{id?}

            //Note: UseMvcWithDefaultRoute() method internally calls UseMvc() method and it sets up the default route. 
            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                //The default route template "{controller=Home}/{action=Index}/{id?}" maps most URL's that have the following 
                //pattern http://localhost:14763/home/details/1
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World");
                });
            });

        }
    }
}
