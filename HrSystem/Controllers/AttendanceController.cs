using GraduationProject.Models;
using GraduationProject.Services;
using GraduationProject.ViewModels;
using HrSystem.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GraduationProject.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly IRepository<Attendance_Leaving> repAttend;
        private readonly IRepository<Employee> repEmp;
        private readonly HRSystem hRSystem;

        public AttendanceController(IRepository<Employee> RepEmp,IRepository<Attendance_Leaving> RepAttend, HRSystem hRSystem)
        {
            repAttend = RepAttend;
            this.hRSystem = hRSystem;
            repEmp = RepEmp;

        }
        public IActionResult Index()
        {
            var AllAttendance = repAttend.GetAll();
            return View(AllAttendance);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Show(SearchAttendanceViewModel MyView)
        {
            if (ModelState.IsValid == true)
            {
                var ValidEmpName = hRSystem.Attendance_Leavings.Where(e => e.Emp.Name.Contains(MyView.Search)).Count();
                var ValidDeptName = hRSystem.Attendance_Leavings.Where(e => e.Emp.Dept.Name.Contains(MyView.Search)).Count();
                var record = hRSystem.Attendance_Leavings.Where(e => e.Date >= MyView.From && e.Date <= MyView.To && (e.Emp.Dept.Name.Contains(MyView.Search) || e.Emp.Name.Contains(MyView.Search))).Include(e => e.Emp).Include(e => e.Emp.Dept).Count();
                if (ValidEmpName==0 && ValidDeptName==0)
                {
                    ModelState.AddModelError("Search", "Employee Name or Department Not Found");

                }
                else if( MyView.From > MyView.To)
                {
                    ModelState.AddModelError("From", "enter Valid Date From");

                }
                else if (record == 0)
                {
                    ModelState.AddModelError("Search", "There is No Recorded Available");

                }
                else
                {
                    var Attend = hRSystem.Attendance_Leavings.Where(e => e.Date >= MyView.From && e.Date <= MyView.To && (e.Emp.Dept.Name.Contains(MyView.Search) || e.Emp.Name.Contains(MyView.Search))).Include(e => e.Emp).Include(e => e.Emp.Dept).ToList();
                    return View(Attend);
                }


            }
            return View("AttendanceBySearch",MyView);
        }

        public IActionResult AttendanceBySearch()
        {
            return View();
        }


        public IActionResult Delete(int id)
        {

            repAttend.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)

        {
            
            ViewData["Emp"] = repEmp.GetAll();

            var Attend = repAttend.GetById(id);

            return View(Attend);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(int id, Attendance_Leaving EditAttend)
        {
            var empAttendTime = TimeSpan.Parse(hRSystem.Employees.Where(e => e.ID == EditAttend.Emp_ID).Select(e => e.AttendanceTime).First());
            int ArriveHour = empAttendTime.Hours;
            var empLeaveTime = TimeSpan.Parse(hRSystem.Employees.Where(e => e.ID == EditAttend.Emp_ID).Select(e => e.LeavingTime).First());
            var LeaveHour = empLeaveTime.Hours;
          
var previousAttend = hRSystem.Attendance_Leavings.Where(a => a.Date == EditAttend.Date && a.Emp_ID == EditAttend.Emp_ID).Count();


            var newattend = TimeSpan.Parse(EditAttend.AttendanceTime);
            int AttendHour = newattend.Hours;
            if (ModelState.IsValid == true)
            {
                if (AttendHour < ArriveHour)
                {

                    ModelState.AddModelError("AttendanceTime", $"AttendanceTime can't be less than {ArriveHour}");

                }
                else if (TimeSpan.Parse(EditAttend.AttendanceTime).Hours >= TimeSpan.Parse(EditAttend.LeavingTime).Hours)
                {
                    ModelState.AddModelError("AttendanceTime", "AttendanceTime Cant be greater than Leaving Time or Equal");

                }
                else if (TimeSpan.Parse(EditAttend.LeavingTime).Hours < LeaveHour)
                {
                    ModelState.AddModelError("LeavingTime", $"LeavingTime can't be less than {LeaveHour}");

                }
                else if (previousAttend != 0)
                {
                    ModelState.AddModelError("Date", $"This Date already recorded ");

                }
                else
                {
                    repAttend.Update(id, EditAttend);
                    return RedirectToAction("Index");
                }
            }
            ViewData["Emp"] = repEmp.GetAll();


            return View("Edit", EditAttend);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Attendance_Leaving Newobj)
        {
            var empAttendTime = TimeSpan.Parse(hRSystem.Employees.Where(e=>e.ID==Newobj.Emp_ID).Select(e => e.AttendanceTime).First());
            int ArriveHour = empAttendTime.Hours;
            var empLeaveTime = TimeSpan.Parse(hRSystem.Employees.Where(e => e.ID == Newobj.Emp_ID).Select(e => e.LeavingTime).First());
            var LeaveHour = empLeaveTime.Hours;

            var newattend = TimeSpan.Parse(Newobj.AttendanceTime);
            int AttendHour = newattend.Hours;
          
var previousAttend = hRSystem.Attendance_Leavings.Where(a => a.Date == Newobj.Date && a.Emp_ID == Newobj.Emp_ID).Count();


            if (ModelState.IsValid == true)
            {
                if(AttendHour < ArriveHour)
                {
                    
                    ModelState.AddModelError("AttendanceTime", $"AttendanceTime can't be less than {ArriveHour}");

                }
                else if (TimeSpan.Parse(Newobj.AttendanceTime).Hours >= TimeSpan.Parse(Newobj.LeavingTime).Hours)
                {
                    ModelState.AddModelError("AttendanceTime", "AttendanceTime Cant be greater than Leaving Time or Equal");

                }
                else if(TimeSpan.Parse(Newobj.LeavingTime).Hours< LeaveHour)
                {
                    ModelState.AddModelError("LeavingTime", $"LeavingTime can't be less than {LeaveHour}");

                }
                else if (previousAttend != 0)
                {
                    ModelState.AddModelError("Date", $"This Date already recorded ");

                }
                else
                {
                    repAttend.Insert(Newobj);
                    return RedirectToAction("Index");
                }


            }
            ViewData["Emp"] = repEmp.GetAll();

            return View("New", Newobj);
        }

        public IActionResult New()
        {
            ViewData["Emp"] = repEmp.GetAll();
            return View();
        }
    }
}
