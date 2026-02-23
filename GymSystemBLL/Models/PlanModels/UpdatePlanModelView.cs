using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystemBLL.Models.PlanModels
{
    public class UpdatePlanModelView
    {
        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(30,MinimumLength = 3,ErrorMessage = "Name must be between 3 and 30 char")]
        [RegularExpression(@"^[a-zA-Z\s]+$",ErrorMessage = "Name Must Contain Only letters or spaces")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Description Is Required")]
        [StringLength(55,MinimumLength = 3,ErrorMessage = "Description must be between 10 and 100 char")]
        public string Description { get; set; } = null!;



        [Required(ErrorMessage = "DurationDays Is Required")]
        [Range(1,365,ErrorMessage = "DurationDays must be between 1 and 365 days")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "DurationDays Is Required")]
        public decimal Price { get; set; }

    }
}