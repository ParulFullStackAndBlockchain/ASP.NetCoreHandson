using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    //The built-in IdentityUser class has very limited set of properties like Id, Username, Email, PasswordHash etc.
    //To store additional data about the user like Gender, City, Country etc.Extend the IdentityUser class.
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
