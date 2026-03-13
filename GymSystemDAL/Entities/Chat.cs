using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Entities.Enums;

namespace GymSystemDAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string UserID { get; set; } = null!;

        [ForeignKey("UserID")]
        public virtual AppUser User { get; set; } = null!;


        public virtual IEnumerable<Message> Messages { get; set; } = null!;

        public Status Status { get; set; }        

        public DateTime CreatedAt { get; set; }
    }
}   