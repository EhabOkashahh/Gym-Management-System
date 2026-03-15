using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.Models;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Entities;
using GymSystemDAL.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.Controllers
{
    [Authorize]
    public class ChatController(AppDbContext _context, UserManager<AppUser> _userManager) : Controller
    {
        public async Task<IActionResult> ChatAsync()
        {
            var chats = new List<Chat>();
            int chatId = 0;
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "SuperAdmin"))
            {
                chats = await _context.Chat.Where(c => c.Status == Status.Open && c.Messages.Any()).ToListAsync();
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
                else
                {
                    chatId = IsInChat.Id;
                }
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
                .Select(m => new
                {
                    userName = m.Sender.UserName,
                    msg = m.Content,
                    sentAt = m.SentAt
                })
                .ToListAsync();

            return Json(messages);
        }
        
        [HttpPost]
        public async Task<IActionResult> CloseTicket(int chatId)
        {
            var chat = await _context.Chat.FirstOrDefaultAsync(c => c.Id == chatId);

            if (chat == null)
                return NotFound();

            chat.Status = Status.Cancelled;

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}