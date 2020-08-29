using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage = "Invalid email format")]
        [Required]
        public string Email { get; set; }
        //Note : By default an enum underlying data type is int.
        //Value types(such as int, float, decimal, DateTime) are inherently required and don't need the Required attribute.
        //Select List Required Validation : If you make the Department property, a nullable property by including a question 
        //mark then the[Required] attribute is needed to make the field a required field.
        [Required]
        public Dept? Department { get; set; }
    }
}
