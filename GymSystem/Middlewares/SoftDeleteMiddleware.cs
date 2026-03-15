using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.Middlewares
{
    public class SoftDeleteMiddleWare(RequestDelegate _next)
    {
        public async Task InvokeAsync(
            HttpContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            AppDbContext db)
        {
            if (context.User.Identity?.IsAuthenticated ?? false)
            {

                if (context.User != null)
                {
                    var member = db.Members.Where(m => m.Name.Trim().ToLower() == context.User.Identity.Name.Trim().ToLower()).Select(m => new { m.IsDeleted}).AsNoTracking().FirstOrDefault();

                    // Only check soft delete if this user is a member
                    if (member != null && member.IsDeleted)
                    {
                        await signInManager.SignOutAsync();
                        context.Response.Redirect("/Account/Login");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}