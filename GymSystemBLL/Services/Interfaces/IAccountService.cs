using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.UserModelView;
using GymSystemDAL.Entities;

namespace GymSystemBLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AppUser?> ValidateUser(LoginModelView model);
    }
}