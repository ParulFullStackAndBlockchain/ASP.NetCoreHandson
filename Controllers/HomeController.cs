﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using EmployeeManagement.Security;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IDataProtector protector;

        public HomeController(IEmployeeRepository employeeRepository,IHostingEnvironment hostingEnvironment,
                                IDataProtectionProvider dataProtectionProvider,
                                DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
        }

        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees().Select(e =>
                        {
                            e.EncryptedId = protector.Protect(e.Id.ToString());
                            return e;
                        });
            return View(model);
        }

        [AllowAnonymous]
        public ViewResult Details(string id)
        {
            string decryptedId = protector.Unprotect(id);
            int decryptedIntId = Convert.ToInt32(decryptedId);

            Employee employee = _employeeRepository.GetEmployee(decryptedIntId);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                // If "id" is null use 1, else use the value passed from the route
                Employee = employee,
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.Photo != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        // Through model binding, the action method parameter
        // EmployeeEditViewModel receives the posted edit form data
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the employee being edited from the database
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                // Update the employee object with the data in the model object
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (model.Photo != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object which will be
                    // eventually saved in the database
                    employee.PhotoPath = ProcessUploadedFile(model);
                }

                // Call update method on the repository service passing it the
                // employee object to update the data in the database table
                Employee updatedEmployee = _employeeRepository.Update(employee);

                return RedirectToAction("index");
            }

            return View(model);
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

    }
}
    
