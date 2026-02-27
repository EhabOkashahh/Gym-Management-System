using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models.PlanModels;
using GymSystemDAL.Entities.Enums;

namespace GymSystemBLL.Models.MeemberShipModels
{
    public class MemberShipModelView
    {
        public int Id { get; set; }

        [Display(Name = "Membership start")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Membership End")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Membership CreatedAt")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Membership UpdatedAt")]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

        public ICollection<Member> Members { get; set; } = null!;

        public string status =>  EndDate < DateTime.Now ? "Expired" : "Active";

        public bool IsCanceled { get; set; } = false;
        public MemberShipStatus MemberShipStatus {get; set;}
        
        public MemberShipStatus ComputedStatus =>
        MemberShipStatus == MemberShipStatus.Canceled
        ? MemberShipStatus.Canceled
        : DateTime.Today > EndDate
            ? MemberShipStatus.Expired
            : MemberShipStatus.Active;
        public int PlanID { get; set; }
        public PlanModelView Plan { get; set; } = null!;
    }

}