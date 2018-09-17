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
        [Display(Name = "����")]
        public DateTime? Date { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Potentian { get; set; }

        [Required]
        [Range(typeof(decimal), "0,01", "1000000000,00", ErrorMessage = "�������� ������ ���� � ���� ��������� 0,01 - 1000000000,00")]
        [Display(Name = "����� �������")]
        public decimal? Sum { get; set; }

        public ConsumptionReason ConsumptionReason { get; set; }

        public OperationType OperationType { get; set; }

        public Source Source1 { get; set; }

        public long UserId { get; set; }
        [Required]
        [Display(Name = "��� �������")]
        public long Type { get; set; }

        [Required(ErrorMessage = "� ��� ��� ������!")]
        [Display(Name = "�� ����� ����")]
        public long Source { get; set; }
        [Required]
        [Display(Name = "������� �������")]
        public int Reason { get; set; }
    }
}
