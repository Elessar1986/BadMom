using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Registration
{
    public class UserPasswordData
    {
        public string passwordHash { get; set; }

        public string salt { get; set; }

        public string Login { get; set; }

    }
}