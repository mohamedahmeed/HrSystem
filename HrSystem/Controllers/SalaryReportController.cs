using GraduationProject.Models;
using GraduationProject.Services;
using HrSystem.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using HrSystem.ViewModel;
using System.Globalization;

namespace HrSystem.Controllers
{
    public class SalaryReportController : Controller
    {
        private IRepository<Employee> employee;
        private GraduationProject.Services.IRepository<Attendance_Leaving> attendance;
        private GraduationProject.Services.IRepository<GeneralSetting> setting;
        private HRSystem db;
        GraduationProject.Services.IRepository<OfficialHolidays> holidays;
        public SalaryReportController( IRepository<Employee> _employee,
                                      GraduationProject.Services.IRepository<OfficialHolidays> _holidays,
                                      GraduationProject.Services.IRepository<Attendance_Leaving> _attendance,
                                      GraduationProject.Services.IRepository<GeneralSetting> _setting,
                                      HRSystem _dp)
        {
            employee = _employee;
            holidays = _holidays;
            attendance = _attendance;
            setting = _setting;
            db = _dp;
        }
     
       public int CountHolidays(int month, int year)
        {
          return( holidays.GetAll().Where(x=>x.Date.Month==month&& x.Date.Year==year).Count()+8);

        }
       public int AttendanceDays(string name,int month,int year)
        {
            var countDays = db.Attendance_Leavings.Where(x => x.Emp.Name == name && x.Date.Month == month && x.Date.Year == year).Count();
            return countDays;
        }
       public int AbsenceDays(string name,int month,int year)
        {
            var x=30-((AttendanceDays(name,month,year)+CountHolidays(month,year)));
            return x;
        }
       public float DeductionHours(string name,int month, int year)
        {
            float totalDeductionHours=0;
            float deductionHours = 0;
            var emplyeeInfo= db.Employees.FirstOrDefault(x=>x.Name==name);
            var setting = db.General_Settings.FirstOrDefault();
            var daysInfo = db.Attendance_Leavings.Where(x => x.Emp.Name == name && x.Date.Month == month && x.Date.Year == year).ToList();
            foreach (var day in daysInfo)
            {
                if (TimeSpan.Parse(day.AttendanceTime).Hours > TimeSpan.Parse(emplyeeInfo.AttendanceTime).Hours)
                {
                    deductionHours +=TimeSpan.Parse(day.AttendanceTime).Hours-TimeSpan.Parse(emplyeeInfo.AttendanceTime).Hours;
                    totalDeductionHours =deductionHours * setting.Discount;
                }
            }
            return totalDeductionHours;
        }
       public float BounsHours(string name, int month, int year)
        {
            float totalBounsHours = 0;
            float BounsHours = 0;
            var emplyeeInfo = db.Employees.FirstOrDefault(x => x.Name == name);
            var setting = db.General_Settings.FirstOrDefault();
            var daysInfo = db.Attendance_Leavings.Where(x => x.Emp.Name == name && x.Date.Month == month && x.Date.Year == year).ToList();
            foreach (var day in daysInfo)
            {
                if (TimeSpan.Parse(day.LeavingTime).Hours > TimeSpan.Parse(emplyeeInfo.LeavingTime).Hours)
                {
                    BounsHours += TimeSpan.Parse(day.LeavingTime).Hours - TimeSpan.Parse(emplyeeInfo.LeavingTime).Hours;
                    totalBounsHours = BounsHours * setting.Extra;
                }
            }
            return totalBounsHours;
        }
       public float NetBouns(string name, int month, int year)
        {
            var employee = db.Employees.FirstOrDefault(x => x.Name == name);
            var daysInfo = db.Attendance_Leavings.Where(x => x.Emp.Name == name && x.Date.Month == month && x.Date.Year == year).ToList();
            var salaryPerHour = employee.Salary/( 30*(TimeSpan.Parse(employee.LeavingTime).Hours-TimeSpan.Parse(employee.AttendanceTime).Hours));
            var netBouns=salaryPerHour*BounsHours(name, month, year);

            return netBouns;
        }
       public float NetDeduction(string name, int month, int year)
        {
            var employee = db.Employees.FirstOrDefault(x => x.Name == name);
            var daysInfo = db.Attendance_Leavings.Where(x => x.Emp.Name == name && x.Date.Month == month && x.Date.Year == year).ToList();
            var salaryPerHour = employee.Salary / (30 * (TimeSpan.Parse(employee.LeavingTime).Hours - TimeSpan.Parse(employee.AttendanceTime).Hours));
            var netBouns = salaryPerHour * DeductionHours(name, month, year);

            return netBouns;
        }
       public float NetSalary(string name, int month, int year)
        {
            var employee = db.Employees.FirstOrDefault(x => x.Name == name);
            var Salary = (30-AbsenceDays(name,month,year))* (TimeSpan.Parse(employee.LeavingTime).Hours - TimeSpan.Parse(employee.AttendanceTime).Hours)* employee.Salary / (30 * (TimeSpan.Parse(employee.LeavingTime).Hours - TimeSpan.Parse(employee.AttendanceTime).Hours));
            var netSalary=(Salary-NetDeduction(name,month,year))+NetBouns(name,month,year);
            return netSalary;
        }
    

        public IActionResult GetReport()
        {
            return View();
        }
        public IActionResult CreateReport(SalaryReport report)
        {

            if (ModelState.IsValid == true)
            {
                var Check = db.Employees.Where(e => e.Name.Equals(report.Search)).Include(e => e.Dept).ToList().Count();
                var record = db.Attendance_Leavings.Where(n => n.Emp.Name == report.Search && n.Date.Month == report.Month && n.Date.Year == report.Year).Count();
                var validateGeneralSettings = db.General_Settings.Count();
                if (Check == 0)
                {
                    ModelState.AddModelError("Search", "Employee Name InValid");
                }
                else if (report.Year < 2008 || report.Year >DateTime.Now.Year)
                {
                    ModelState.AddModelError("Year", "Enter Valid Year");
                }
                else if (report.Month > DateTime.Now.Month && report.Year==DateTime.Now.Year)
                {
                    DateTime date = new DateTime(DateTime.Now.Year,report.Month,DateTime.Now.Day);

                    ModelState.AddModelError("Month", $"{date.ToString("MMMM")} greater than current Month");

                }
                else if(record == 0)
                {
                    ModelState.AddModelError("Search"," No available records here!");
                }
                else if(validateGeneralSettings==0)
                {
                    ModelState.AddModelError("Search", "Kindly add the general settings fisrt");
                }

                else
                {
                    ViewData["emp"] = db.Employees.Where(e => e.Name.Equals(report.Search)).Include(e => e.Dept).ToList();
                    report.AttendanceDayNumbers = AttendanceDays(report.Search, report.Month, report.Year);
                    report.AbsenceDayNumbers = AbsenceDays(report.Search, report.Month, report.Year);
                    report.ExtraHours = BounsHours(report.Search, report.Month, report.Year);
                    report.DeductionHours = DeductionHours(report.Search, report.Month, report.Year);
                    report.TotalBonus = NetBouns(report.Search, report.Month, report.Year);
                    report.TotalDeduction = NetDeduction(report.Search, report.Month, report.Year);
                    report.NetSalary = NetSalary(report.Search, report.Month, report.Year);
                    return View(report);
                }
            }
            return View("GetReport",report);
        }
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult Details(SalaryReport report)
        {
            ViewData["Emp"] = db.Employees.Where(e => e.Name.Equals(report.Search)).Include(e => e.Dept).FirstOrDefault();
            var Attend = db.Attendance_Leavings.Where(e => e.Date.Month == report.Month && e.Date.Year == report.Year && e.Emp.Name.Equals(report.Search)).ToList();
            ViewData["General"] = db.General_Settings.FirstOrDefault();
            ViewData["Holidays"] = db.OfficialHolidays.Where(e => e.Date.Month == report.Month && e.Date.Year == report.Year).ToList();
            ViewData["Rep"] = report;
            return View(Attend);
        }
      
    }
}
