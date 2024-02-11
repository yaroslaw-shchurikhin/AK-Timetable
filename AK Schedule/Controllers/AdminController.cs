using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AK_Schedule.Models;

namespace AK_Schedule.Controllers
{
    public class AdminController : Controller
    {
        private readonly AkScheduleContext _context;

        public AdminController(AkScheduleContext context)
        {
            _context = context;
        }

        // GET: Admin
        public IActionResult Index()
        {
            //var akScheduleContext = _context.Lessons.Include(l => l.Class).Include(l => l.Classroom).Include(l => l.Day).Include(l => l.Ltime).Include(l => l.Subject).Include(l => l.Teacher);
            // return View(await akScheduleContext.ToListAsync());
            return View();
        }
        public IActionResult ChooseGrade()
        {
            return View();
        }

        public IActionResult EditTimetable(int grade)
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
                Endtime = lesson.Endtime,
                Day = lesson.Day
            }).ToList();

            return View(lessonViewModels);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Class)
                .Include(l => l.Classroom)
                .Include(l => l.Day)
                .Include(l => l.Ltime)
                .Include(l => l.Subject)
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId");
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "ClassroomId", "ClassroomId");
            ViewData["DayId"] = new SelectList(_context.Weekdays, "DayId", "DayId");
            ViewData["LtimeId"] = new SelectList(_context.LessonTimes, "LtimeId", "LtimeId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LessonId,ClassId,ClassroomId,SubjectId,TeacherId,DayId,LtimeId")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", lesson.ClassId);
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "ClassroomId", "ClassroomId", lesson.ClassroomId);
            ViewData["DayId"] = new SelectList(_context.Weekdays, "DayId", "DayId", lesson.DayId);
            ViewData["LtimeId"] = new SelectList(_context.LessonTimes, "LtimeId", "LtimeId", lesson.LtimeId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", lesson.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", lesson.TeacherId);
            return View(lesson);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", lesson.ClassId);
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "ClassroomId", "ClassroomId", lesson.ClassroomId);
            ViewData["DayId"] = new SelectList(_context.Weekdays, "DayId", "DayId", lesson.DayId);
            ViewData["LtimeId"] = new SelectList(_context.LessonTimes, "LtimeId", "LtimeId", lesson.LtimeId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", lesson.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", lesson.TeacherId);
            return View(lesson);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LessonId,ClassId,ClassroomId,SubjectId,TeacherId,DayId,LtimeId")] Lesson lesson)
        {
            if (id != lesson.LessonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.LessonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", lesson.ClassId);
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "ClassroomId", "ClassroomId", lesson.ClassroomId);
            ViewData["DayId"] = new SelectList(_context.Weekdays, "DayId", "DayId", lesson.DayId);
            ViewData["LtimeId"] = new SelectList(_context.LessonTimes, "LtimeId", "LtimeId", lesson.LtimeId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", lesson.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", lesson.TeacherId);
            return View(lesson);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Class)
                .Include(l => l.Classroom)
                .Include(l => l.Day)
                .Include(l => l.Ltime)
                .Include(l => l.Subject)
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.LessonId == id);
        }
    }
}
