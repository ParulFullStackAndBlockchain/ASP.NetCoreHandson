using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class UserRolesViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }

        //Note : We could include UserId property also in the UserRolesViewModel class, but as far as this view is concerned,
        //there is a one-to-many relationship from User to Roles. So, in order not to repeat UserId for each Role, we will 
        //use ViewBag to pass UserId from controller to the view.
    }
}
