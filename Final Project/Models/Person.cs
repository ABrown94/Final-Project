//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Person
    {
        public Person()
        {
            this.Bonus = new HashSet<Bonu>();
            this.Course_Details = new HashSet<Course_Detail>();
            this.Expenses = new HashSet<Expens>();
            this.Reminders = new HashSet<Reminder>();
            this.Reminders1 = new HashSet<Reminder>();
            this.Student_Bill = new HashSet<Student_Bill>();
            this.Student_Course = new HashSet<Student_Course>();
        }
    
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<Bonu> Bonus { get; set; }
        public virtual ICollection<Course_Detail> Course_Details { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Expens> Expenses { get; set; }
        public virtual ICollection<Reminder> Reminders { get; set; }
        public virtual ICollection<Reminder> Reminders1 { get; set; }
        public virtual ICollection<Student_Bill> Student_Bill { get; set; }
        public virtual ICollection<Student_Course> Student_Course { get; set; }
    }
}
