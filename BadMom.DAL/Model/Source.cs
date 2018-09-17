namespace BadMom.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Source")]
    public partial class Source
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Source()
        {
            Consumption = new HashSet<Consumption>();
            Events = new HashSet<Events>();
            Income = new HashSet<Income>();
        }

        public long Id { get; set; }

        public long UserId { get; set; }

        public long Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public decimal Sum { get; set; }

        public DateTime Created { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consumption> Consumption { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Events> Events { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Income> Income { get; set; }

        public virtual ResourceType ResourceType { get; set; }

        public virtual Users Users { get; set; }
    }
}
