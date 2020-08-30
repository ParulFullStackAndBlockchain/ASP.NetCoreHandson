using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    //To keep the DbContext class clean, you may move the seeding code from the DbContext class into an extension 
    //method on the ModelBuilder class.
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(

                    //initial data entered for id=1
                    //new Employee
                    //{
                    //    Id = 1,
                    //    Name = "Mark",
                    //    Department = Dept.IT,
                    //    Email = "mark@abc.com"
                    //}

                    //updated data entered for id=1
                    new Employee
                    {
                        Id = 1,
                        Name = "Mary",
                        Department = Dept.IT,
                        Email = "mary@pragimtech.com"
                    },
                    new Employee
                    {
                        Id = 2,
                        Name = "John",
                        Department = Dept.HR,
                        Email = "john@pragimtech.com"
                    }
                );
        }
    }
}
