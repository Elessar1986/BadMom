namespace BadMom.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public long Id { get; set; }

        public long MessageId { get; set; }

        [Required]
        public string Body { get; set; }

        public long UserId { get; set; }

        public long? TargetUserId { get; set; }

        [StringLength(100)]
        public string TargetUserName { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public virtual Messages Messages { get; set; }

        public virtual Users Users { get; set; }
    }
}
