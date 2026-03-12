using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystemDAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }

        public string SenderId { get; set; } = null!;
        public virtual AppUser Sender { get; set; } = null!;


        public virtual Chat Chat { get; set; } = null!;
        public int ChatID { get; set; }
    }
}