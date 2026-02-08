using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;

namespace GymSystemDAL.Entities
{
    public class MemberShip : BaseEntity
    {
        public ICollection<Member> Members { get; set; } = null!;

        public Plan Plan { get; set; } = null!;
        public int PlanID { get; set; }

        public DateTime EndDate { get; set; }
        public string status =>  EndDate < DateTime.Now ? "Expired" : "Active";
    }
}