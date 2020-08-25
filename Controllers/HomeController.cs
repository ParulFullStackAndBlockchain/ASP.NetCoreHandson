﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

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
    }
}
