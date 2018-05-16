namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public partial class Message
    {

        public long Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public byte[] Photo { get; set; }

        public long UserId { get; set; }

        public int Theme { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public List<Comment> Comments { get; set; }

        public Theme ThemeObj { get; set; }

        public User User { get; set; }

        public List<User> UsersLikes { get; set; }
    }
}
