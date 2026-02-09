using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Entities;

namespace GymSystem.DAL.Entities
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }


        // relationships
        #region Category 1--* Session
            public virtual Category Category { get; set; } = null!;
            public int CategoryID { get; set; }
        #endregion
        
        #region Trainer 1--* Session
            public virtual Trainer Trainer { get; set; } = null!;
            public int TrainerID { get; set; }
        #endregion
        
        #region Member *--* Session
            public virtual ICollection<MemberSessions> MemberSessions { get; set; } = null!;
        #endregion
        
    }
}