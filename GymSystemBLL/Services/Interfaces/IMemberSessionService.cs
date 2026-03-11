using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymSystemBLL.Services.Interfaces
{
    public interface IMemberSessionService
    {
        Task<EnrollResult> EnrollAsync(string UserId,int sessionId);
        Task<EnrollResult> WithdrawAsync(string UserId,int sessionId);
    }
}