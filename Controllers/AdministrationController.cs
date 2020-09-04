using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}
//1. To create a user in asp.net core we use UserManager class. Similarly, to create a role, we use RoleManager class 
//      provided by asp.net core
//2. The built-in IdentityRole class represents a Role
//3. RoleManager class performs all the CRUD operations i.e Creating, Reading, Updating and Deleting roles from the 
//      underlying database table AspNetRoles
//4. To tell the RoleManager class to work with IdentityRole class, we specify IdentityRole class as the generic argument 
//      to RoleManager 
//5. RoleManager is made available to any controller or view by asp.net core dependency injection system
