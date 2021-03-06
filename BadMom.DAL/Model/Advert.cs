namespace BadMom.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advert")]
    public partial class Advert
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Advert()
        {
            FavoriteAdvert = new HashSet<FavoriteAdvert>();
        }

        public long Id { get; set; }

        public int Category { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Body { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        public long UserId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public decimal? Price { get; set; }

        public bool? New { get; set; }

        public virtual Category Category1 { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FavoriteAdvert> FavoriteAdvert { get; set; }
    }
}
