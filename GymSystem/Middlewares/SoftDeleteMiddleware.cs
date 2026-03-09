using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymSystem.Middlewares
{
    public class SoftDeleteMiddleWare(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext _context)
        {
            if (context.User.Identity?.IsAuthenticated ?? false)
            {
                var user = await userManager.GetUserAsync(context.User);
                var member = _context.Members.FirstOrDefault(m => m.UserId == user!.Id);
                if (member != null && member.IsDeleted)
                {
                    await signInManager.SignOutAsync();

                    context.Response.Redirect("/Account/Login");
                    return; 
                }
            }

            await _next(context);
        }
    }
}