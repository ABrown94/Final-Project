using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1();
        CourseRepository re = new CourseRepository();
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllCourses()
        {
            var allCourses = re.GetAllCourses();         
            return View(allCourses);
        }
        public ActionResult CourseDetails(int id)
        {
            TempData["id"] = id;
            var courseDetails = re.GetCourseDetails(id);
            return View(courseDetails);
        }
        public ActionResult ViewSchedule(int id)
        {          
            var schedule = re.GetCourseSchedule(id);           
            return PartialView(schedule);
        }
        [HttpGet]
        public ActionResult CreateCourseSchedule()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateCourseSchedule(FormCollection col, int id)
        {
            string[] days = col["selectedDays"].Split(',');
            foreach (var day in days)
            {
                Course_Schedule newSchedule = new Course_Schedule();
                newSchedule.CourseId = id;
                newSchedule.DayOfWeek = day;
                newSchedule.StartTime = TimeSpan.Parse(col["startTime"]);
                newSchedule.EndTime = TimeSpan.Parse(col["endTime"]);
               

                db.Course_Schedules.Add(newSchedule);
                db.SaveChanges();
            }                
                return RedirectToAction("CourseDetails", "Course", new {id = id});            
        }
        [HttpGet]
        public ActionResult EditCourseSchedule(int id)
        {
            //var scheduleToEdit = re.GetCourseSchedule(id);
            //return View(scheduleToEdit);
            //replace next line with whatever you coded to get all the info
            var model = new EditScheduleViewModel();
            model.Schedule = re.GetCourseSchedule(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditCourseSchedule(EditScheduleViewModel model, FormCollection col, int id)
        {
            var scheduleToEdit = (from s in db.Course_Schedules
                                  where s.CourseId == id
                                  select s).ToList();
            db.Course_Schedules.RemoveRange(scheduleToEdit);
            db.SaveChanges();

            string[] days = col["selectedDays"].Split(',');
            foreach (var day in days)
            {
                Course_Schedule newSchedule = new Course_Schedule();
                newSchedule.CourseId = id;
                newSchedule.DayOfWeek = day;
                newSchedule.StartTime = TimeSpan.Parse(col["startTime"]);
                newSchedule.EndTime = TimeSpan.Parse(col["endTime"]);


                db.Course_Schedules.Add(newSchedule);
                db.SaveChanges();
            }
            //model.Schedule = re.GetCourseSchedule(id);
            //var scheduleToEdit = re.GetCourseSchedule(id);
            //foreach(var schedule in scheduleToEdit)
            //{
            //    string[] days = col["selectedDays"].Split(',');
            //    foreach (var day in days)
            //    {
            //        schedule.DayOfWeek = day;
            //    }               
            //    schedule.StartTime = TimeSpan.Parse(col["startTime"]);
            //    schedule.EndTime = TimeSpan.Parse(col["endTime"]);
            //    //schedule.StartTime = model.Schedule.First().StartTime;
            //    //schedule.EndTime = model.Schedule.First().EndTime;
            //    db.SaveChanges();                  
            //   // }                         
            //}

            return RedirectToAction("CourseDetails", "Course", new { id = id });   
        }
                                                                             
        public ActionResult SelectCourses()
        {
            var allCourses = re.GetAllCourses();
            return PartialView(allCourses);
        }
        [HttpGet]
        public ActionResult CreateCourse()
        {
            var model = new CourseViewModel();
            model.Course = new Course();
            model.CourseDetail = new Course_Detail();
            ViewBag.Teachers = re.GetTeachers();
            return View(model);
        }
       
        [HttpPost]
        public ActionResult CreateCourse(CourseViewModel model)
        {
            using( Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1())
            {
                Course newCourse = model.Course;
                Course_Detail cd = model.CourseDetail;
                cd.TeacherId = model.CourseDetail.TeacherId;
                newCourse.Course_Details = cd;

                db.Courses.Add(newCourse);
                db.Course_Details.Add(cd);
                db.SaveChanges();

            }         
            return RedirectToAction("AllCourses");
        }
        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var model = new CourseViewModel();
            model.Course = re.GetCourseToEdit(id);
            model.CourseDetail = re.GetCourseDetails(id);
            ViewBag.Teachers = re.GetTeachers();
            return View(model);
            //var courseToEdit = re.GetCourseToEdit(id);
            //var courseDetails = re.GetCourseDetails(id);
            //ViewBag.Teachers = db.Employees.ToList();
            //return View(courseToEdit);
         
        }
        [HttpPost]
        public ActionResult EditCourse(CourseViewModel model , int id)
        {
            var courseToEdit = (from c in db.Courses
                                    where c.CourseId == id
                                    select c).Single();
            courseToEdit.CourseName = model.Course.CourseName;
            courseToEdit.Course_Details.StartDate = model.CourseDetail.StartDate;
            courseToEdit.Course_Details.EndDate = model.CourseDetail.EndDate;
            courseToEdit.Course_Details.Price = model.CourseDetail.Price;
            courseToEdit.Course_Details.TeacherId = model.CourseDetail.TeacherId;
       
            db.SaveChanges();
                            
            return RedirectToAction("AllCourses");
        }

        public ActionResult DeleteCourse(int id)
        {
            re.DeleteCourse(id);
            return RedirectToAction("AllCourses");
        }
    
    }
}