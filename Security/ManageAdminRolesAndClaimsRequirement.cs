using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Security
{
    //Application Authorization Requirement
    //An Admin user can manage other Admin user roles and claims but not their own claims and role.
    //To achieve this, we need to know the logged-in UserID and the UserId of the Admin being edited. If they are the same we 
    //do not want to allow access. The admin UserID being edited is passed in the URL as a query string parameter.

    //Note:  A func cannot be used to satisfy our authorization requirement here because we need to access the query string 
    //parameter. Also as your authorization requirements get complex, you may need access to other services via 
    //dependency injection. In situations like these we create custom requirements and handlers.

    //Step1: Creating a custom authorization requirement
    public class ManageAdminRolesAndClaimsRequirement : IAuthorizationRequirement
    {
    }
}
