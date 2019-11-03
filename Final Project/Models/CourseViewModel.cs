using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    
    public class CourseViewModel
    {
        [Required]
        public Course Course { get; set; }
        [Required]
        public Course_Detail CourseDetail { get; set; }
        public List<Employee> Teacher { get; set; }
        //public int EmployeeId { get; set; }
          
    }


    public class EditScheduleViewModel
    {
        public List<Course_Schedule> Schedule { get; set; }
        //public class Course_Schedule
        //{
        //    [Required]
        //    [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
        //    public TimeSpan StartTime { get; set; }

        //    [Required]
        //    [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
        //    public TimeSpan EndTime { get; set; }

        //}
      
        //public List<DateTime> DayOfWeek { get; set; }
    }
}