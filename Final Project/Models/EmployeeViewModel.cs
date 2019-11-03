using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public class EmployeeViewModel
    {
        public Person Employee { get; set; }
        public Employee EmployeeDetails { get; set; }
        [Display(Name = "Courses")]
        public List<Course> TeacherCourses { get; set; }
        [Display(Name = "Teachers")]
        public List<Employee> Teachers { get; set; }
        [Display(Name = "Administrators")]
        public List<Employee> Administrators { get; set; }
        public Expens Payment { get; set; }
    
    }
}