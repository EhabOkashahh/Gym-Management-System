using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Entities.Enums;

namespace GymSystemDAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public virtual AppUser User { get; set; } = null!;
        public int UserId { get; set; }

        public IEnumerable<Message> Messages { get; set; } = null!;

        public Status Status { get; set; }        

        public DateTime CreatedAt { get; set; }
    }
}   