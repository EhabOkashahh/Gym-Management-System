using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.UserModelView;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymSystemBLL.Services.Classes
{
    public class AccountService(UserManager<AppUser> _userManager) : IAccountService
    {
        public async Task<AppUser?> ValidateUser(LoginModelView model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return null;

            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            
            return IsPasswordValid ? user : null;
        }
    }
}