using BadMom.Models.Advert;
using BadMom.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Admin
{
    public class AdminUserDataVM
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Roles { get; set; }

        public DateTime Created { get; set; }

        public byte[] Photo { get; set; }

        public bool Confirmed { get; set; }

        public bool? Gender { get; set; }

        public int? Status { get; set; }

    }

    public enum UserStatus
    {
        Blocked,
        Unblocked
    }

    public enum UserRoles
    {
        User,
        Admin,
        Moder
    }
}