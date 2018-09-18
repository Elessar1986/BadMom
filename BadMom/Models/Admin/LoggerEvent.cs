using BadMom.BLL.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Admin
{
    public class LoggerEvent
    {

        public DateTime Created { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public logTypes Type { get; set; }
    }
}