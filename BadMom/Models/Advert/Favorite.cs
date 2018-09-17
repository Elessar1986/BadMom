using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadMom.Models.Advert
{
    public class Favorite
    {

        public long UserId { get; set; }

        public long AdvertId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? PlaningOn { get; set; }

    }
}