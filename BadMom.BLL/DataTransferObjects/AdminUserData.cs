using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.BLL.DataTransferObjects
{
    public class AdminUserData
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
}
