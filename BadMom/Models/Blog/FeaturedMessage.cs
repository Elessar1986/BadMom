using BadMom.Models.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Blog
{
    public class FeaturedMessage
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public byte[] Photo { get; set; }

        public List<CommentVM> Comments { get; set; }

        public List<User> UsersLikes { get; set; }
    }
}