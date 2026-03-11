using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystemBLL.Models.SessionModels
{
    public class SessionModelView
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TrainerName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public int  ReservedSeats{ get; set; }

        // Computed fields
        public string Status { get; set; } = null!;
        public TimeSpan Duration { get; set; }
        public string DateDisplay { get; set; } = null!;
        public string TimeRangeDisplay { get; set; } = null!;
        
        public bool IsEnrolled { get; set; }
    }
}