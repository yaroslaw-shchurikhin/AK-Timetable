using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AK_Schedule.Models;

public partial class AkScheduleContext : DbContext
{
    public AkScheduleContext()
    {
    }

    public AkScheduleContext(DbContextOptions<AkScheduleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonTime> LessonTimes { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TimetableAll> TimetableAlls { get; set; }

    public virtual DbSet<Weekday> Weekdays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AK_Schedule;Username=postgres;Password=2311");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("Classes_pkey");

            entity.Property(e => e.ClassId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("class_id");
            entity.Property(e => e.ClassName)
                .HasColumnType("character varying")
                .HasColumnName("class_name");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.ClassroomId).HasName("Classrooms_pkey");

            entity.Property(e => e.ClassroomId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("classroom_id");
            entity.Property(e => e.ClassroomName)
                .HasColumnType("character varying")
                .HasColumnName("classroom_name");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("Lessons_pkey");

            entity.Property(e => e.LessonId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("lesson_id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ClassroomId).HasColumnName("classroom_id");
            entity.Property(e => e.DayId).HasColumnName("day_id");
            entity.Property(e => e.LtimeId).HasColumnName("ltime_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Class).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("class_lesson");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("classroom_lesson");

            entity.HasOne(d => d.Day).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.DayId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("weekday_lesson");

            entity.HasOne(d => d.Ltime).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.LtimeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ltime_lessons");

            entity.HasOne(d => d.Subject).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("subject_lesson");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("teacher_lesson");
        });

        modelBuilder.Entity<LessonTime>(entity =>
        {
            entity.HasKey(e => e.LtimeId).HasName("LessonTimes_pkey");

            entity.Property(e => e.LtimeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ltime_id");
            entity.Property(e => e.Lendtime).HasColumnName("lendtime");
            entity.Property(e => e.Lnumber).HasColumnName("lnumber");
            entity.Property(e => e.Lstarttime).HasColumnName("lstarttime");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("Subjects_pkey");

            entity.Property(e => e.SubjectId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("subject_id");
            entity.Property(e => e.SubjectName)
                .HasColumnType("character varying")
                .HasColumnName("subject_name");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("Teachers_pkey");

            entity.Property(e => e.TeacherId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("teacher_id");
            entity.Property(e => e.TeacherName)
                .HasColumnType("character varying")
                .HasColumnName("teacher_name");
        });

        modelBuilder.Entity<TimetableAll>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Timetable_All");

            entity.Property(e => e.Classroom)
                .HasColumnType("character varying")
                .HasColumnName("classroom");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Grade)
                .HasColumnType("character varying")
                .HasColumnName("grade");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LessonNum).HasColumnName("lesson_num");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Subject)
                .HasColumnType("character varying")
                .HasColumnName("subject");
            entity.Property(e => e.Teacher)
                .HasColumnType("character varying")
                .HasColumnName("teacher");
        });

        modelBuilder.Entity<Weekday>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("Weekday_pkey");

            entity.ToTable("Weekday");

            entity.Property(e => e.DayId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("day_id");
            entity.Property(e => e.DayName)
                .HasColumnType("character varying")
                .HasColumnName("day_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
