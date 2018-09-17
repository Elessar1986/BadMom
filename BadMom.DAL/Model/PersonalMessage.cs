namespace BadMom.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonalMessage")]
    public partial class PersonalMessage
    {
        public long Id { get; set; }

        public long UserTo { get; set; }

        public long UserFrom { get; set; }

        [Required]
        [StringLength(500)]
        public string Body { get; set; }

        public DateTime Created { get; set; }

        public int? Status { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }
    }
}
