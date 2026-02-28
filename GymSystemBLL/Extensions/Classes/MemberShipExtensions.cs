using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Extensions.Interfaces;
using GymSystemDAL.Entities.Enums;

namespace GymSystemBLL.Extensions.Classes
{
    public class MemberShipExtensions : IMemberShipExtensionsMethods
    {
        public string ToBadgeClass(MemberShipStatus memberShipStatus)
        {
            return memberShipStatus switch
            {
              MemberShipStatus.Active => "bg-success",
              MemberShipStatus.Expired => "bg-warning text-dark",
              MemberShipStatus.Canceled => "bg-danger",
              MemberShipStatus.InTrashCan => "bg-info",
              _ => "bg-secondary"
            };
        }

        public string GetDisplayName(MemberShipStatus memberShip)
        {
            return memberShip.ToString();
        }
    }
}