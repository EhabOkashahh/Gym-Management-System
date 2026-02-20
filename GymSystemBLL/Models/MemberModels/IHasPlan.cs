using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.PlanModels;

namespace GymSystemBLL.Models.MemberModels
{
    public interface IHasPlan
    {
        public IEnumerable<PlanModelView> Plans { get; set; }
    }
}