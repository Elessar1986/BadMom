namespace BadMom.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FavoriteAdvert")]
    public partial class FavoriteAdvert
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long AdvertId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? PlaningOn { get; set; }

        public virtual Advert Advert { get; set; }

        public virtual Users Users { get; set; }
    }
}
