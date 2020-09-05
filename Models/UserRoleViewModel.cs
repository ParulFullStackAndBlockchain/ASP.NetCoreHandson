using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    //1. In the UserRoleViewModel class, in addition to UserId property, we have UserName and IsSelected properties.
    //2. UserName property is required so we can display the UserName on the view.
    //3. IsSelected property is required to determine if the user is selected to be a member of the role.
    //4. We could include RoleId property also in the UserRoleViewModel class, but as far as this view is concerned, 
    //there is a one-to-many relationship from Role to Users. So, in order not to repeat RoleId for each User, we will use 
    //ViewBag to pass RoleId from controller to the view.
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}

//AspNetUserRoles identity database table
//1. Application users are stored in AspNetUsers database table, where as roles are stored in AspNetRoles table. UserRoles i.e 
//  user to role mapping data is stored in AspNetUserRoles table.
//2. There is a Many - to - Many relationship between AspNetUsers and AspNetRoles table. A user can be a member of many roles 
//  and a role can contain many users as it's members. This User and Role mapping data is stored in AspNetUserRoles table.
//3. This table has just 2 columns - UserId and RoleId. Both are foreign keys. UserId column references Id column in AspNetUsers 
//  table and RoleId column references Id column in AspNetRoles table.
