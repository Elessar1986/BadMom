namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;


    public partial class Events
    {
        public long Id { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }

        public string Description { get; set; }

        public bool Remind { get; set; }

        public int Type { get; set; }

        public long? Source { get; set; }

        public long UserId { get; set; }

        public string Title { get; set; }

        public virtual EventType EventType { get; set; }

        public virtual Source Source1 { get; set; }

        public long? AdvertId { get; set; }

    }
}
