using System;
using System.Collections.Generic;

namespace Penalty_Calculation1.Models;

public partial class Userrole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public DateTime? CntlTimestamp { get; set; }

    public string? CntlUserId { get; set; }

    public virtual ICollection<LoginUser> LoginUsers { get; set; } = new List<LoginUser>();
}
