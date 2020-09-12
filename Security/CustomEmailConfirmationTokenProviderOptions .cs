using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Security
{
    // To set the lifespan of email confirmation token type to 3 days, you can do so by creating a custom token provider. 
    // By default, it is the built-in DataProtectionTokenProviderOptions class that controls the token lifespan of all 
    // token types. If you want to set a specific lifespan for just the email confirmation token type, create a 
    // CustomEmailConfirmationTokenProviderOptions class. 

    // Step1: Create custom email confirmation token provider options
    public class CustomEmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
    }
}
