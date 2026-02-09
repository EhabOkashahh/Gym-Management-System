using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Data.Configurations;
using GymSystemDAL.Entities;

namespace GymSystem.DAL.Entities
{
    public class Member : GymUser
    {
        // JoinDATE == CreatdAt
        public string? Photo { get; set; }
        public virtual HealthRecord healthRecord { get; set; } = null!;


        //relationships
        public virtual ICollection<MemberSessions> MemberSessions { get; set; } = null!;



        public virtual MemberShip MemberShip { get; set; } = null!;
        public int MemberShipID { get; set; }
        
    }
}
