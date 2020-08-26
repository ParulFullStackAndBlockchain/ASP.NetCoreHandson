using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;

namespace EmployeeManagement.ViewModels
{

    //In some cases, a model object may not contain all the data a view needs. This is when we create a ViewModel.
    //It contains all the data a view needs.
    //This ViewModel class contains all the data the view needs. In general, we use ViewModels to shuttle data between 
    //a View and a Controller. For this reason ViewModels are also called as Data Transfer Objects
    public class HomeDetailsViewModel
    {
        public Employee Employee { get; set; }
        public string PageTitle { get; set; }
    }
}
