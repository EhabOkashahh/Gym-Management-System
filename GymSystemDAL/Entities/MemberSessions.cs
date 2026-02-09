using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;

namespace GymSystemDAL.Entities
{
    public class MemberSessions : BaseEntity
    {
        public virtual Member Member { get; set; } = null!;
        public int MemberID { get; set; }

        public virtual Session Session { get; set; } = null!;
        public int SessionID { get; set; }

        public DateTime BookingDay { get; set; }
        public bool IsAttended { get; set; }
    }
}