namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public partial class Comment
    {
        public long Id { get; set; }

        public long MessageId { get; set; }

        public string Body { get; set; }

        public long? TargetUserId { get; set; }

        public string TargetUserName { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public User User { get; set; }
    }
}
