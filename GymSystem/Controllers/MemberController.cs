using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.Extension;
using GymSystem.Extension.Classes;
using GymSystemBLL.Models;
using GymSystemBLL.Services.Classes;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Controllers
{
    public class MemberController(IMemberService _memberService , FilesFactory _fileFactory ,IPlanService _planService, IMemberShipService _memberShip) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var Members = await _memberService.GetAllMembersAsync();
            return View(Members);
        }

        #region Create Member
        [HttpGet]
        public async Task<IActionResult> AddMemberAsync() { 
            var model = new CreateMemberModelView
            {
               Plans = await _planService.GetAllPlans()
            };
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
                        return View(model);
                    }

                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(String.Empty , "There are some fields Empty");
            return View(model);
        } 
        #endregion   


        #region Details
        [HttpGet]
        public async Task<IActionResult> MemberDetails(int? id)
        {
            var res = await _memberService.GetMemberByIdAsync(id);
            var membership = await _memberShip.GetMemberShipDetails(res.MemberShipID);
            res.MemberShip = membership;
            return View(res);
        }

        #endregion
    }
}