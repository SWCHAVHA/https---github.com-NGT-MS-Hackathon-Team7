using System;
using System.Collections.Generic;

namespace Penalty_Calculation1.Models;

public partial class SecurityPrice
{
    public long PriceId { get; set; }

    public short? Poh { get; set; }

    public string? IsinSecId { get; set; }

    public DateOnly? ValidFromDate { get; set; }

    public decimal? SecPrice { get; set; }

    public DateTime CntlTimestamp { get; set; }

    public string CntlUserid { get; set; } = null!;

    public bool? Enable { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
