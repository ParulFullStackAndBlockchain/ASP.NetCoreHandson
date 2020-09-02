using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            services.AddDbContextPool<AppDbContext>(
            options => options.UseSqlServer(_configuration.GetConnectionString("EmployeeDBConnection")));

            //To be able to return data in XML format, Xml Serializer Formatter is added.This way application respects 
            //content negotiation. It looks at the Request Accept Header and if it is set to application/xml, then XML data is 
            //returned. If the Accept header is set to application/json, then JSON data is returned.
            services.AddMvc(option => option.EnableEndpointRouting = false).AddXmlSerializerFormatters();

            //Note : We are using AddScoped() method because we want the instance to be alive and available for the 
            //entire scope of the given HTTP request. For another new HTTP request, a new instance of 
            //SQLEmployeeRepository class will be provided and it will be available throughout the entire scope of 
            //that HTTP request.
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

            //1. AddIdentity() method adds the default identity system configuration for the specified user and role types.
            //2. IdentityUser class is provided by ASP.NET core and contains properties for UserName, PasswordHash, Email etc.
            //This is the class that is used by default by the ASP.NET Core Identity framework to manage registered users of 
            //your application.
            //3. If you want store additional information about the registered users like their Gender, City etc.Create a 
            //custom class that derives from IdentityUser.In this custom class add the additional properties you need and 
            //then plug-in this class instead of the built-in IdentityUser class. 
            //4. Similarly, IdentityRole is also a builtin class provided by ASP.NET Core Identity and contains Role information.
            //5. We want to store and retrieve User and Role information of the registered users using EntityFrameWork Core
            //from the underlying SQL Server database.We specify this using AddEntityFrameworkStores<AppDbContext>() 
            //passing our application DbContext class as the generic argument.
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();

            //Adds the Authentication middleware to the application's request processing pipeline. We want to be able to 
            //authenticate users before the request reaches the MVC middleware. So it's important we add authentication
            //middleware before the MVC middleware in the request processing pipeline.
            app.UseAuthentication();

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
