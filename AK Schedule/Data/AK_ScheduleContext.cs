using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AK_Schedule.Models;

namespace AK_Schedule.Data
{
    public class AK_ScheduleContext : DbContext
    {
        public AK_ScheduleContext (DbContextOptions<AK_ScheduleContext> options)
            : base(options)
        {
        }

        public DbSet<AK_Schedule.Models.Lesson> Lesson { get; set; } = default!;
    }
}
