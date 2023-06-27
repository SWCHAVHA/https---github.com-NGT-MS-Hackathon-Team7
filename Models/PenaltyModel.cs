using System;
using System.Collections.Generic;

namespace Penalty_Calculation1.Models;

public partial class PenaltyModel
{
    public int? TransactionId { get; set; }

    public int? FailedTransactionQty { get; set; }

    public decimal? SecurityPrice { get; set; }

    public decimal? SecurityPenaltyRate { get; set; }
}
