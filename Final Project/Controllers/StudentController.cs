using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1();
        StudentRepository re = new StudentRepository();
        UserRepository ur = new UserRepository();
     
            
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllStudents()
        {
            var allStudents = re.GetAllStudents();
            return View(allStudents);
        }
        [HttpGet]
        public ActionResult StudentDetails(int id, string quote)
        {          
            var model = new StudentViewModel();
            model.Student = re.GetStudentById(id);
            model.Bills = re.GetStudentBills(id);
            model.ConfirmedStudentCourses = re.GetConfirmedCourses(id);
            model.ProspectiveStudentCourses = re.GetProspectiveCourses(id);            
            var rejectList = model.ConfirmedStudentCourses.Concat(model.ProspectiveStudentCourses);
            var allCourses = re.GetAllCourses();
            ViewBag.remainingCourses = allCourses.Except(rejectList);
            ViewBag.Message = "due";
                              
            return View(model);
        }
        [HttpPost]
        public ActionResult StudentDetails(FormCollection col)
        {
             if (col.AllKeys.Contains("selectedCourses"))
             {
                 string[] courses = col["selectedCourses"].Split(',');
                 foreach (var course in courses)
                 {
                     Student_Course newStudentCourse = new Student_Course()
                     {
                         StudentId = Convert.ToInt32(col["StudentId"]),
                         RegistrationStatus = "Prospective",
                         CourseId = Convert.ToInt32(course)
                     };
                     db.Student_Courses.Add(newStudentCourse);
                     db.SaveChanges();
                 } 
             }
         
            var username = User.Identity.GetUserName();
            var currentUser = ur.GetCurrentUser(username);
            if (col["notes"] != "")
            {
                Reminder newReminder = new Reminder()
                {
                    StudentId = Convert.ToInt32(col["StudentId"]),
                    AdvisorId = currentUser.EmployeeId,
                    Date = Convert.ToDateTime(col["date"]),
                    Notes = col["notes"],
                    Completed = false
                };
                db.Reminders.Add(newReminder);
                db.SaveChanges();
            }
          
            return RedirectToAction("StudentDetails", new { id = Convert.ToInt32(col["StudentId"])});
        }
        public ActionResult ConfirmStudentCourse(int studentid, int courseid)
        {
            re.EnrollInCourse(studentid, courseid);
            return RedirectToAction("ConfirmBill", new { sId = studentid, cId = courseid});
        }
        //[HttpGet]
        public ActionResult ConfirmBill(int sId, int cId)
        {
            var confirmation = re.GetBill(sId, cId);
            return View(confirmation);
        }
        public ActionResult AddBonus(int studentId, int courseId)
        {
            var username = User.Identity.GetUserName();
            var currentUser = ur.GetCurrentUser(username);
            ur.AddBonus(currentUser.EmployeeId, courseId);

            return RedirectToAction("StudentDetails", new { id = studentId });
        }
        public ActionResult DeleteBill(int studentId, int courseId)
        {
            re.DeleteBill(studentId, courseId);
            return RedirectToAction("AllStudents");
        }
        public ActionResult PayStudentBill(int billId, int studentId)
        {
            re.PayStudentBill(billId);
            return RedirectToAction("StudentDetails", new { id = studentId });
        }
        [HttpGet]
        public ActionResult CreateNewStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewStudent(FormCollection col)
        {
            var username = User.Identity.GetUserName();
            var currentUser = ur.GetCurrentUser(username);

            Person newStudent = new Person()
            {
                FirstName = col["firstName"],
                LastName = col["lastName"],
                PersonType = "Student",
                Phone = col["phone"],
                Email = col["email"]
            };
                     
            db.People.Add(newStudent);
            db.SaveChanges();

            if (col.AllKeys.Contains("selectedCourses"))
            {
                string[] courses = col["selectedCourses"].Split(',');
                foreach (var course in courses)
                {
                    Student_Course newStudentCourse = new Student_Course()
                    {
                        StudentId = newStudent.PersonId,
                        RegistrationStatus = "Prospective",
                        CourseId = Convert.ToInt32(course)
                    };
                    db.Student_Courses.Add(newStudentCourse);
                    db.SaveChanges();
                }
            }
            if (col["notes"] != "")
            {
                Reminder newReminder = new Reminder()
                {
                    StudentId = newStudent.PersonId,
                    AdvisorId = currentUser.EmployeeId,
                    Date = Convert.ToDateTime(col["date"]),
                    Notes = col["notes"],
                    Completed = false
                };
                db.Reminders.Add(newReminder);
                db.SaveChanges();
            }
            return RedirectToAction("AllStudents");            
        }
        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            var studentToEdit = re.GetStudentById(id);
            return View(studentToEdit);
        }
        [HttpPost]
        public ActionResult EditStudent(Person student)
        {
            re.EditStudent(student);
            return RedirectToAction("AllStudents");
        }
        public ActionResult DeleteStudent(int id)
        {
            re.DeleteStudent(id);
            return RedirectToAction("AllStudents");
        }
    }    
}