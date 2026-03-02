using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.Extension;
using GymSystem.Extension.Classes;
using GymSystemBLL.Models;
using GymSystemBLL.Models.MemberModels;
using GymSystemBLL.Services.Classes;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GymSystem.Controllers
{
    public class MemberController(IMemberService _memberService , FilesFactory _fileFactory ,IPlanService _planService, IMemberShipService _memberShip , IMapper _mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var Members = await _memberService.GetAllMembersAsync();
            return View(Members);
        }

        #region Create Member
        [HttpGet]
        public async Task<IActionResult> AddMemberAsync() { 
            var model = new CreateMemberModelView();
            await PopulatePlans(model);
            return View(model); 
        }
    

        [HttpPost]
        public async Task<IActionResult> AddMember(CreateMemberModelView model)
        {
            if(ModelState.IsValid){
                if(model.PhotoFile is not null) {

                        // Upload PhotoFile
                        var UploadedFile = _fileFactory.GetFileUploader(model.PhotoFile.ContentType);
                        model.Photo = await UploadedFile.UploadFile(model.PhotoFile);
                    } else model.Photo = "Default.png";

                    
                    // Add Member
                    var res = await _memberService.CreateMemberAsync(model);

                    // Creation Failed
                    if (!res) 
                    {
                        ViewData["CreationFailed"] = true;
                        await PopulatePlans(model);
                        return View(model);
                    }

                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(String.Empty , "There are some fields Empty");
            await PopulatePlans(model);
            return View(model);
        } 
        #endregion   

        #region Details - HealthRecords
        [HttpGet]
        public async Task<IActionResult> MemberDetails(int? id)
        {
            var res = await _memberService.GetMemberByIdAsync(id);
            var membership = await _memberShip.GetMemberShipDetails(res!.MemberShipID);
            res.MemberShip = membership;
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> ViewHealthRecordData(int? Id)
        {
            var res = await _memberService.GetHealthRecordDetails(Id);
            return View(res);
        }

        #endregion

        #region Edit - Delete
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var res = await _memberService.GetMemberByIdAsync(Id);
            var mappedModel = _mapper.Map<UpdateMemberModelView>(res);
            mappedModel.PlanID = res!.MemberShip.PlanID;
            await PopulatePlans(mappedModel);
            return View(mappedModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateMemberModelView model)
        {
            if (!ModelState.IsValid)
            {   
                ModelState.AddModelError(string.Empty,"Some fields are empty");
                await PopulatePlans(model);
                return View(model);
            }

            var res = await _memberService.UpdateMember(model.Id, model);

            if (!res)
            {
                ViewData["CreationFailed"] = true;
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteMemberAsync(int? Id)
        {
            if (Id is null)
            {
                TempData["DeletionError"] = "Invalid Member ID. Please try again.";
                return RedirectToAction(nameof(Index));
            }

            var res = await _memberService.SoftDeleteMember(Id.Value);

            if (!res)
            {
                TempData["DeletionError"] = "Something went wrong while deleting. Try again later.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Member deleted successfully!";
           return RedirectToAction(nameof(Index));
        }


        #endregion 

        
        public async Task<IActionResult> RestoreMember(int id)
        {
            var res = await _memberService.RestoreMember(id);
            if(!res) TempData["DeletionError"] = "Something went wrong while deleting. Try again later.";
            
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulatePlans<T>(T model) where T : IHasPlan
        {
            model.Plans = await _planService.GetAllPlans();
        }
    }
}