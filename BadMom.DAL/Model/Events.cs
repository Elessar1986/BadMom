namespace BadMom.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Events
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public int Type { get; set; }

        public DateTime Date { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? Source { get; set; }

        public bool Remind { get; set; }

        public virtual EventType EventType { get; set; }

        public virtual Source Source1 { get; set; }

        public virtual Users Users { get; set; }
    }
}
