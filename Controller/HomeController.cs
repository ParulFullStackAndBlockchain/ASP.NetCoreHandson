using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controller
{
    public class HomeController 
    {
        public string Index()
        {
            return "Hello from MVC";
        }
    }
}
