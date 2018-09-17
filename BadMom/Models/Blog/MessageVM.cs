using System;
using System.Collections.Generic;
using BadMom.Models.Registration;
using System.Linq;
using System.Web;

namespace BadMom.Models.Blog
{
    public class MessageVM
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public byte[] Photo { get; set; }

        public long UserId { get; set; }

        public int Theme { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public List<CommentVM> Comments { get; set; }

        public Themes ThemeObj { get; set; }

        public UserData User { get; set; }

        public List<UserData> UsersLikes { get; set; }
    }
}