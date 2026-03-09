using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Controllers
{
    public class MemberShipController(IMemberShipService _memberShipService) : Controller
    {
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var MS = await _memberShipService.GetAllMemberShipAsync();
            return View(MS);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> ToggleCancelMemberShip(int? id)
        {
            if (id is null)
                return BadRequest();

            var result = await  _memberShipService.ToggleCancelMemberShip(id.Value);

            if (!result)
                return NotFound();

            return RedirectToAction("Index"); 
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> RenewMemberShip(int id)
        {
            var result = await _memberShipService.RenewMemberShip(id);

            if (!result)
                return NotFound();

            return RedirectToAction("Index");

        }
    }
}