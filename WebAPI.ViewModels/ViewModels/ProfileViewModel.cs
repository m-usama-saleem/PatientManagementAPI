using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.ViewModels.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        [Required]
        public string LoginId { get; set; }
        //[Required]

        //At least one upper case English letter, (?=.*?[A - Z])
        //At least one lower case English letter, (?=.*?[a - z])
        //At least one digit, (?=.*?[0 - 9])
        //At least one special character, (?=.*?[#?!@$%^&*-])
        //Minimum eight in length.{8,}
        //(with the anchors)

        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage = "Password is not Strong")]
        public string Password { get; set; }
        //[Required]
        public int RoleId { get; set; }
    }
}
