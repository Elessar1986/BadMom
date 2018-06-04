using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BadMom.Models.Registration
{
    public class ChangePass
    {
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Пароль меньше 8 символов!")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Пвроль ёще раз")]
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}