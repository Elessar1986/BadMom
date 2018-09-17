using BadMom.Models.Registration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BadMom.Models.Advert
{
    public class AdvertVM
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public byte[] Photo { get; set; }

        public int Category { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public Categories Category1 { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public bool New { get; set; }

        [Required]
        [Range(typeof(decimal), "0,01", "1000000000,00", ErrorMessage = "Значение должно быть в этом диапазане 0.01 - 1000000000.00")]
        //[DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
    }
}