using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using GymSystemDAL.Entities.Enums;

namespace GymSystemDAL.Entities
{
    public class MemberShip : BaseEntity
    {
        public virtual ICollection<Member> Members { get; set; } = null!;

        public virtual Plan Plan { get; set; } = null!;
        public int PlanID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MemberShipStatus MemberShipStatus {get; set;}
        
    }
}