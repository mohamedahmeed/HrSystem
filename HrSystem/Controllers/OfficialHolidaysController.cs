using GraduationProject.Models;
using GraduationProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Authorize]

    public class OfficialHolidaysController : Controller
    {
        private readonly IRepository<OfficialHolidays> repholi;
        private readonly HRSystem hRSystem;

        public OfficialHolidaysController(IRepository<OfficialHolidays> RepHoli, HRSystem hRSystem)
        {
            repholi = RepHoli;
            this.hRSystem = hRSystem;
        }
        public IActionResult Index()
        {
            var Holidays=repholi.GetAll();
            return View(Holidays);
        }

        public IActionResult Delete(int id)
        {

            repholi.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {

            var Dept = repholi.GetById(id);

            return View(Dept);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(int id, OfficialHolidays newDept)
        {

            if (ModelState.IsValid == true)
            {
                repholi.Update(id, newDept);
                return RedirectToAction("Index");
            }

            return View("Edit", newDept);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(OfficialHolidays Newobj)
        {

            if (ModelState.IsValid == true)
            {
                repholi.Insert(Newobj);
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
