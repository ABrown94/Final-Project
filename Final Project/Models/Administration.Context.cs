﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Administration_DatabaseEntities1 : DbContext
    {
        public Administration_DatabaseEntities1()
            : base("name=Administration_DatabaseEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bonu> Bonus { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Course_Detail> Course_Details { get; set; }
        public virtual DbSet<Course_Schedule> Course_Schedules { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Expens> Expenses { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<Student_Bill> Student_Bills { get; set; }
        public virtual DbSet<Student_Course> Student_Courses { get; set; }
    }
}
