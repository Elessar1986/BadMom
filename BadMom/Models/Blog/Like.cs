using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Blog
{
    public class Like
    {
        public int likes { get; set; }

        public bool myLike { get; set; }

        public int messageId { get; set; }
    }
}