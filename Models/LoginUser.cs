using System;
using System.Collections.Generic;

namespace Penalty_Calculation1.Models;

public partial class LoginUser
{
    public int LoginId { get; set; }

    public string UserId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? TelephoneNumber { get; set; }

    public DateTime? CntlTimestamp { get; set; }

    public string? CntlUserid { get; set; }

    public int RoleId { get; set; }

    public virtual Userrole Role { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
