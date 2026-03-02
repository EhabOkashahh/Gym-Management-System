using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystemBLL.Models.TrainerModels
{
    public class TrainerDetailsModelView
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Specialization { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public AddressModelView Address { get; set; } = null!;
    }   
}