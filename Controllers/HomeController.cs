using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]/[action]")] // to be used in token attribute routing only
    /*[Route("Home")] */// Not to be used in token attribute routing
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

        //IMP Note : the controller route template is not combined with action method route template if the route template on 
        //the action method begins with / or ~/.

        [Route("~/Index")]
        [Route("")]
        [Route("~/")]
        /*[Route("Index")]*/ // Not to be used in token attribute routing
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        [Route("{id?}")] //to be used with token based routing only
        /*[Route("Details/{id?}")] */// Not to be used in token attribute routing
        // The ? makes id route parameter optional. To make it required remove ?
        //[Route("Home/Details/{id?}")]
        // ? makes id method parameter nullable
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                // If "id" is null use 1, else use the value passed from the route
                Employee = _employeeRepository.GetEmployee(id ?? 1),
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }
    }
}
