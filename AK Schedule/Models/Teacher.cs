using System;
using System.Collections.Generic;

namespace AK_Schedule.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string TeacherName { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
