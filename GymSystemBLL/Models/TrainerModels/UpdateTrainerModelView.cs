using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entites.Enums;

namespace GymSystemBLL.Models.TrainerModels
{
    public class UpdateTrainerModelView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; } = null!;  

        // Address Info
        [Required]
        public AddressModelView Address { get; set; } = null!;

        // Professional Info
        [Required(ErrorMessage = "Specialization is required.")]
        public Specialities Specialization { get; set; }
    }
}