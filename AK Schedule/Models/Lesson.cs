using System;
using System.Collections.Generic;

namespace AK_Schedule.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int? ClassId { get; set; }

    public int? ClassroomId { get; set; }

    public int? SubjectId { get; set; }

    public int? TeacherId { get; set; }

    public int? DayId { get; set; }

    public int? LtimeId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Classroom? Classroom { get; set; }

    public virtual Weekday? Day { get; set; }

    public virtual LessonTime? Ltime { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
