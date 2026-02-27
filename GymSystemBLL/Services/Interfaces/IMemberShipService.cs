using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.MeemberShipModels;
using GymSystemBLL.Models.MemberModels;
using GymSystemDAL.Entities;

namespace GymSystemBLL.Services.Interfaces
{
    public interface IMemberShipService
    {
        Task<IEnumerable<MemberShipModelView>> GetAllMemberShipAsync();
        Task<MemberShipModelView> GetMemberShipDetails(int Id);
        Task<bool> UpdateMemberShip(int id , MemberShipModelView model);
        Task<bool> CancelMemberShip(int id);
        Task<bool> RenewMemberShip(int id, int months = 1);
        Task<IEnumerable<MemberShip>> GetActiveMemberships();

    }
}