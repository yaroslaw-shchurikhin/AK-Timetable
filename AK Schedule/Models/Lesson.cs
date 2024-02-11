using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace AK_Schedule.Models
{
    [Table("Timetable_All")]
    public class Lesson
    {
        [Column("id")]  // Указываем точное имя столбца
        public int Id { get; set; }

        [Column("grade")]
        public String? Grade { get; set; }

        [Column("classroom")]
        public String? Classroom { get; set; }

        [Column("teacher")]
        public String? Teacher { get; set; }

        [Column("subject")]
        public String? Subject { get; set; }

        [Column("day")]
        public int? Day { get; set; }

        [Column("starttime")]
        [DataType(DataType.Time)]
        public TimeOnly? StartTime { get; set; }

        [Column("endtime")]
        [DataType(DataType.Time)]
        public TimeOnly? EndTime { get; set; }

        [Column("lesson_num")]
        public int? LessonNum { get; set; }

        [NotMapped] // Это свойство не отображается в базе данных
        public string TeacherFormatted
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Teacher))
                    return string.Empty;

                var nameParts = Teacher.Split(' ');

                if (nameParts.Length >= 1)
                {
                    // Фамилия
                    var lastName = nameParts[0];

                    // Инициалы имени и отчества
                    var initials = nameParts.Skip(1).Select(p => p.Length > 0 ? p[0].ToString() + "." : string.Empty);

                    return $"{lastName} {string.Join(" ", initials)}";
                }

                return Teacher;
            }
        }
    }
}
