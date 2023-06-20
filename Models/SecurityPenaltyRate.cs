using System;
using System.Collections.Generic;

namespace Penalty_Calculation1.Models;

public partial class SecurityPenaltyRate
{
    public long PenaltyId { get; set; }

    public DateOnly? ValidFromDate { get; set; }

    public decimal? PenaltyRate { get; set; }

    public DateOnly? LastUpdatedDate { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
