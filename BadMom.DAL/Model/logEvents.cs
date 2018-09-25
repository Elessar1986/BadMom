namespace BadMom.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class logEvents
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public int Type { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual logTypes logTypes { get; set; }
    }
}
