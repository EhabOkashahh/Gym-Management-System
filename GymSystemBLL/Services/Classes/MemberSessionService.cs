using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymSystemBLL.Services.Classes
{
    public class MemberSessionService(AppDbContext _context) : IMemberSessionService
    {
        public async Task<EnrollResult> EnrollAsync(string userId, int sessionId)
        {
            var member = await _context.Members
        .FirstOrDefaultAsync(m => m.UserId == userId);

            if (member == null)
                return new EnrollResult
                {
                    IsSuccessed = false,
                    Message = "Member not found."
                };

            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
                return new EnrollResult
                {
                    IsSuccessed = false,
                    Message = "Session not found."
                };

            var alreadyEnrolled = await _context.MemberSessions
                .AnyAsync(ms => ms.MemberID == member.Id && ms.SessionID == sessionId);

            if (alreadyEnrolled)
                return new EnrollResult
                {
                    IsSuccessed = false,
                    Message = "You are already enrolled in this session."
                };

            // Check capacity
            if (session.ReservedSeats >= session.Capacity)
                return new EnrollResult
                {
                    IsSuccessed = false,
                    Message = "This session is full."
                };

            session.ReservedSeats++;

            var memberSession = new MemberSessions
            {
                MemberID = member.Id,
                SessionID = sessionId,
                BookingDay = DateTime.Now
            };

            _context.MemberSessions.Add(memberSession);

            try
            {
                await _context.SaveChangesAsync();
                return new EnrollResult { IsSuccessed = true };
            }
            catch (DbUpdateConcurrencyException)
            {
                return new EnrollResult
                {
                    IsSuccessed = false,
                    Message = "Another user just booked this session. Please try again."
                };
            }
        }
        
        public async Task<EnrollResult> WithdrawAsync(string userId, int sessionId)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userId);
            if (member == null) return new EnrollResult { IsSuccessed = false, Message = "Member not found." };

            var memberSession = await _context.MemberSessions
                .FirstOrDefaultAsync(ms => ms.MemberID == member.Id && ms.SessionID == sessionId);

            if (memberSession == null)
                return new EnrollResult { IsSuccessed = false, Message = "You are not enrolled in this session." };

            _context.MemberSessions.Remove(memberSession);

            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
            if (session != null && session.ReservedSeats > 0) session.ReservedSeats--;

            await _context.SaveChangesAsync();

            return new EnrollResult { IsSuccessed = true };
        }
    }
}