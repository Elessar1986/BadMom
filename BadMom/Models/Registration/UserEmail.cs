using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BadMom.Models.Registration
{
    public class UserEmail
    {

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

    }
}