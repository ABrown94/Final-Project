using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project.Models
{
    public class EmployeeRepository
    {
        Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1();

        public List<Employee> GetAllTeachers()
        {
            var teachers = (from t in db.Employees
                            where t.EmployeeType == "Teacher"
                            orderby t.Person.LastName
                            select t).ToList();
            return teachers;
        }
        public List<Employee> GetAllAdministrators()
        {
            var admins = (from a in db.Employees
                            where a.EmployeeType == "Administration"
                            orderby a.Person.LastName
                            select a).ToList();
            return admins;
        }
        public Person GetEmployeeById(int id)
        {
            var emp = (from e in db.People
                           where e.PersonId == id
                           select e).Single();
            return emp;
        }
        public Employee GetEmployeeDetailsById(int id)
        {
            var emp = (from e in db.Employees
                       where e.EmployeeId == id
                       select e).Single();
            return emp;
        }
        public List<Course> GetTeacherCourses(int id)
        {
            var courses = (from c in db.Courses
                           where c.Course_Details.TeacherId == id
                           where !c.CourseName.Contains("(Inactive)")
                           orderby c.CourseName
                           select c).ToList();
            return courses;
                            
        }
        public decimal GetBonuses(int id)
        {
            var bonus = (from b in db.Bonus
                         where b.EmployeeId == id &&
                         b.Paid == false
                         select b.Amount).ToList();
            var bonuses = bonus.Sum();
            //move to controller
            SetBonusAsPaid(id);
            return Convert.ToDecimal(bonuses);
        }
        public void SetBonusAsPaid(int id)
        {
            var bonus = (from b in db.Bonus
                         where b.EmployeeId == id &&
                         b.Paid == false
                         select b).ToList();
            foreach (var b in bonus)
            {
                b.Paid = true;
            }
            db.SaveChanges();
        }
        public decimal GetReceipt()
        {
            var receipt = (from e in db.Expenses
                               orderby e.ExpenseId descending
                               select e.Amount).First();
            decimal amount = Convert.ToDecimal(receipt);
            return amount;
        }
        public void DeleteEmployee(int id)
        {
            var employeeToDelete = (from e in db.Employees
                                    where e.EmployeeId == id
                                    select e).Single();
            employeeToDelete.EmployeeType = "Former";
            db.SaveChanges();
        }
    }
}