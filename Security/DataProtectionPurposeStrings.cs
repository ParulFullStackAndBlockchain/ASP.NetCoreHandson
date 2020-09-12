using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Security
{
    //Requirement: To encrypt and decrypt route values

    // Step1: Create purpose string
    public class DataProtectionPurposeStrings
    {
        public readonly string EmployeeIdRouteValue = "EmployeeIdRouteValue";
    }
}

//Purpose string in ASP.NET Core

// You can think of purpose string as an encryption key. This key is then combined with the master or root key to 
// generate a unique key. The data that is encrypted by a given combination of purpose string and root key can only be 
// decrypted by that same combination of keys.

// The purpose string is inherent to the security of the data protection system, as it provides isolation between 
// cryptographic consumers, even if the root keys are the same.
