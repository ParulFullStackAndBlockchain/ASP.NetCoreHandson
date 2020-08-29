using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //1. We can use either AddDbContext() or AddDbContextPool() method to register our application specific 
            //DbContext class with the ASP.NET Core dependency injection system.
            //2. UseSqlServer() extension method is used to configure our application specific DbContext class to use 
            //Microsoft SQL Server as the database.
            //3. To connect to a database, we need the database connection string which is provided as a parameter to 
            //UseSqlServer() extension method.Instead of hard-coding the connection string in application code, we store
            //it in appsettings.json configuration file.
            //4. To read connection string from appsettings.json file we use IConfiguration service 
            //GetConnectionString() method.
            services.AddDbContextPool<AppDbContext>(
            options => options.UseSqlServer(_configuration.GetConnectionString("EmployeeDBConnection")));

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
