using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.BLL.DataTransferObjects
{
    public class PrivateMessageByUser
    {
        public string Login { get; set; }

        public long Id { get; set; }

        public byte[] Avatar { get; set; }

        public List<PersonalMessage> Messages { get; set; }
    }
}
