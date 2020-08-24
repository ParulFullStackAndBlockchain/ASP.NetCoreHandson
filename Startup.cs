using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //This middleware responds with the developer exception page, if there is an exception and if the environment is Development.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

             //IMP Points :
             //1. The second middleware that is registered using the Run() method, can only write a message to the Response object.
             //At the moment, this is the middleware that responds to every request.
             //2. Doesn’t matter what your request path is.All requests will be handled by this one piece of middleware and the response we 
             //get is the string message that the middleware is writing to the Response object.The response is plain text and not html.
             //3. A middleware that is registered using the Run() method cannot call the next middleware in the pipeline
             //So, the middleware that we register using Run() method is a terminal middleware.

             //app.Run(async (context) =>
             //{
             //    await context.Response.WriteAsync("Hello from 1st Middleware");
             //});


             //IMP Points : 
             //1. If you want your middleware to be able to call the next middleware in the pipeline, then register the middleware using 
             //Use() method 
             //2. Use() method has 2 parameters. The first parameter is the HttpContext context object and the second parameter is 
             //the Func type i.e it is a generic delegate that represents the next middleware in the pipeline.
             //3. It is through this HttpContext object, the middleware gains access to both the incoming http request 
             //and outgoing http response.

            app.Use(async (context, next) =>
            {
                logger.LogInformation("MW1: Incoming Request");
                await next();
                logger.LogInformation("MW1: Outgoing Response");
            });

            app.Use(async (context, next) =>
            {
                logger.LogInformation("MW2: Incoming Request");
                await next();
                logger.LogInformation("MW2: Outgoing Response");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("MW3: Request handled and response produced");
                    logger.LogInformation("MW3: Request handled and response produced");
                });
            });
        }
    }
}
