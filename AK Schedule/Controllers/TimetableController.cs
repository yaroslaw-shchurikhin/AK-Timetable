using AK_Schedule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AK_Schedule.Controllers
{
	public class TimetableController(AkScheduleContext context) : Controller
	{
		private readonly AkScheduleContext _context = context;
        public IActionResult Index(int grade = 5)
		{
			var lessons = _context.TimetableAlls.Where(l => l.Grade == grade.ToString()).ToList(); // Получение данных из базы данных
			var lessonViewModels = lessons.Select(lesson => new TimetableAll
			{
				Grade = lesson.Grade,
				Classroom = lesson.Classroom,
				Subject = lesson.Subject,
				Teacher = lesson.Teacher,
				LessonNum = lesson.LessonNum,
				Starttime = lesson.Starttime,
				Endtime	= lesson.Endtime,
				Day	= lesson.Day
			}).ToList();

			return View(lessonViewModels);
		}
	}
}
