using System;
using System.Collections.Generic;

namespace Penalty_Calculation1.Models;

public partial class Party
{
    public string PartyId { get; set; } = null!;

    public string PartyName { get; set; } = null!;

    public string? CntlUserId { get; set; }

    public DateTime? CntlTimestamp { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
