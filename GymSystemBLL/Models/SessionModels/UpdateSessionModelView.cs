using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.TrainerModels;

namespace GymSystemBLL.Models.SessionModels
{
    public class UpdateSessionModelView : IPopuateNaviagtionsProp
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
		[StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
		public string Description { get; set; } = null!;

		[Required(ErrorMessage = "Start date is required")]
		[Display(Name = "Start Date & Time")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "End date is required")]
		[Display(Name = "End Date & Time")]
		public DateTime EndDate { get; set; }

		[Required(ErrorMessage = "Trainer is required")]
		[Display(Name = "Trainer")]
		public int TrainerId { get; set; }
		[Required(ErrorMessage = "Category is required")]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }
        public IEnumerable<CategoryModelView>? Category { get; set; }
        public IEnumerable<TrainerModelView>? Trainers { get; set; }
    }
}