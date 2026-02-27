using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystemBLL.Models.PlanModels;
using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Controllers
{
    public class PlanController(IPlanService _PlanService , IMapper _mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var Plans = await _PlanService.GetAllPlans();
            return View(Plans);
        }

        public async Task<IActionResult> PlanDetails(int? Id)
        {
            if(Id is null)
            {
                TempData["ErrorMessage"] = "Someething went wrong, Try again later";
                return RedirectToAction(nameof(Index));
            }
            var plan = await _PlanService.GetPlanDetails(Id);
            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int? Id)
        {
            if(Id is null) TempData["ErrorMessage"] = "Something went wrong, Try again later";
            

            var res = await _PlanService.TogglePlanActiveStatus(Id!.Value);

            if (!res) TempData["ErrorMessage"] = "Something went wrong (maybe there are some members assigned to this plan), Try again later";
            else TempData["SuccessMessage"] = "Plan status updated successfully";
            
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> PlanEdit(int? id)
        {
            if(id is null) TempData["ErrorMessage"] = "Something went wrong, Try again later";
            var plan = await _PlanService.GetPlanDetails(id!.Value);
            var MappedModel = _mapper.Map<UpdatePlanModelView>(plan) ;
            return View(MappedModel);
        }
        [HttpPost]
        public async Task<IActionResult> PlanEdit(int? id , UpdatePlanModelView model)
        {
            if(id is null) TempData["ErrorMessage"] = "Something went wrong, Try again later";

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty,"Some Fields are empty");
                return View(model);    
            }

            var res = await _PlanService.UpdatePlanData(id!.Value,model);
            
            if (!res)
            {
                TempData["ErrorMessage"] = "Something went wrong, Try again later";
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PlanMembers(int? id)
        {
            if(id is null) TempData["ErrorMessage"] = "Something went wrong, Try again later";

            var plan = await _PlanService.GetPlanDetails(id!.Value);
            if(plan is null) TempData["ErrorMessage"] = "Something went wrong, Try again later";

            var members = plan!.MemberShip.Where(ms => ms.MemberShipStatus == GymSystemDAL.Entities.Enums.MemberShipStatus.Canceled).SelectMany(ms => ms.Members).ToList();

            return PartialView("_PlanMembersPartial",members);
        }
    }
}