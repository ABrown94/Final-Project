using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public class StudentViewModel
    {
        public Person Student { get; set; }
   
        [Display(Name = "Prospective Courses")]
        public List<Course> ProspectiveStudentCourses { get; set; }
        [Display(Name = "Enrolled Courses")]
        public List<Course> ConfirmedStudentCourses { get; set; }
        public List<Student_Bill> Bills { get; set; }
        //public List<Course> RejectList { get; set; }
        //public List<Course> AllCourses { get; set; }
        //public List<Course> remainingCourses { get; set; }

        public string[] selectedCourses { get; set; }

        public Reminder NewReminder { get; set; }
        public Student_Course NewStudentCourse { get; set; }


        
    }

    public class StudentViewModelPost
    {
        public Reminder NewReminder { get; set; }
        public string[] selectedCourses { get; set; }
        
    }
}