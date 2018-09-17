using System;
using BadMom.Models.Registration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Blog
{
    public class CommentVM
    {
        public long Id { get; set; }

        public long MessageId { get; set; }

        public string Body { get; set; }

        public long? TargetUserId { get; set; }

        public string TargetUserName { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public User Users { get; set; }
    }
}