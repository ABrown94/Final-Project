using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    [MetadataTypeAttribute(typeof(Person.StudentMetadata))]
    public partial class Person
    {
        internal sealed class StudentMetadata
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            [StringLength(100, ErrorMessage = "Please enter a valid phone number", MinimumLength = 7)]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }        
        }

    
    }
    [MetadataTypeAttribute(typeof(Employee.EmployeeMetadata))]
    public partial class Employee
    {
        internal sealed class EmployeeMetadata
        {
            [Required]
            [Display(Name = "Salary")]
            public decimal Salary { get; set; }
        }


    }
   
}