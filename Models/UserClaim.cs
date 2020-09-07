using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class UserClaim
    {
        public string ClaimType { get; set; }
        //determines if the clam is selected on the UI
        public bool IsSelected { get; set; }
    }
}
