using System;
using System.Collections.Generic;

namespace AK_Schedule.Models;

public partial class Classroom
{
    public int ClassroomId { get; set; }

    public string ClassroomName { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
