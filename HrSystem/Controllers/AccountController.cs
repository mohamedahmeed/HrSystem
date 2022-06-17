using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using HrSystem.ViewModel;

namespace Cheri_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel newuser)
        {
            if (ModelState.IsValid == true)
            {
                IdentityUser userModel = new IdentityUser();
                userModel.UserName = newuser.username;
                userModel.PasswordHash = newuser.password;
                userModel.Email = newuser.email;
                IdentityResult result = await userManager.CreateAsync(userModel, newuser.password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(newuser);

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel myuser)
        {
            if (ModelState.IsValid == true)
            {
                IdentityUser user = await userManager.FindByNameAsync(myuser.username);
                if (user != null)
                {
                    SignInResult res = await signInManager.PasswordSignInAsync(user, myuser.password, myuser.RememberMe, false);
                    if (res.Succeeded)
                    {
                        return RedirectToAction("Index", "Department");
                   
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid password!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password!");
                }

            }
            return View(myuser);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult AddAdmin()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(RegisterViewModel newUser)
        {

            if (ModelState.IsValid == true)
            {
                //save object db
                IdentityUser userModel = new IdentityUser();
                userModel.UserName = newUser.username;
                userModel.PasswordHash = newUser.password;
                userModel.Email = newUser.email;

                IdentityResult result = await userManager.CreateAsync(userModel, newUser.password);//hash passwor
                if (result.Succeeded)
                {
                    //enroll in role
                    await userManager.AddToRoleAsync(userModel, "Admin");
                    //save sucess
                    await signInManager.SignInAsync(userModel, false);//cookie
                    return Redirect("");
                }
                else
                {
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);
                    }
                }
            }
            return View(newUser);

        }

        public IActionResult Index()
        {
            return View();
        }

    }

}