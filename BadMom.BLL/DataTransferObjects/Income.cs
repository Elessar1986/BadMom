namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public partial class Income
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public IncomeReason IncomeReason { get; set; }

        public OperationType OperationType { get; set; }

        public Source Source { get; set; }

        public long UserId { get; set; }

        public long Reason { get; set; }

        public decimal Sum { get; set; }

        public long Type { get; set; }

        public long Destination { get; set; }

    }
}
