using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models.MemberModels;
using GymSystemBLL.Models.PlanModels;

namespace GymSystemBLL.Models
{
    public class UpdateMemberModelView : IHasPlan
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(30,MinimumLength = 3,ErrorMessage = "Name must be between 3 and 30 char")]
        [RegularExpression(@"^[a-zA-Z\s]+$",ErrorMessage = "Name Must Contain Only letters or spaces")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Email Is Required")]
        [StringLength(100,MinimumLength = 5,ErrorMessage = "Email must be between 5 and 100 char")] 
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invalid Phone Format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Error Message must be valid Egyptian number")]
        public string Phone { get; set; } = null!;

        public AddressModelView AddressModel { get; set; } = null!;

        public string? Photo { get; set; }

        
        public int PlanID { get; set; }
        public IEnumerable<PlanModelView>? Plans { get; set;} = null!;
    }
}