using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.User
{
    public class PrivateMessageByUserVM
    {
        public string Login { get; set; }

        public long Id { get; set; }

        public byte[] Avatar { get; set; }

        public List<PersonalMessage> Messages { get; set; }
    }
}