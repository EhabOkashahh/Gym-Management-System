using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Controllers
{
    public class MemberShipController(IMemberShipService _memberShipService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var MS = await _memberShipService.GetAllMemberShipAsync();
            return View(MS);
        }
    }
}