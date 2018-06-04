using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Shared
{
    public class Error
    {
        public string Code { get; set; }

        public string ExDescription { get; set; }

        public string ExInnerDescription { get; set; }
    }
}