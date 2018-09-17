using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BadMom.Models.Wallet
{

    public partial class Consumption
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime? Date { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Potentian { get; set; }

        [Required]
        [Range(typeof(decimal), "0,01", "1000000000,00", ErrorMessage = "Значение должно быть в этом диапазане 0,01 - 1000000000,00")]
        [Display(Name = "Сумма расхода")]
        public decimal? Sum { get; set; }

        public ConsumptionReason ConsumptionReason { get; set; }

        public OperationType OperationType { get; set; }

        public Source Source1 { get; set; }

        public long UserId { get; set; }
        [Required]
        [Display(Name = "Вид расхода")]
        public long Type { get; set; }

        [Required(ErrorMessage = "У вас нет счетов!")]
        [Display(Name = "На какой счет")]
        public long Source { get; set; }
        [Required]
        [Display(Name = "Пречина расхода")]
        public int Reason { get; set; }
    }
}
