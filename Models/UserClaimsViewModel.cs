using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    //As far as this view is concerned, there is a one-to-many relationship from the user to claim.
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Cliams = new List<UserClaim>();
        }

        //holds the ID of the user for whom we are adding or removing a claim
        public string UserId { get; set; }
        //holds the list of claims
        public List<UserClaim> Cliams { get; set; }
    }
}
