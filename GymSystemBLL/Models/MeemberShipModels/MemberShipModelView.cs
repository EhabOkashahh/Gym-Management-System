using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.PlanModels;

namespace GymSystemBLL.Models.MeemberShipModels
{
    public class MemberShipModelView
    {
        public int Id { get; set; }

        [Display(Name = "Membership End")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Membership start")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Membership UpdatedAt")]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

        public int PlanID { get; set; }
        public PlanModelView Plan { get; set; } = null!;
    }

}