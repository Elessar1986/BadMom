using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BadMom.Models.Wallet
{

    public partial class Source
    {

        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ResourceType ResourceType { get; set; }

        public long UserId { get; set; }

        [Required]
        [Range(typeof(decimal), "0,01", "1000000000,00", ErrorMessage = "Значение должно быть в этом диапазане 0,01 - 1000000000,00")]
        public decimal? Sum { get; set; }

        [Required]
        public long Type { get; set; }

        public DateTime Created { get; set; }
    }
}
