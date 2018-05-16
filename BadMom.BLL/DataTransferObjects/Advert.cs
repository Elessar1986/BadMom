namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public partial class Advert
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public byte[] Photo { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public Category Category { get; set; }

        public User User { get; set; }
    }
}
