namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public partial class Source
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public ResourceType ResourceType { get; set; }

        public long UserId { get; set; }

        public decimal Sum { get; set; }

        public long Type { get; set; }

        public DateTime Created { get; set; }
    }
}
