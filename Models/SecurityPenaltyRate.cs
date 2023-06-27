using System;
using System.Collections.Generic;

namespace Penalty_Calculation1.Models;

public partial class SecurityPenaltyRate
{
    public long PenaltyId { get; set; }

    public DateOnly? ValidFromDate { get; set; }

    public decimal? PenaltyRate { get; set; }

    public DateOnly? LastUpdatedDate { get; set; }

    public DateTime CntlTimestamp { get; set; }

    public string? CntlUserid { get; set; }

    public decimal ApprovePenaltyRequired { get; set; }

    public string Approval { get; set; } = null!;

    public bool? Enable { get; set; }

    public string? Isin { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
