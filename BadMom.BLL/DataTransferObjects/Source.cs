namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public partial class Source
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public ResourceType ResourceType { get; set; }

        public User User { get; set; }
    }
}
