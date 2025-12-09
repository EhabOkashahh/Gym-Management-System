using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystemBLL.Models.MemberModels
{
    public class MemberDetailsModelView : MemberModelView
    {
        public string? PlanName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? MemberShipStartedDate { get; set; }
        public string? MemberShipEndDate { get; set; }
        public string? Address { get; set; }
    }
}