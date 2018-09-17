using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMom.BLL.DataTransferObjects
{
    public class FavoriteAdvert
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long AdvertId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? PlaningOn { get; set; }

        public virtual Advert Advert { get; set; }

        public virtual User Users { get; set; }

    }
}
