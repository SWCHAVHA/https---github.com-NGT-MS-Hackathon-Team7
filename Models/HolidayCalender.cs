using System;
using System.Collections.Generic;

namespace Penalty_Calculation1.Models;

public partial class HolidayCalender
{
    public int HolidayId { get; set; }

    public string CountryId { get; set; } = null!;

    public DateOnly HolidayDate { get; set; }

    public DateOnly LastUpdatedDate { get; set; }

    public string Description { get; set; } = null!;

    public int Year { get; set; }

    public string CntlUserid { get; set; } = null!;

    public bool? Enable { get; set; }

    public virtual Country Country { get; set; } = null!;
}
