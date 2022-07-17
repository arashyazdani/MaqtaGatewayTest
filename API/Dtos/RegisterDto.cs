using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Dtos
{
    [DisplayName("Register")]
    public class RegisterDto
    {
        [Required(ErrorMessage = "DisplayName is required")]
        [RegularExpression(@"^[\u0061-\u007a\u0041-\u005a\u0030-\u0039\u0600-\u06ff\u0750-\u077f\ufb50-\ufdff\u08a0\u2014\u08ff\ufe70\u002d\ufefc\u200b\s]{1,50}$", ErrorMessage = "User name must be Alphanumeric and at least 6 characters.")]
        [Display(Name = "DisplayName")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                            + "@"
                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Email is incorrect")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.{8,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$", ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 Number, 1 non Alphanumeric and at least 6 characters.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
