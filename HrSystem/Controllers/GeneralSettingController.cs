using GraduationProject.Models;
using GraduationProject.Services;
using HrSystem.services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HrSystem.Controllers
{
    public class GeneralSettingController : Controller
    {
        private GraduationProject.Services.IRepository<GeneralSetting> services;
        public GeneralSettingController(GraduationProject.Services.IRepository<GeneralSetting> _services )
        {
            services = _services;
        }

        public bool flag;
        public IActionResult Index()
        {
         

            List<GeneralSetting> settings = services.GetAll();
            return View(settings);
        }


        public IActionResult Create()
        {
            ViewData["SettingsCount"] = services.GetAll().Count;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GeneralSetting gs)
        {
            if (ModelState.IsValid ==true)
            {
                int settingsNumber = services.GetAll().Count;

                    if (settingsNumber==0  )
                    {

                        TempData["Success"] = "Success";
                        services.Insert(gs);
                        return RedirectToAction("Index");

                    }
                
                
                else
                {
                    TempData["error"] = "Can't Created";


                }
            }
            return View("Create",gs);
        }

        public IActionResult Edit(int id)
        {
            GeneralSetting setting = services.GetById(id);
            return View(setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,GeneralSetting gs)
        {
            if (ModelState.IsValid)
            {
                services.Update(id,gs);
                return RedirectToAction("Index");

            }
            return View("Edit",gs);
        }

        public IActionResult Delete(int id)
        {
            services.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            GeneralSetting setting = services.GetById(id);
            return View(setting);
        }

    }
}
