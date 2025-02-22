﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        // Inject ASP.NET Core ILogger service. Specify the Controller
        // Type as the generic parameter. This helps us identify later
        // which class or controller has logged the exception
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        // If there is 404 status code, the route path will become Error/404
        // Note: following action method is used with UseStatusCodePagesWithRedirects middleware or 
        // UseStatusCodePagesWithReExecute middleware.
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            //Note: If you are using UseStatusCodePagesWithReExecute middleware, it's also possible to get the original 
            //path in the ErrorController using IStatusCodeReExecuteFeature.
            var statusCodeResult =  HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    // LogWarning() method logs the message under
                    // Warning category in the log
                    logger.LogWarning($"404 error occured. Path = " +
                        $"{statusCodeResult.OriginalPath} and QueryString = " +
                        $"{statusCodeResult.OriginalQueryString}");
                    break;
            }

            return View("NotFound");
        }

        //Retrieves the exception details and returns the custom Error view. 
        //Note: In a production application, we do not display the exception details on the error view. We instead log them 
        //to a database table, file, event viewer etc, so a developer can review them and provide a code fix if required. 
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            // Retrieve the exception Details
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            // LogError() method logs the exception under Error category in the log
            logger.LogError($"The path {exceptionHandlerPathFeature.Path} " +
                $"threw an exception {exceptionHandlerPathFeature.Error}");

            return View("Error");
        }
    }
}
