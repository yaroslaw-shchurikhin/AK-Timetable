using System;
using System.Collections.Generic;

namespace AK_Schedule.Models;

public partial class Weekday
{
    public int DayId { get; set; }

    public string DayName { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
