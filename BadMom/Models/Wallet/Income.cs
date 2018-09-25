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
        [Display(Name = "����")]
        public DateTime? Date { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "������������ ����� �������� 500 ��������.")]
        public string Description { get; set; }

        public IncomeReason IncomeReason { get; set; }

        public OperationType OperationType { get; set; }

        public Source Source { get; set; }

        public long UserId { get; set; }

        [Required]
        [Display(Name = "��� ������")]
        public long Reason { get; set; }

        [Required]
        [Range(typeof(decimal), "0,01", "1000000000,00", ErrorMessage = "�������� ������ ���� � ���� ��������� 0,01 - 1000000000,00")]
        [Display (Name ="����� ������")]
        public decimal? Sum { get; set; }

        [Required]
        [Display(Name = "��� ������")]
        public long Type { get; set; }

        [Required (ErrorMessage ="� ��� ��� ������!")]
        [Display(Name = "�� ����� ����")]
        public long Destination { get; set; }

    }

}
