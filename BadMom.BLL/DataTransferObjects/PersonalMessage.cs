using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.BLL.DataTransferObjects
{
    public class PersonalMessage
    {
        public long Id { get; set; }

        public long UserTo { get; set; }

        public long UserFrom { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }

        public int? Status { get; set; }
 
    }
}
