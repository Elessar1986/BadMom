namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public partial class User
    {

        public long Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public string Email { get; set; }

        public string Roles { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public byte[] Photo { get; set; }

    }
}
