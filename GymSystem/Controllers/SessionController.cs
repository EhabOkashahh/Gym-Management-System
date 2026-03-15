using System.Threading.Tasks;
using AutoMapper;
using GymSystemBLL.Models;
using GymSystemBLL.Models.SessionModels;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.Controllers
{
    public class SessionController(ISessionService _sessionService, IMapper _mapper, ITrainerService _trainerService, AppDbContext _context,UserManager<AppUser> _userManager,IMemberSessionService _memberSessionsService) : Controller
    {

        [Authorize(Roles = "Admin,SuperAdmin")]
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var now = DateTime.Now;
            var sessions = await _context.Sessions
                .Include(s => s.MemberSessions)
                .Select(s => new SessionModelView
                {
                    Id = s.Id,
                    Description = s.Description,
                    Capacity = s.Capacity,
                    ReservedSeats = s.ReservedSeats,
                    TrainerName = s.Trainer.Name,
                    DateDisplay = s.StartDate.ToString("dd MMM yyyy"),
                    TimeRangeDisplay = $"{s.StartDate:HH:mm} - {s.EndDate:HH:mm}",
                    Duration = s.EndDate - s.StartDate,
                    IsEnrolled = s.MemberSessions.Any(ms => ms.Member.UserId == user!.Id),
                    CategoryName = s.Category.Name,
                    Status = now < s.StartDate
                    ? "Upcoming"
                    : DateTime.Now > s.EndDate
                        ? "Completed"
                        : "Ongoing"
                }).ToListAsync();   
            return View(sessions);
        }
        #endregion


        #region Create Session
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateSessionModelView();
            await PopulateTrainers(model);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionModelView model)
        {
            if (ModelState.IsValid)
            {
                if((model.StartDate - model.EndDate).TotalSeconds == 0)
                {
                    ModelState.AddModelError("EndDate", "Total Duration Must be at least 1 Hour");
                    await PopulateTrainers(model);
                    return View(model);
                }

                if (model.StartDate > model.EndDate) {
                    ModelState.AddModelError("EndDate", "End date must be after start date.");
                    await PopulateTrainers(model);
                    return View(model);
                } 
                var res = await _sessionService.CreateSessionAsync(model);

                if (!res)
                {
                    ViewData["CreationFailed"] = true;
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "There are some fields empty");
            await PopulateTrainers(model);
            return View(model);
        }
        #endregion


        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session is null) return NotFound();

            return View(session);
        }
        #endregion


        #region Edit / Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session is null) return NotFound();

            var model = _mapper.Map<UpdateSessionModelView>(session);
            await PopulateTrainers(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSessionModelView model)
        {
            if (model.StartDate > model.EndDate)
            {
                ModelState.AddModelError("EndDate", "End date must be after start date.");
                await PopulateTrainers(model);
                return View(model);
            }

            if ((model.EndDate - model.StartDate).TotalHours < 1)
            {
                ModelState.AddModelError("EndDate", "Total Duration must be at least 1 hour.");
                await PopulateTrainers(model);
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                await PopulateTrainers(model);
                return View(model);
            }

            var res = await _sessionService.UpdateSessionAsync(model.Id, model);

            if (!res)
            {
                ViewData["UpdateFailed"] = true;
                await PopulateTrainers(model);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                TempData["DeletionError"] = "Invalid Session ID. Please try again.";
                return RedirectToAction(nameof(Index));
            }

            var res = await _sessionService.DeleteSessionAsync(id);

            if (!res)
            {
                TempData["DeletionError"] = "Something went wrong while deleting. Try again later.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Session deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Enroll(int sessionId)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _memberSessionsService.EnrollAsync(user!.Id, sessionId);

            if (!result.IsSuccessed)
            {
                TempData["EnrollError"] = result.Message;
                return RedirectToAction("Index");
            }

            TempData["EnrollSuccess"] = "You have successfully enrolled!";
            return RedirectToAction("Details", new { id = sessionId });
        }
        [HttpPost]
        public async Task<IActionResult> Withdraw(int sessionId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) TempData["EnrollError"] = "Somthing Went wrong, try again later";
            var result = await _memberSessionsService.WithdrawAsync(user!.Id, sessionId);

            if (!result.IsSuccessed)
                TempData["EnrollError"] = result.Message;
            else
                TempData["EnrollSuccess"] = "You have successfully withdrawn from the session.";

            return RedirectToAction("Index");
        }
        
        private async Task PopulateTrainers<T>(T model) where T : IPopuateNaviagtionsProp
        {
            model.Trainers = await _trainerService.GetAllTrainersAsync();
            model.Category = _mapper.Map<IEnumerable<CategoryModelView>>(_context.Categories);
        }
    }
}