using System;
using System.Collections.Generic;
using BadMom.Models.Registration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BadMom.Models.Blog
{
    public class PostVM
    {
        public long Id { get; set; }

        [StringLength(100, ErrorMessage ="Максимальная длина заголовка 100 символов.")]
        public string Title { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        public byte[] Photo { get; set; }

        public long UserId { get; set; }

        public int Theme { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public List<CommentVM> Comments { get; set; }

        public Themes ThemeObj { get; set; }

        public User User { get; set; }

        public List<User> UsersLikes { get; set; }
    }
}