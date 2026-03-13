using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.Models;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Entities;
using GymSystemDAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.Controllers
{
    public class ChatController(AppDbContext _context, UserManager<AppUser> _userManager) : Controller
    {
        public async Task<IActionResult> ChatAsync()
        {
            var chats = new List<Chat>();
            int chatId = 0;
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "SuperAdmin"))
            {
                chats = _context.Chat.ToList();
            }
            else
            {

                var IsInChat = _context.Chat.FirstOrDefault(C => C.UserID == user.Id && C.Status == Status.Open);
                if (IsInChat is null)
                {
                    var newChat = new Chat()
                    {
                        UserID = user.Id,
                        Status = Status.Open
                    };
                    await _context.Chat.AddAsync(newChat);
                    await _context.SaveChangesAsync();

                    chatId = newChat.Id;
                }
                else chatId = IsInChat.Id;
            }

            var model = new ChatModel()
            {
                Chats = chats,
                ChatId = chatId
            };

            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var messages = await _context.Messages
                .Where(m => m.ChatID == chatId)
                .OrderBy(m => m.SentAt)
                .Select(m => new {
                    id              = m.Id,
                    content         = m.Content,
                    sentAt          = m.SentAt,
                    senderId        = m.SenderId,          
                    chatId          = m.ChatID,
                    senderUserName  = m.Sender.UserName
                })
                .ToListAsync();

            return Json(messages);
        }
    }
}