using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project.Models
{
    public class CourseRepository
    {
        Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1();

        public List<Course> GetAllCourses()
        {
            var courses = (from c in db.Courses
                           where !c.CourseName.Contains("(Inactive)")
                           orderby c.CourseName
                            select c).ToList();
            return courses;
        }
        public Course GetCourseToEdit(int id)
        {
            var course = (from c in db.Courses
                          where c.CourseId == id
                          select c).SingleOrDefault();
            return course;

        }
        public Course_Detail GetCourseDetails(int id)
        {
            var course = (from c in db.Course_Details
                          where c.CourseId == id
                          select c).SingleOrDefault();
            return course;

        }
        public List<Course_Schedule> GetCourseSchedule(int id)
        {
            var schedule = (from s in db.Course_Schedules
                            where s.CourseId == id
                            select s).ToList();
            return schedule;
        }
        public List<Employee> GetTeachers()
        {
            var teachers = (from e in db.Employees
                            where e.EmployeeType == "Teacher"
                            orderby e.Person.LastName
                            select e).ToList();
            return teachers;
        }
        public void DeleteCourse(int id)
        {
            var courseToDelete = (from c in db.Courses
                                  where c.CourseId == id
                                  select c).Single();
            courseToDelete.CourseName = courseToDelete.CourseName + "(Inactive)";
            db.SaveChanges();         
        }

        //public void newStudentCourse(Student_Course studentCourse)
        //{
        //    Student_Course newStudentCourse = new Student_Course();
        //    newStudentCourse.StudentId = studentCourse.StudentId;
        //    newStudentCourse.CourseId = studentCourse.CourseId;
        //    newStudentCourse.RegistrationStatus = "Prospective";

        //    db.Student_Courses.Add(newStudentCourse);
        //    db.SaveChanges();                                               
        //}
    }
}