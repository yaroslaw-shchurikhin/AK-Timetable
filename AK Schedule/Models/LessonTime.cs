using System;
using System.Collections.Generic;

namespace AK_Schedule.Models;

public partial class LessonTime
{
    public int LtimeId { get; set; }

    public int Lnumber { get; set; }

    public TimeOnly Lstarttime { get; set; }

    public TimeOnly Lendtime { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
