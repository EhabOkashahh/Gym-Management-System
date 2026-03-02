using System.Threading.Tasks;
using AutoMapper;
using GymSystemBLL.Models.TrainerModels;
using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Controllers
{
    public class TrainerController(ITrainerService _trainerService, IMapper _mapper) : Controller
    {
        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trainers = await _trainerService.GetAllTrainersAsync();
            return View(trainers);
        }
        #endregion

        #region Create Trainer
        [HttpGet]
        public IActionResult AddTrainer()
        {
            var model = new CreateTrainerModelView();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainer(CreateTrainerModelView model)
        {
            if (ModelState.IsValid)
            {
                var res = await _trainerService.CreateTrainerAsync(model);
                if (!res)
                {
                    ViewData["CreationFailed"] = true;
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "There are some fields empty");
            return View(model);
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var trainer = await _trainerService.GetTrainerByIdAsync(id);
            if (trainer is null) return NotFound();

            return View(trainer);
        }
        #endregion

        #region Edit / Update
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var trainer = await _trainerService.GetTrainerByIdAsync(id);
            if (trainer is null) return NotFound();

            var model = _mapper.Map<UpdateTrainerModelView>(trainer);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTrainerModelView model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Some fields are empty");
                return View(model);
            }

            var res = await _trainerService.UpdateTrainerAsync(model.Id, model);
            if (!res)
            {
                ViewData["UpdateFailed"] = true;
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (id is null)
            {
                TempData["DeletionError"] = "Invalid Trainer ID. Please try again.";
                return RedirectToAction(nameof(Index));
            }

            var res = await _trainerService.SoftDeleteTrainerAsync(id);
            if (!res)
            {
                TempData["DeletionError"] = "Something went wrong while deleting. Try again later.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Trainer deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RestoreTrainer(int id)
        {
            var res = await _trainerService.RestoreTrainer(id);
            if(!res) TempData["DeletionError"] = "Something went wrong while deleting. Try again later.";
            
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}