using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Security
{
    //Step2: Creating a custom authorization handler
    public class CanEditOnlyOtherAdminRolesAndClaimsHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ManageAdminRolesAndClaimsRequirement requirement)
        {
            //Resource property of AuthorizationHandlerContext returns the resource that we are protecting. 
            //In our case, we are using this custom requirement to protect a controller action method. 
            //So the following line returns the controller action being protected as the AuthorizationFilterContext and 
            //provides access to HttpContext, RouteData, and everything else provided by MVC and Razor Pages.
            var authFilterContext = context.Resource as AuthorizationFilterContext;

            //If AuthorizationFilterContext is NULL, we cannot check if the requirement is met or not, so we return
            //Task.CompletedTask and the access is not authorised.
            if (authFilterContext == null)
            {
                return Task.CompletedTask;
            }

            string loggedInAdminId =
                context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

            if (context.User.IsInRole("Admin") &&
                context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") &&
                adminIdBeingEdited.ToLower() != loggedInAdminId.ToLower())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
