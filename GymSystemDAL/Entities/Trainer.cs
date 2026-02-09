using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entites.Enums;

namespace GymSystem.DAL.Entities
{
    public class Trainer : GymUser
    {
         // HiringDate == CreatdAt
        public virtual Specialities Specialities { get; set; }


        // relationships
        public virtual ICollection<Session> Sessions { get; set; } = null!;
    }
}