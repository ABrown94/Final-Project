using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project.Models;

namespace Final_Project.Models
{
    
    public class StudentRepository
    {
        Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1();

        public List<Person> GetAllStudents()
        {
            var students = (from s in db.People
                            where s.PersonType == "Student"
                            orderby s.LastName
                           select s).ToList();
            return students;
        }

        public Person GetStudentById(int id)
        {
            var student = (from s in db.People
                                 where s.PersonId == id
                                 select s).Single();
            return student;
        }

        public List<Course> GetConfirmedCourses(int id)
        {
            var confirmedCourses = (from c in db.Courses
                                  join s in db.Student_Courses
                                  on c.CourseId equals s.CourseId
                                  where s.StudentId == id &&
                                  s.RegistrationStatus == "Confirmed"
                                  select c).ToList();
            return confirmedCourses.ToList();
        }
        public List<Course> GetAllCourses()
        {
            var courseList = (from c in db.Courses
                              where !c.CourseName.Contains("(Inactive)")
                              orderby c.CourseName
                              select c).ToList();
            return courseList;
        }
        public List<Student_Bill> GetStudentBills(int id)
        {
            var bills = (from b in db.Student_Bills
                         where b.StudentId == id &&
                         b.Paid == false
                         select b).ToList();
            return bills;
        }
        public List<Course> GetProspectiveCourses(int id)
        {
            var prospectiveCourses = (from c in db.Courses
                                    join s in db.Student_Courses
                                    on c.CourseId equals s.CourseId
                                    where s.StudentId == id &&
                                    s.RegistrationStatus == "Prospective"
                                    select c).ToList();
            return prospectiveCourses;
        }

        public void EnrollInCourse(int studentId, int courseId)
        {
            var confirmedCourse = (from s in db.Student_Courses
                                   where s.StudentId == studentId &&
                                   s.CourseId == courseId
                                   select s).Single();
            confirmedCourse.RegistrationStatus = "Confirmed";
            db.SaveChanges();
                 
            decimal installmentCost = Convert.ToDecimal(confirmedCourse.Course.Course_Details.Price) / 3;

            for (int i = 0; i < 3; i++)
            {
                Student_Bill newBill = new Student_Bill()
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    Amount = installmentCost,
                    Paid = false,                 
                    DueDate = DateTime.Now.AddMonths(+i)
                                                        
                };
                db.Student_Bills.Add(newBill);
                db.SaveChanges();
            }                                                                                                                   
        }
        public List<Student_Bill> GetBill(int studentId, int courseId)
        {
            var bill = (from b in db.Student_Bills
                        where b.StudentId == studentId &&
                        b.CourseId == courseId
                        select b).ToList();
            return bill;
        }
        public void DeleteBill(int studentId, int courseId)
        {
            db.Student_Bills.RemoveRange(db.Student_Bills.Where(b => b.StudentId == studentId && b.CourseId == courseId));
            var cancelCourse = (from s in db.Student_Courses
                                   where s.StudentId == studentId &&
                                   s.CourseId == courseId
                                   select s).Single();
            cancelCourse.RegistrationStatus = "Prospective";
            db.SaveChanges();          
        }
        public void PayStudentBill(int id)
        {
            var billToPay = (from b in db.Student_Bills
                             where b.BillId == id
                             select b).Single();
            billToPay.Paid = true;
            db.SaveChanges();
        }

        public void EditStudent(Person student)
        {
            var studentToEdit = (from s in db.People
                                 where s.PersonId == student.PersonId
                                 select s).Single();

            studentToEdit.FirstName = student.FirstName;
            studentToEdit.LastName = student.LastName;
            studentToEdit.Phone = student.Phone;
            studentToEdit.Email = student.Email;
            db.SaveChanges();
        }


        public void CreateStudent(Person student)
        {
            Person newStudent = new Person();
            newStudent.FirstName = student.FirstName;
            newStudent.LastName = student.LastName;
            newStudent.PersonType = "Student";
            newStudent.Phone = student.Phone;
            newStudent.Email = student.Email;

            db.People.Add(newStudent);
            db.SaveChanges();
        }
        public void DeleteStudent(int id)
        {
            var studentToDelete = (from p in db.People
                                   where p.PersonId == id
                                   select p).Single();
            studentToDelete.PersonType = "Inactive";

            //var billsToDelete = (from b in db.Student_Bills
            //                     where b.StudentId == id
            //                     select b).ToList();
            //foreach(var bill in billsToDelete)
            //{
            //    bill.Paid = true;
            //}
            var remindersToDelete = (from r in db.Reminders
                                     where r.StudentId == id
                                     select r).ToList();
            foreach (var re in remindersToDelete)
            {
                re.Completed = true;
            }
            db.SaveChanges();
        }


    }
}