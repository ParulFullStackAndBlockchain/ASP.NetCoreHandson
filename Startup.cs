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

            //Creates a Singleton service. A Singleton service is created when it is first requested. This same instance is then 
            //used by all the subsequent requests.So in general, a Singleton service is created only one time per application and 
            //that single instance is used throughout the application life time.
            services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();

            //Creates a Scoped service. A new instance of a Scoped service is created once per request within the scope.For example,
            //in a web application it creates 1 instance per each http request but uses the same instance in the other calls within
            //that same web request.
            //services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();

            //Creates a Transient service. A new instance of a Transient service is created each time it is requested.
            //services.AddTransient<IEmployeeRepository, MockEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {               
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
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
