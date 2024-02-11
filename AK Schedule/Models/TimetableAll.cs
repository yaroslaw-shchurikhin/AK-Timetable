using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AK_Schedule.Models;

public partial class TimetableAll
{
    public int Id { get; set; }

    public string? Grade { get; set; }

    public string? Classroom { get; set; }

    public int? LessonNum { get; set; }

    public TimeOnly? Starttime { get; set; }

    public TimeOnly? Endtime { get; set; }

    public string? Subject { get; set; }

    public string? Teacher { get; set; }

    public int? Day { get; set; }
}
