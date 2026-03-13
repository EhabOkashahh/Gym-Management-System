using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using GymSystemDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymSystemBLL.Sockets
{
    public class ChatHub(AppDbContext _context,UserManager<AppUser> _userManager) : Hub
    {

        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User is null) return;
            
            var isAdmin = Context.User.IsInRole("Admin") || Context.User.IsInRole("SuperAdmin")  ;
            if (isAdmin)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            }
        }

       public async Task SendMessage(int chatId, string message)
        {
            var username = Context.User?.Identity?.Name;
            if (username == null) return;

            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return;

            var chat = await _context.Chat
                .FirstOrDefaultAsync(c => c.Id == chatId && c.Status == Status.Open);

            if (chat == null) return;

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());

            var dbMessage = new Message
            {
                ChatID = chatId,
                SenderId = user.Id,
                Content = message,
                SentAt = DateTime.UtcNow
            };

            await _context.Messages.AddAsync(dbMessage);
            await _context.SaveChangesAsync();

            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", new
            {
                userName = username,
                chatId = chatId,
                msg = message
            });
        }
    }
}