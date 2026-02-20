using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Execution;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models.MeemberShipModels;

namespace GymSystemBLL.Models.MemberModels
{
    public class MemberDetailsModelView : MemberModelView
    {
        public string? DateOfBirth { get; set; }
        public AddressModelView? Address { get; set; }

        public int MemberShipID { get; set; }
        public MemberShipModelView MemberShip { get; set; } = null!;
    }
}