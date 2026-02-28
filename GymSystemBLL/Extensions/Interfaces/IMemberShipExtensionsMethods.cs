using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Entities.Enums;

namespace GymSystemBLL.Extensions.Interfaces
{
    public interface IMemberShipExtensionsMethods
    {
        string ToBadgeClass(MemberShipStatus memberShipStatus);
        string GetDisplayName(MemberShipStatus memberShipStatus);
    }
}