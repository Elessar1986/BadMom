using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BadMom.Models.Wallet
{
    public class SourceInfoVM
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        public IncomeReason IncomeReason { get; set; }

        public ConsumptionReason ConsumptionReason { get; set; }

        public OperationType OperationType { get; set; }

        public Source Source { get; set; }

        [Required]
        [Range(typeof(decimal), "0,01", "1000000000,00", ErrorMessage = "Значение должно быть в этом диапазане 0,01 - 1000000000,00")]
        [Display(Name = "Сумма дохода")]
        public decimal Sum { get; set; }

    }
}