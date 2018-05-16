namespace BadMom.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Income")]
    public partial class Income
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long Reason { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public long Type { get; set; }

        public DateTime Date { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long Destination { get; set; }

        public virtual IncomeReason IncomeReason { get; set; }

        public virtual OperationType OperationType { get; set; }

        public virtual Source Source { get; set; }

        public virtual Users Users { get; set; }
    }
}
