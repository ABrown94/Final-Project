using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public class UserViewModel
    {
        public Employee User { get; set; }

        public Person Student { get; set; }

        public List<Person> students { get; set; }

        [Display(Name = "Today's Reminders")]
        public List<Reminder> Today { get; set; }

        [Display(Name = "This Week's Reminders")]
        public List<Reminder> ThisWeek { get; set; }

        [Display(Name = "Student Bills Due")]
        public List<Student_Bill> BillsDue { get; set; }
               
    }
}