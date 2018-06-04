namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public partial class logEvents
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public logTypes Type { get; set; }
    }
}
