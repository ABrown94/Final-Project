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
    public class UserController : Controller
    {
        UserRepository re = new UserRepository();
        StudentRepository sr = new StudentRepository();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserHomepage()
        {
            using(Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1())
            {
                var username = User.Identity.GetUserName();
                var currentUser = re.GetCurrentUser(username);

                var model = new UserViewModel();
                model.User = re.GetCurrentUser(username);
                model.Today = re.GetTodayReminders(currentUser.EmployeeId);
                model.ThisWeek = re.GetWeekReminders(currentUser.EmployeeId);

                model.BillsDue = re.GetBillsDue(currentUser.EmployeeId);
                ViewBag.Students = sr.GetAllStudents();
                return View(model);
            }
        }
        public ActionResult ReminderComplete(int id)
        {
            re.SetReminderToComplete(id);
            return RedirectToAction("UserHomepage");
         
        }
    }
}