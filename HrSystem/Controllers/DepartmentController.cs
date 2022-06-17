using GraduationProject.Models;
using GraduationProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {
        private readonly IRepository<Department> repDept;
        private readonly HRSystem hRSystem;

        public DepartmentController(IRepository<Department> RepDept, HRSystem hRSystem)
        {
            repDept = RepDept;
            this.hRSystem = hRSystem;
        }
        public IActionResult Index()
        {
            var AllDepartment = repDept.GetAll();
            return View(AllDepartment);
        }


        public IActionResult Delete(int id)
        {

            repDept.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {

            var Dept = repDept.GetById(id);

            return View(Dept);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(int id, Department newDept)
        {

            if (ModelState.IsValid == true)
            {
                repDept.Update(id, newDept);
                return RedirectToAction("Index");
            }

            return View("Edit", newDept);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Department Newobj)
        {

            if (ModelState.IsValid == true)
            {
                repDept.Insert(Newobj);
                return RedirectToAction("Index");

            }
            return View("New", Newobj);
        }

        public IActionResult New()
        {
            return View();
        }
    }
}
