using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Entities;

namespace GymSystemBLL.Models.PlanModels
{
    public class PlanModelView
    {
        public int Id { get; set; }
        public string Name{ get; set; } = null!;
        public string Description{ get; set; } = null!;
        public int DurationDays{ get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<MemberShip> MemberShip { get; set; } = null!;
    }
}