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
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class MemberShipController(IMemberShipService _memberShipService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var MS = await _memberShipService.GetAllMemberShipAsync();
            return View(MS);
        }
        public async Task<IActionResult> ToggleCancelMemberShip(int? id)
        {
            if (id is null)
                return BadRequest();

            var result = await  _memberShipService.ToggleCancelMemberShip(id.Value);

            if (!result)
                return NotFound();

            return RedirectToAction("Index"); 
        }

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