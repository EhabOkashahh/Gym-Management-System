using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystemBLL.Models
{
    public class AddressModelView
    {
        [Required(ErrorMessage = "BuildingNumber Is Required")]
        [Range(1,1000,ErrorMessage = "BuildingNumber must be between 5 and 100 char")]
        public int BuildingNo { get; set; }




        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30,MinimumLength = 3,ErrorMessage = "Street must be between 3 and 30 char")]
        public string Street { get; set; }= null!;



        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30,MinimumLength = 3,ErrorMessage = "City must be between 3 and 30 char")]
        [RegularExpression(@"^[a-zA-Z\s]+$",ErrorMessage = "City Must Contain Only letters or spaces")]
        public string City { get; set; } = null!;
    }
}