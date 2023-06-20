using System;
using System.Collections.Generic;

namespace Penalty_Calculation1.Models;

public partial class Country
{
    public string CountryId { get; set; } = null!;

    public string? CountryName { get; set; }

    public string? CntlUserid { get; set; }

    public DateTime? CntlTimestamp { get; set; }

    public virtual ICollection<HolidayCalender> HolidayCalenders { get; set; } = new List<HolidayCalender>();
}
