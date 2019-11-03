using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project.Models
{
    public class UserRepository
    {
        Administration_DatabaseEntities1 db = new Administration_DatabaseEntities1();


        public Employee GetCurrentUser(string username)
        {
            var currentUser = (from e in db.Employees
                               where e.Username == username
                               select e).Single();
            return currentUser;
        }
            
        public List<Reminder> GetTodayReminders(int id)
        {
            var currentReminders = (from r in db.Reminders
                             where r.AdvisorId == id &&
                             r.Date == DateTime.Today &&
                             r.Completed == false
                             select r).ToList();
            return currentReminders;                             
        }

        public List<Reminder> GetWeekReminders(int id)
        {
            var baselineDate = DateTime.Now.AddDays(8);

            var remindersThisWeek = (from r in db.Reminders
                                    where r.AdvisorId == id &&
                                    r.Date <= baselineDate &&
                                    r.Date > DateTime.Today &&
                                    r.Completed == false
                                    select r).ToList();
            return remindersThisWeek;
        }

        public List<Student_Bill> GetBillsDue(int id)
        {
            var bills = (from b in db.Student_Bills
                         join r in db.Reminders on b.StudentId equals
                         r.StudentId
                         where r.AdvisorId == id &&                       
                         b.DueDate <= DateTime.Today &&
                         b.Paid == false
                         select b).ToList();
            return bills;
                         
        }
        public void SetReminderToComplete(int reminderId)
        {
            var reminder = (from r in db.Reminders
                            where r.ReminderId == reminderId
                            select r).Single();
            reminder.Completed = true;
            db.SaveChanges();
                                
        }
        public void AddBonus(int employeeId, int courseId)
        {
            var coursePrice = (from c in db.Course_Details
                               where c.CourseId == courseId
                               select c.Price).Single();
            Bonu newBonus = new Bonu();
            newBonus.EmployeeId = employeeId;
            newBonus.Amount = coursePrice * 0.05m;
            newBonus.Paid = false;

            db.Bonus.Add(newBonus);
            db.SaveChanges();
        }
    }
}