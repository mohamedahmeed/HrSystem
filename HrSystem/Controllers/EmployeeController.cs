using GraduationProject.Models;
using GraduationProject.Services;
using HrSystem.services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HrSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee>employeeService;
        private readonly IRepository<Department>departmentService;

        public EmployeeController(IRepository<Employee> employeeService, IRepository<Department> departmentService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
        }
        public IActionResult Index()
        {
         List<Employee> employees=employeeService.GetAll();
            return View(employees);
        }

        public IActionResult Details(int id)
        {
            Employee employee=employeeService.GetById(id);
            return View(employee);
        }
       
        public IActionResult Add()
        {
         var d= departmentService.GetAll();
            ViewBag.d = d;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Employee e)
        {
            if (ModelState.IsValid)
            {
                
               if (e.ContractDate.Year > System.DateTime.Now.Year)
                {
                    ModelState.AddModelError("ContractDate", "Hiring Date cannot be greater than current years");
               
                }
                else
                {

                    employeeService.Insert(e);
                    return RedirectToAction("Index");
                }
              
               


            }
            var d = departmentService.GetAll();
            ViewBag.d = d;
            return View(e);
        }

        public IActionResult Edit(int id ) 
        {
            var d = departmentService.GetAll();
            ViewBag.d = d;
            Employee employee = employeeService.GetById(id);
            return View(employee); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Employee e)
        {
            if (ModelState.IsValid)
            {
                if (e.ContractDate.Year > System.DateTime.Now.Year)
                {
                    ModelState.AddModelError("ContractDate", "Hiring Date cannot be greater than current years");

                }
                else
                {
                    employeeService.Update(id, e);
                    return RedirectToAction("Index");
                }
            }
            var d = departmentService.GetAll();
            ViewBag.d = d;
            return View("Edit", e);
        }

        public IActionResult Delete(int id)
        {
             employeeService.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult search(string SearchString)
        {

            if (!string.IsNullOrEmpty(SearchString))
            {
                Employee emp = employeeService.GetByName(SearchString);
                return View(emp);
            }
            return RedirectToAction("index");


        }
    }
}
