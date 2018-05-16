namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;


    public partial class Events
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }

        public string Description { get; set; }

        public bool Remind { get; set; }

        public EventType EventType { get; set; }

        public Source Source1 { get; set; }

        public User User { get; set; }
    }
}
