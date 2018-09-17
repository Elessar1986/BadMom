namespace BadMom.BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    public partial class Consumption
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastUpdate { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public bool? Potentian { get; set; }

        public decimal Sum { get; set; }

        public ConsumptionReason ConsumptionReason { get; set; }

        public OperationType OperationType { get; set; }

        public Source Source1 { get; set; }

        public long UserId { get; set; }

        public long Type { get; set; }

        public long Source { get; set; }

        public int Reason { get; set; }

    }
}
