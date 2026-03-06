using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystemBLL.Models.SessionModels
{
    public class DetailsSessionModelView
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public string CategoryName { get; set; } = null!;
        public string TrainerName { get; set; } = null!;

        public string Status { get; set; } = null!;

        public int CategoryID { get; set; }
        public int TrainerID { get; set; }

    }
}