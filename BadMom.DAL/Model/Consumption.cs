namespace BadMom.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consumption")]
    public partial class Consumption
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public int Reason { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public DateTime Date { get; set; }

        public long Type { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public long Source { get; set; }

        public bool? Potentian { get; set; }

        public decimal Sum { get; set; }

        public virtual ConsumptionReason ConsumptionReason { get; set; }

        public virtual OperationType OperationType { get; set; }

        public virtual Source Source1 { get; set; }

        public virtual Users Users { get; set; }
    }
}
