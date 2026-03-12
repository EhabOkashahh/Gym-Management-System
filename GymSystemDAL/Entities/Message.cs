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
        public bool IsRead { get; set; }

        public string SenderId { get; set; } = null!;
        public virtual AppUser Sender { get; set; } = null!;
        public string? ReceiverId { get; set; }
        public virtual AppUser Receiver { get; set; } = null!;
    }
}