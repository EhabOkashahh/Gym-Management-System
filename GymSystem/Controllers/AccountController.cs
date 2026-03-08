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
            if (!ModelState.IsValid) return View(model);

            var User = await _accountService.ValidateUser(model);
            if (User is null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
                return View(model);
            }

            var Result = await _SignInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, true);
            if (Result.IsLockedOut) ModelState.AddModelError("InvalidLogin", "Account Is Locked Try Again Later.");
            if (Result.IsNotAllowed) ModelState.AddModelError("InvalidLogin", "Your are not Allowed to Login.");

            if (Result.Succeeded) return RedirectToAction("Index", "Home");

            ModelState.AddModelError("InvalidLogin", "Something went wrong while logging you in, Try again later");
            return View(model);

        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}