using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Registration
{
    public class UserData
    {
        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string Roles { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public byte[] Photo { get; set; }

        public bool Confirmed { get; set; }
    }
}