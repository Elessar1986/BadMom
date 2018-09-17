using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BadMom.Models.User
{
    public class PersonalMessage
    {
        public long Id { get; set; }

        public long UserTo { get; set; }

        public long UserFrom { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Максимальная длина сообщения 500 символов")]
        public string Body { get; set; }

        public DateTime Created { get; set; }

        public PersonalMessageStatus Status { get; set; }
    }

    public enum PersonalMessageStatus
    {
        New = 0,
        Readed = 1,
        Deleted = 2
    }
}
