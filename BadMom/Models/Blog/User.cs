using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Blog
{
    public class User
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public byte[] Photo { get; set; }
    }
}