using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Security
{
    //Requirement: One of the following 2 conditions must be met for a logged-in user to be able to manage user roles and claims.
    //Codition 1 :
    //The user must be in the Admin role AND has Edit Role claim type with a claim value of true
    //AND
    //the logged-in user Id must NOT BE EQUAL TO the Id of the Admin user being edited
    //Condition 2 : 
    //The user must be in the Super Admin role

    //Note: We implement multiple handlers for a given single requirement, when we have cases where want the evaluation to be on 
    //an OR basis. 

    public class ManageAdminRolesAndClaimsRequirement : IAuthorizationRequirement
    {
    }
}
