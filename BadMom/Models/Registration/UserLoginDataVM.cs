using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BadMom.Models.Registration
{
    public class UserLoginDataVM
    {
        [Display(Name = "Логин")]
        [Required]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}