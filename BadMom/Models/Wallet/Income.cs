using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BadMom.Models.Wallet
{
    public partial class Income
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        [DataType (DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime? Date { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Максимальная длина описания 500 символов.")]
        public string Description { get; set; }

        public IncomeReason IncomeReason { get; set; }

        public OperationType OperationType { get; set; }

        public Source Source { get; set; }

        public long UserId { get; set; }

        [Required]
        [Display(Name = "Тип дохода")]
        public long Reason { get; set; }

        [Required]
        [Range(typeof(decimal), "0,01", "1000000000,00", ErrorMessage = "Значение должно быть в этом диапазане 0,01 - 1000000000,00")]
        [Display (Name ="Сумма дохода")]
        public decimal? Sum { get; set; }

        [Required]
        [Display(Name = "Вид дохода")]
        public long Type { get; set; }

        [Required (ErrorMessage ="У вас нет счетов!")]
        [Display(Name = "На какой счет")]
        public long Destination { get; set; }

    }

}
