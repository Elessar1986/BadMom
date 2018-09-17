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

        public int Category { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public Category Category1 { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public decimal Price { get; set; }

        public bool New { get; set; }
    }
}
