using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models;

namespace GymSystemBLL.Services.Interfaces
{
    public interface IAnalyticsService
    {
        Task<AnalyticsModelView> GetAnalyticsData();


    }
}