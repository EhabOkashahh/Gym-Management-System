using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.MeemberShipModels;
using GymSystemBLL.Models.MemberModels;

namespace GymSystemBLL.Services.Interfaces
{
    public interface IMemberShipService
    {
        Task<IEnumerable<MemberShipModelView>> GetAllMemberShipAsync();
        Task<MemberShipModelView> GetMemberShipDetails(int Id);
        Task<bool> UpdateMemberShip(int id , MemberShipModelView model);

    }
}