using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entites.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.DAL.Entities
{
    public class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; }= null!;
        public string Phone { get; set; }= null!;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;
    }

    [Owned]
    public class Address
    {
        public string BuildingNo{ get; set; } = null!;
        public string Street{ get; set; } = null!;
        public string City{ get; set; } = null!;
    }
}