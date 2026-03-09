using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models;
using GymSystemBLL.Models.MemberModels;
using GymSystemBLL.Models.PlanModels;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Entities;
using GymSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GymSystemDAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using GymSystemDAL.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GymSystemBLL.Services.Classes
{
    public class MemberService(IUnitOfWork _UnitOfWork, IMapper _autoMapper , IPlanService _planService,UserManager<AppUser> _userManager, IEmailService _emailService) : IMemberService
    {
        
        public async Task<IEnumerable<MemberModelView>> GetAllMembersAsync()
        {
           var Members = await GetRepo().GetAllAsync();
           return _autoMapper.Map<IEnumerable<MemberModelView>>(Members);
        }
        

        public async Task<MemberDetailsModelView?> GetMemberByIdAsync(int? id)
        {
            if(id is null ) return null;
            
            var Member = await GetRepo().GetByIdAsync(id.Value);
            if(Member is null) return null!;

            return _autoMapper.Map<MemberDetailsModelView>(Member);
        }

        public async Task<CreateMemberResult> CreateMemberAsync(CreateMemberModelView modelView)
        {
            // Check if phone or email already exists
            var exists = await FindByEmailOrPhone(modelView.Phone, modelView.Email);
            if (exists) 
                return new CreateMemberResult { IsSuccessed = false, Error = "Phone or Email already exists." };

            // Validate plan
            if (modelView.PlanID is null) 
                return new CreateMemberResult { IsSuccessed = false, Error = "Plan ID is required." };

            var plan = await _planService.GetPlanDetails(modelView.PlanID);
            if (plan == null) 
                return new CreateMemberResult { IsSuccessed = false, Error = "Plan not found." };

            var now = DateTime.Now;

            // Map Member and create membership
            var member = _autoMapper.Map<Member>(modelView);
            var membership = new MemberShip
            {
                CreatedAt = now,
                StartDate = now,
                EndDate = now.AddDays(plan.DurationDays - 1),
                UpdatedAt = now,
                MemberShipStatus = MemberShipStatus.Active,
                PlanID = plan.Id
            };
            member.MemberShip = membership;
            member.MemberShipID = membership.Id;


            // Create AppUser
            var appUser = new AppUser
            {
                Email = modelView.Email,
                PhoneNumber = modelView.Phone,
                UserName = modelView.Name.Replace(" ", "").ToLower()
            };
            member.appUser = appUser;
            member.UserId = appUser.Id;
            var password = GeneratePassword();

            var result = await _userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new CreateMemberResult { IsSuccessed = false, Error = $"User creation failed: {errors}" };
            }
            await GetRepo().AddAsync(member);

            // Send email
            _emailService.SendEmail(appUser.UserName, appUser.Email, password);

            // Apply changes to database
            await _UnitOfWork.ApplyToDataBaseAsync();

            return new CreateMemberResult { IsSuccessed = true};
        }

        public async Task<bool> SoftDeleteMember(int? id)
        {
            if(id is null) return false;

            var user = await GetRepo().GetByIdAsync(id.Value);
            if(user is null) return false;

            await GetRepo().SoftDelete(id.Value);
            user.MemberShip.MemberShipStatus = MemberShipStatus.InTrashCan;
            return await _UnitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<bool> UpdateMember(int id , UpdateMemberModelView model)
        {
            var member = await GetRepo().GetByIdAsync(id);
            if(member is null) return false;

            var UpdatedMember =_autoMapper.Map(model,member);

            UpdatedMember.MemberShip.PlanID = model.PlanID;
            GetRepo().Update(UpdatedMember);

            return await _UnitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<HealthRecordModelView?> GetHealthRecordDetails(int? id)
        {
            if(id is null) return null;

            var member = await GetRepo().GetByIdAsync(id.Value);
            if(member is null || member.healthRecord is null) return null;

            return _autoMapper.Map<HealthRecordModelView>(member.healthRecord);
        }

        public async Task<bool> RestoreMember(int id)
        {
           var member = await GetRepo().GetByIdAsync(id);
           if(member is null) return false;

           member.IsDeleted = false;
           member.MemberShip.MemberShipStatus = MemberShipStatus.Active;
           return await _UnitOfWork.ApplyToDataBaseAsync() > 0;
        }

        private async Task<bool> FindByEmailOrPhone(string phone , string email)
        {
            var Member = await GetRepo().GetAllAsync();
            return Member.Any(m => m.Phone == phone || m.Email == email);
        }

        private IGenericRepository<Member> GetRepo()
        {
            return _UnitOfWork.GenerateRepository<Member>();
        }

        private string GeneratePassword()
        {
            var ch =  Enumerable.Range('A', 26).Select(x => (char)x).ToArray(); 
            var guid = Guid.NewGuid().ToString("N"); // removes dashes
            var random = new Random();
            // take first 6 chars + add an uppercase, a digit, and a special char
            return char.ToUpper(ch[random.Next(0,25)]) + guid.Substring(1,6) + char.ToLower(ch[random.Next(0,25)]) + random.Next(0,9) + "!";
        }
    }
}