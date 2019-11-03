using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Project.Models;

namespace Final_Project.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        EmployeeRepository re = new EmployeeRepository();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllEmployees()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.Teachers = re.GetAllTeachers();
            model.Administrators = re.GetAllAdministrators();
            return View(model);
        }
        [HttpGet]
        public ActionResult EmployeeDetails(int id)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.Employee = re.GetEmployeeById(id);
            model.TeacherCourses = re.GetTeacherCourses(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EmployeeDetails(FormCollection col)
        {
            var bonus = re.GetBonuses(Convert.ToInt32(col["employeeId"]));
            var id = (Convert.ToInt32(col["employeeId"]));
            using(Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1())
            {
                Expens newPayment = new Expens();
                newPayment.EmployeeId = id;
                newPayment.Amount = (Convert.ToDecimal(col["hours"]) * Convert.ToDecimal(col["salary"])) + bonus;
                newPayment.Paid = true;
                db.Expenses.Add(newPayment);
                db.SaveChanges();
            }
            TempData["receipt"] = re.GetReceipt();
            TempData["bonus"] = re.GetBonuses(id);
            return RedirectToAction("EmployeeDetails", new { id = id});
            //return RedirectToAction("PaymentReceipt", new { id = Convert.ToInt32(col["employeeId"])});           
        }
        public ActionResult PaymentReceipt(int id)
        {
            var receipt = re.GetReceipt();

            ViewBag.Bonus = re.GetBonuses(id);
            return View(receipt);
        }
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.Employee = re.GetEmployeeById(id);
            model.EmployeeDetails = re.GetEmployeeDetailsById(id);
            return View(model);           
        }
        [HttpPost]
        public ActionResult EditEmployee(EmployeeViewModel model, int id)
        {
            using (Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1())
            {
                var employeeToEdit = (from e in db.People
                                      where e.PersonId == id
                                      select e).Single();
                employeeToEdit.FirstName = model.Employee.FirstName;
                employeeToEdit.LastName = model.Employee.LastName;
                employeeToEdit.Phone = model.Employee.Phone;
                employeeToEdit.Email = model.Employee.Email;
                employeeToEdit.Employee.Salary = Convert.ToDecimal(model.EmployeeDetails.Salary);
                db.SaveChanges();
            }
            return RedirectToAction("EmployeeDetails", new { id = id});
                      
        }
        public ActionResult DeleteEmployee(int id)
        {
            re.DeleteEmployee(id);
            return RedirectToAction("AllEmployees");
        }
    }
}