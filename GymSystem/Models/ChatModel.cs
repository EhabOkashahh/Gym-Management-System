using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Entities;

namespace GymSystem.Models
{
    public class ChatModel
    {
        public IEnumerable<Chat> Chats { get; set; } = null!;
        public int ChatId { get; set; }
    }
}