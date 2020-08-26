using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        //Inject IEmployeeRepository using Constructor Injection.This prevents HomeController from being coupled to 
        //MockEmployeeRepository. Later if we provide a new implementation for IEmployeeRepository and if we want to use that 
        //new implementation we can do the same very easily.So dependency injection prevents tight coupling.
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public string Index()
        {
            return _employeeRepository.GetEmployee(1).Name;
        }

        //Controller returns ObjectResult.This is for, when we are building an API.
        //public ObjectResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    return new ObjectResult(model);
        //}

        //Controller returns View.This is for when we are building an MVC application.
        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);
            //return View(model);

            //Using View(string viewName) method
            //return View("Test");

            //  Absolute View file path//Note : With the absolute path, to get to the root project directory, we can use
            //  / or ~/. So the following 3 lines of code does the same thing: 'return View("MyViews/Test.cshtml");',
            //  'return View("/MyViews/Test.cshtml");','return View("~/MyViews/Test.cshtml");'.
            //return View("MyViews/Test.cshtml");

            //Relative View File Path-1. Note: With relative path we do not specify the file extension .cshtml.
            //return View("../Test/Update");

            //Relative View File Path-2.
            return View("../../MyViews/Test");
        }
    }
}
