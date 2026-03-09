using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.UserModelView;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GymSystem.Controllers
{
    public class AccountController(IAccountService _accountService, SignInManager<AppUser> _SignInManager) : Controller
    {


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelView model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _accountService.ValidateUser(model);

            if (user is null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
                return View(model);
            }

            var result = await _SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("InvalidLogin", "Account is locked.");
                return View(model);
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("InvalidLogin", "Login not allowed.");
                return View(model);
            }

            ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
            return View(model);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}