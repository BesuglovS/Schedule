﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Schedule.DataLayer;
using Schedule.DataLayer.Migrations;
using Schedule.DomainClasses.Config;
using Schedule.DomainClasses.Main;
using Schedule.DomainClasses.Logs;
using Calendar = Schedule.DomainClasses.Main.Calendar;
using System.IO;

namespace Schedule.Repositories
{
    public class ScheduleRepository : IDisposable
    {
        #region Global
        public ScheduleRepository()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScheduleContext, Configuration>());
        }
        
        public void RecreateDB()
        {
            using (var context = new ScheduleContext())
            {
                context.Database.Delete();
                context.Database.CreateIfNotExists();
            }
        }

        public void DebugLog(string message)
        {
            var sw = new StreamWriter("DebugLog.txt", true);
            sw.WriteLine(message);
            sw.Close();
        }
        #endregion

        #region IDisposable
        private void Dispose(bool b)
        {
            
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #region AuditoriumRepository
        public List<Auditorium> GetAllAuditoriums()
        {
            using (var context = new ScheduleContext())
            {
                return context.Auditoriums.ToList();
            }
        }

        public List<Auditorium> GetFiltredAuditoriums(Func<Auditorium, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Auditoriums.ToList().Where(condition).ToList();
            }
        }

        public Auditorium GetFirstFiltredAuditoriums(Func<Auditorium, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Auditoriums.ToList().FirstOrDefault(condition);
            }
        }

        public Auditorium GetAuditorium(int auditoriumId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == auditoriumId);
            }
        }

        public Auditorium FindAuditorium(string name)
        {
            using (var context = new ScheduleContext())
            {
                return context.Auditoriums.FirstOrDefault(a => a.Name == name);
            }
        }

        public void AddAuditorium(Auditorium aud)
        {
            using (var context = new ScheduleContext())
            {
                aud.AuditoriumId = 0;

                context.Auditoriums.Add(aud);
                context.SaveChanges();
            }
        }

        public void UpdateAuditorium(Auditorium aud)
        {
            using (var context = new ScheduleContext())
            {
                var curAud = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == aud.AuditoriumId);

                curAud.Name = aud.Name;

                context.SaveChanges();
            }
        }

        public void RemoveAuditorium(int auditoriumId)
        {
            using (var context = new ScheduleContext())
            {
                var aud = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == auditoriumId);

                context.Auditoriums.Remove(aud);
                context.SaveChanges();
            }
        }

        public void AddAuditoriumRange(IEnumerable<Auditorium> audList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var aud in audList)
                {
                    aud.AuditoriumId = 0;
                    context.Auditoriums.Add(aud);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region CalendarRepository
        public List<Calendar> GetAllCalendars()
        {
            using (var context = new ScheduleContext())
            {
                return context.Calendars.ToList();
            }
        }

        public List<Calendar> GetFiltredCalendars(Func<Calendar, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Calendars.ToList().Where(condition).ToList();
            }
        }

        public Calendar GetFirstFiltredCalendar(Func<Calendar, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Calendars.ToList().FirstOrDefault(condition);
            }
        }

        public Calendar GetCalendar(int calendarId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Calendars.FirstOrDefault(c => c.CalendarId == calendarId);
            }
        }

        public Calendar FindCalendar(DateTime date)
        {
            using (var context = new ScheduleContext())
            {
                return context.Calendars.FirstOrDefault(c => c.Date == date);
            }
        }

        public void AddCalendar(Calendar calendar)
        {
            using (var context = new ScheduleContext())
            {
                calendar.CalendarId = 0;

                context.Calendars.Add(calendar);
                context.SaveChanges();
            }
        }

        public void UpdateCalendar(Calendar calendar)
        {
            using (var context = new ScheduleContext())
            {
                var curCalendar = context.Calendars.FirstOrDefault(c => c.CalendarId == calendar.CalendarId);

                curCalendar.Date = calendar.Date;

                context.SaveChanges();
            }
        }

        public void RemoveCalendar(int calendarId)
        {
            using (var context = new ScheduleContext())
            {
                var calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == calendarId);

                context.Calendars.Remove(calendar);
                context.SaveChanges();
            }
        }

        public void AddCalendarRange(IEnumerable<Calendar> calendarList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var calendar in calendarList)
                {
                    calendar.CalendarId = 0;
                    context.Calendars.Add(calendar);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region RingRepository
        public List<Ring> GetAllRings()
        {
            using (var context = new ScheduleContext())
            {
                return context.Rings.ToList();
            }
        }

        public List<Ring> GetFiltredRings(Func<Ring, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Rings.ToList().Where(condition).ToList();
            }
        }

        public Ring GetFirstFiltredRing(Func<Ring, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Rings.ToList().FirstOrDefault(condition);
            }
        }

        public Ring GetRing(int ringId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Rings.FirstOrDefault(r => r.RingId == ringId);
            }
        }

        public Ring FindRing(DateTime time)
        {
            using (var context = new ScheduleContext())
            {
                return context.Rings.FirstOrDefault(r => ((r.Time.Hour == time.Hour) && (r.Time.Minute == time.Minute)));
            }
        }

        public void AddRing(Ring ring)
        {
            using (var context = new ScheduleContext())
            {
                ring.RingId = 0;

                context.Rings.Add(ring);
                context.SaveChanges();
            }
        }

        public void UpdateRing(Ring ring)
        {
            using (var context = new ScheduleContext())
            {
                var curRing = context.Rings.FirstOrDefault(r => r.RingId == ring.RingId);

                curRing.Time = ring.Time;

                context.SaveChanges();
            }
        }

        public void RemoveRing(int ringId)
        {
            using (var context = new ScheduleContext())
            {
                var ring = context.Rings.FirstOrDefault(r => r.RingId == ringId);

                context.Rings.Remove(ring);
                context.SaveChanges();
            }
        }

        public void AddRingRange(IEnumerable<Ring> ringList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var ring in ringList)
                {
                    ring.RingId = 0;
                    context.Rings.Add(ring);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region StudentRepository
        public List<Student> GetAllStudents()
        {
            using (var context = new ScheduleContext())
            {
                return context.Students.ToList();
            }
        }

        public List<Student> GetGroupStudents(string groupName)
        {
            using (var context = new ScheduleContext())
            {
                var group = context.StudentGroups.FirstOrDefault(g => g.Name == groupName);
                if (group == null)
                {
                    return null;
                }

                return context.StudentsInGroups.Where(sig => sig.StudentGroup.StudentGroupId == group.StudentGroupId).Select(stig => stig.Student).ToList();
            }
        }

        public List<Student> GetFiltredStudents(Func<Student, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Students.ToList().Where(condition).ToList();
            }
        }

        public Student GetFirstFiltredStudents(Func<Student, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Students.ToList().FirstOrDefault(condition);
            }
        }

        public Student GetStudent(int studentId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Students.FirstOrDefault(s => s.StudentId == studentId);
            }
        }

        public Student FindStudent(string f, string i, string o)
        {
            using (var context = new ScheduleContext())
            {
                return context.Students.FirstOrDefault(s => s.F == f && s.I == i && s.O == o);
            }
        }

        public Student FindStudent(string zachNumber)
        {
            using (var context = new ScheduleContext())
            {
                return context.Students.FirstOrDefault(s => s.ZachNumber == zachNumber);
            }
        }

        public void AddStudent(Student student)
        {
            using (var context = new ScheduleContext())
            {
                student.StudentId = 0;

                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var context = new ScheduleContext())
            {
                var curStudent = context.Students.FirstOrDefault(s => s.StudentId == student.StudentId);

                curStudent.F = student.F;
                curStudent.I = student.I;
                curStudent.O = student.O;

                curStudent.ZachNumber = student.ZachNumber;
                curStudent.BirthDate = student.BirthDate;
                curStudent.Address = student.Address;
                curStudent.Phone = student.Phone;
                curStudent.Starosta = student.Starosta;
                curStudent.NFactor = student.NFactor;
                curStudent.PaidEdu = student.PaidEdu;
                curStudent.Expelled = student.Expelled;
                curStudent.Orders = student.Orders;

                context.SaveChanges();
            }
        }

        public void RemoveStudent(int studentId)
        {
            using (var context = new ScheduleContext())
            {
                var student = context.Students.FirstOrDefault(s => s.StudentId == studentId);

                context.Students.Remove(student);
                context.SaveChanges();
            }
        }

        public void AddStudentRange(IEnumerable<Student> studentList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var student in studentList)
                {
                    student.StudentId = 0;
                    context.Students.Add(student);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region StudentGroupRepository
        public List<StudentGroup> GetAllStudentGroups()
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentGroups.ToList();
            }
        }

        public List<StudentGroup> GetFiltredStudentGroups(Func<StudentGroup, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentGroups.ToList().Where(condition).ToList();
            }
        }

        public StudentGroup GetFirstFiltredStudentGroups(Func<StudentGroup, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentGroups.ToList().FirstOrDefault(condition);
            }
        }

        public StudentGroup GetStudentGroup(int studentGroupId)
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentGroupId);
            }
        }

        public StudentGroup FindStudentGroup(string name)
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentGroups.FirstOrDefault(sg => sg.Name == name);
            }
        }

        public void AddStudentGroup(StudentGroup studentGroup)
        {
            using (var context = new ScheduleContext())
            {
                studentGroup.StudentGroupId = 0;

                context.StudentGroups.Add(studentGroup);
                context.SaveChanges();
            }
        }

        public void UpdateStudentGroup(StudentGroup studentGroup)
        {
            using (var context = new ScheduleContext())
            {
                var curStudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentGroup.StudentGroupId);

                curStudentGroup.Name = studentGroup.Name;

                context.SaveChanges();
            }
        }

        public void RemoveStudentGroup(int studentGroupId)
        {
            using (var context = new ScheduleContext())
            {
                var studentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentGroupId);

                context.StudentGroups.Remove(studentGroup);
                context.SaveChanges();
            }
        }

        public void AddStudentGroupRange(IEnumerable<StudentGroup> studentGroupList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var studentGroup in studentGroupList)
                {
                    studentGroup.StudentGroupId = 0;
                    context.StudentGroups.Add(studentGroup);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region TeacherRepository
        public List<Teacher> GetAllTeachers()
        {
            using (var context = new ScheduleContext())
            {
                return context.Teachers.ToList();
            }
        }

        public List<Teacher> GetFiltredTeachers(Func<Teacher, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Teachers.ToList().Where(condition).ToList();
            }
        }

        public Teacher GetFirstFiltredTeachers(Func<Teacher, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Teachers.ToList().FirstOrDefault(condition);
            }
        }

        public Teacher GetTeacher(int teacherId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
            }
        }

        public Teacher FindTeacher(string fio, string phone)
        {
            using (var context = new ScheduleContext())
            {
                return context.Teachers.FirstOrDefault(t => t.FIO == fio && t.Phone == phone);
            }
        }

        public void AddTeacher(Teacher teacher)
        {
            using (var context = new ScheduleContext())
            {
                teacher.TeacherId = 0;

                context.Teachers.Add(teacher);
                context.SaveChanges();
            }
        }

        public void UpdateTeacher(Teacher teacher)
        {
            using (var context = new ScheduleContext())
            {
                var curTeacher = context.Teachers.FirstOrDefault(t => t.TeacherId == teacher.TeacherId);

                curTeacher.FIO = teacher.FIO;
                curTeacher.Phone = teacher.Phone;

                context.SaveChanges();
            }
        }

        public void RemoveTeacher(int teacherId)
        {
            using (var context = new ScheduleContext())
            {
                var teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);

                context.Teachers.Remove(teacher);
                context.SaveChanges();
            }
        }

        public void AddTeacherRange(IEnumerable<Teacher> teacherList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var teacher in teacherList)
                {
                    teacher.TeacherId = 0;
                    context.Teachers.Add(teacher);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region DisciplineRepository
        public List<Discipline> GetAllDisciplines()
        {
            using (var context = new ScheduleContext())
            {
                return context.Disciplines.Include(d => d.StudentGroup).ToList();
            }
        }

        public List<Discipline> GetFiltredDisciplines(Func<Discipline, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Disciplines.Include(d => d.StudentGroup).ToList().Where(condition).ToList();
            }
        }

        public List<Discipline> GetTeacherDisciplines(Teacher teacher)
        {
            using (var context = new ScheduleContext())
            {
                return context.TeacherForDiscipline.Where(tfd => tfd.Teacher.TeacherId == teacher.TeacherId).Select(tefd => tefd.Discipline).Include(d => d.StudentGroup).ToList();
            }
        }

        public Discipline GetFirstFiltredDisciplines(Func<Discipline, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Disciplines.Include(d => d.StudentGroup).ToList().FirstOrDefault(condition);
            }
        }

        public Discipline GetDiscipline(int disciplineId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Disciplines.Include(d => d.StudentGroup).FirstOrDefault(d => d.DisciplineId == disciplineId);
            }
        }

        public Discipline FindDiscipline(string name, int attestation, int auditoriumHours, int lectureHours, int practicalHours, string groupName)
        {
            using (var context = new ScheduleContext())
            {
                return context.Disciplines.Include(d => d.StudentGroup).FirstOrDefault(
                    d => d.Name == name &&
                         d.Attestation == attestation &&
                         d.AuditoriumHours == auditoriumHours &&
                         d.LectureHours == lectureHours &&
                         d.PracticalHours == practicalHours &&
                         d.StudentGroup.Name == groupName);
            }
        }

        public void AddDiscipline(Discipline discipline)
        {
            using (var context = new ScheduleContext())
            {
                discipline.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == discipline.StudentGroup.StudentGroupId);
                context.Disciplines.Add(discipline);

                context.SaveChanges();
            }
        }

        public void UpdateDiscipline(Discipline discipline)
        {
            using (var context = new ScheduleContext())
            {
                var curDiscipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == discipline.DisciplineId);

                curDiscipline.Attestation = discipline.Attestation;
                curDiscipline.AuditoriumHours = discipline.AuditoriumHours;
                curDiscipline.LectureHours = discipline.LectureHours;
                curDiscipline.Name = discipline.Name;
                curDiscipline.PracticalHours = discipline.PracticalHours;
                var disciplineGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == discipline.StudentGroup.StudentGroupId);
                curDiscipline.StudentGroup = disciplineGroup;

                context.SaveChanges();
            }
        }

        public void RemoveDiscipline(int disciplineId)
        {
            using (var context = new ScheduleContext())
            {
                var discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == disciplineId);

                context.Disciplines.Remove(discipline);
                context.SaveChanges();
            }
        }

        public void AddDisciplineRange(IEnumerable<Discipline> disciplineList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var discipline in disciplineList)
                {
                    var disciplineGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == discipline.StudentGroup.StudentGroupId);
                    discipline.StudentGroup = disciplineGroup;
                    context.Disciplines.Add(discipline);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region StudentsInGroupsRepository
        public List<StudentsInGroups> GetAllStudentsInGroups()
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentsInGroups.Include(sig => sig.Student).Include(sig => sig.StudentGroup).ToList();
            }
        }

        public List<StudentsInGroups> GetFiltredStudentsInGroups(Func<StudentsInGroups, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentsInGroups.Include(sig => sig.Student).Include(sig => sig.StudentGroup).ToList().Where(condition).ToList();
            }
        }

        public StudentsInGroups GetFirstFiltredStudentsInGroups(Func<StudentsInGroups, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentsInGroups.Include(sig => sig.Student).Include(sig => sig.StudentGroup).ToList().FirstOrDefault(condition);
            }
        }

        public StudentsInGroups GetStudentsInGroups(int studentsInGroupsId)
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentsInGroups.Include(sig => sig.Student).Include(sig => sig.StudentGroup).FirstOrDefault(sig => sig.StudentsInGroupsId == studentsInGroupsId);
            }
        }

        public StudentsInGroups FindStudentsInGroups(Student s, StudentGroup sg)
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentsInGroups.Include(sig => sig.Student).Include(sig => sig.StudentGroup).FirstOrDefault(sig => sig.Student.StudentId == s.StudentId && sig.StudentGroup.StudentGroupId == sg.StudentGroupId);
            }
        }

        public StudentsInGroups FindStudentsInGroups(string studentF, string studentI, string studentO, string groupName)
        {
            using (var context = new ScheduleContext())
            {
                return context.StudentsInGroups.Include(sig => sig.Student).Include(sig => sig.StudentGroup).FirstOrDefault(sig =>
                    sig.Student.F == studentF &&
                    sig.Student.I == studentI &&
                    sig.Student.O == studentO &&
                    sig.StudentGroup.Name == groupName);
            }
        }

        public void AddStudentsInGroups(StudentsInGroups studentsInGroups)
        {
            using (var context = new ScheduleContext())
            {
                studentsInGroups.StudentsInGroupsId = 0;
                                
                studentsInGroups.Student = context.Students.FirstOrDefault(s => s.StudentId == studentsInGroups.Student.StudentId);
                studentsInGroups.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentsInGroups.StudentGroup.StudentGroupId);

                context.StudentsInGroups.Add(studentsInGroups);
                context.SaveChanges();
            }
        }

        public void UpdateStudentsInGroups(StudentsInGroups studentsInGroups)
        {
            using (var context = new ScheduleContext())
            {
                var curStudentsInGroups = context.StudentsInGroups.FirstOrDefault(sig => sig.StudentsInGroupsId == studentsInGroups.StudentsInGroupsId);

                curStudentsInGroups.Student = context.Students.FirstOrDefault(s => s.StudentId == studentsInGroups.Student.StudentId);
                curStudentsInGroups.StudentGroup = context.StudentGroups.FirstOrDefault(sg => sg.StudentGroupId == studentsInGroups.StudentGroup.StudentGroupId);

                context.SaveChanges();
            }
        }

        public void RemoveStudentsInGroups(int studentsInGroupsId)
        {
            using (var context = new ScheduleContext())
            {
                var studentsInGroups = context.StudentsInGroups.FirstOrDefault(sig => sig.StudentsInGroupsId == studentsInGroupsId);

                context.StudentsInGroups.Remove(studentsInGroups);
                context.SaveChanges();
            }
        }

        public void AddStudentsInGroupsRange(IEnumerable<StudentsInGroups> studentsInGroupsList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var studentsInGroups in studentsInGroupsList)
                {
                    studentsInGroups.StudentsInGroupsId = 0;
                    context.StudentsInGroups.Add(studentsInGroups);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region TeacherForDisciplineRepository
        public List<TeacherForDiscipline> GetAllTeacherForDiscipline()
        {
            using (var context = new ScheduleContext())
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).ToList();
            }
        }

        public List<TeacherForDiscipline> GetFiltredTeacherForDiscipline(Func<TeacherForDiscipline, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).ToList().Where(condition).ToList();
            }
        }

        public TeacherForDiscipline GetFirstFiltredTeacherForDiscipline(Func<TeacherForDiscipline, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).ToList().FirstOrDefault(condition);
            }
        }

        public TeacherForDiscipline GetTeacherForDiscipline(int teacherForDisciplineId)
        {
            using (var context = new ScheduleContext())
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).FirstOrDefault(tfd => tfd.TeacherForDisciplineId == teacherForDisciplineId);
            }
        }

        public TeacherForDiscipline FindTeacherForDiscipline(Teacher t, Discipline d)
        {
            using (var context = new ScheduleContext())
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).FirstOrDefault(tfd => tfd.Teacher.TeacherId == t.TeacherId && tfd.Discipline.DisciplineId == d.DisciplineId);
            }
        }

        public TeacherForDiscipline FindTeacherForDiscipline(string teacherFIO, string disciplineName, int disciplineAttestation,
            int disciplineAuditoriumHours, int disciplineLectureHours, int disciplinePracticalHours, string disciplineGroupName)
        {
            using (var context = new ScheduleContext())
            {
                return context.TeacherForDiscipline.Include(tfd => tfd.Teacher).Include(tfd => tfd.Discipline.StudentGroup).FirstOrDefault(tfd =>
                    tfd.Teacher.FIO == teacherFIO &&
                    tfd.Discipline.Name == disciplineName &&
                    tfd.Discipline.Attestation == disciplineAttestation &&
                    tfd.Discipline.AuditoriumHours == disciplineAuditoriumHours &&
                    tfd.Discipline.LectureHours == disciplineLectureHours &&
                    tfd.Discipline.PracticalHours == disciplinePracticalHours &&
                    tfd.Discipline.StudentGroup.Name == disciplineGroupName);
            }
        }

        public void AddTeacherForDiscipline(TeacherForDiscipline teacherForDiscipline)
        {
            using (var context = new ScheduleContext())
            {
                teacherForDiscipline.TeacherForDisciplineId = 0;

                teacherForDiscipline.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == teacherForDiscipline.Teacher.TeacherId);
                teacherForDiscipline.Discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == teacherForDiscipline.Discipline.DisciplineId);

                context.TeacherForDiscipline.Add(teacherForDiscipline);
                context.SaveChanges();
            }
        }

        public void UpdateTeacherForDiscipline(TeacherForDiscipline teacherForDiscipline)
        {
            using (var context = new ScheduleContext())
            {
                var curTeacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == teacherForDiscipline.TeacherForDisciplineId);

                teacherForDiscipline.Teacher = context.Teachers.FirstOrDefault(t => t.TeacherId == teacherForDiscipline.Teacher.TeacherId);
                teacherForDiscipline.Discipline = context.Disciplines.FirstOrDefault(d => d.DisciplineId == teacherForDiscipline.Discipline.DisciplineId);

                context.SaveChanges();
            }
        }

        public void RemoveTeacherForDiscipline(int teacherForDisciplineId)
        {
            using (var context = new ScheduleContext())
            {
                var teacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == teacherForDisciplineId);

                context.TeacherForDiscipline.Remove(teacherForDiscipline);
                context.SaveChanges();
            }
        }

        public void AddTeacherForDisciplineRange(IEnumerable<TeacherForDiscipline> teacherForDisciplineList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var teacherForDiscipline in teacherForDisciplineList)
                {
                    teacherForDiscipline.TeacherForDisciplineId = 0;
                    context.TeacherForDiscipline.Add(teacherForDiscipline);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region LessonRepository
        public List<Lesson> GetAllLessons()
        {
            using (var context = new ScheduleContext())
            {
                return context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .ToList();
            }
        }

        public List<Lesson> GetAllActiveLessons()
        {
            using (var context = new ScheduleContext())
            {
                return context.Lessons.Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .Where(l => l.IsActive).ToList();
            }
        }

        public List<Lesson> GetFiltredLessons(Func<Lesson, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Lessons.Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .ToList().Where(condition).ToList();
            }
        }

        public Lesson GetFirstFiltredLesson(Func<Lesson, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public Lesson GetLesson(int lessonId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .FirstOrDefault(l => l.LessonId == lessonId);
            }
        }
        
        public void AddLessonWOLog(Lesson lesson)
        {
            using (var context = new ScheduleContext())
            {
                lesson.LessonId = 0;

                lesson.TeacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == lesson.TeacherForDiscipline.TeacherForDisciplineId);
                lesson.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == lesson.Calendar.CalendarId);
                lesson.Ring = context.Rings.FirstOrDefault(r => r.RingId == lesson.Ring.RingId);
                lesson.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == lesson.Auditorium.AuditoriumId);

                context.Lessons.Add(lesson);

                context.SaveChanges();
            }
        }

        public void AddLesson(Lesson lesson, string publicComment = "", string hiddenComment = "")
        {
            using (var context = new ScheduleContext())
            {
                lesson.LessonId = 0;

                lesson.TeacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == lesson.TeacherForDiscipline.TeacherForDisciplineId);
                lesson.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == lesson.Calendar.CalendarId);
                lesson.Ring = context.Rings.FirstOrDefault(r => r.RingId == lesson.Ring.RingId);
                lesson.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == lesson.Auditorium.AuditoriumId);

                context.Lessons.Add(lesson);

                context.LessonLog.Add(
                    new LessonLogEvent
                    {
                        OldLesson = null,
                        NewLesson = lesson,
                        DateTime = DateTime.Now,
                        PublicComment = publicComment,
                        HiddenComment = hiddenComment
                    }
                );
                context.SaveChanges();
            }
        }

        public void UpdateLesson(Lesson lesson)
        {
            using (var context = new ScheduleContext())
            {
                var curLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lesson.LessonId);

                curLesson.Auditorium = lesson.Auditorium;
                curLesson.Calendar = lesson.Calendar;
                curLesson.IsActive = lesson.IsActive;
                curLesson.Ring = lesson.Ring;
                curLesson.TeacherForDiscipline = lesson.TeacherForDiscipline;

                context.SaveChanges();
            }
        }

        public void RemoveLessonWOLog(int lessonId)
        {
            using (var context = new ScheduleContext())
            {
                var lesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonId);

                context.Lessons.Remove(lesson);

                context.SaveChanges();
            }
        }
        
        public void RemoveLessonActiveStateWOLog(int lessonId)
        {
            using (var context = new ScheduleContext())
            {
                var curLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonId);

                curLesson.IsActive = false;

                context.SaveChanges();
            }
        }

        public void RemoveLesson(int lessonId, string publicComment = "", string hiddenComment = "")
        {
            using (var context = new ScheduleContext())
            {
                var lesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonId);

                //_context.Lessons.Remove(lesson);
                lesson.IsActive = false;

                context.LessonLog.Add(
                    new LessonLogEvent
                    {
                        OldLesson = lesson,
                        NewLesson = null,
                        DateTime = DateTime.Now,
                        PublicComment = publicComment,
                        HiddenComment = hiddenComment
                    }
                );

                context.SaveChanges();
            }
        }

        public void AddLessonRange(IEnumerable<Lesson> lessonList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var lesson in lessonList)
                {
                    lesson.LessonId = 0;

                    lesson.TeacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == lesson.TeacherForDiscipline.TeacherForDisciplineId);
                    lesson.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == lesson.Calendar.CalendarId);
                    lesson.Ring = context.Rings.FirstOrDefault(r => r.RingId == lesson.Ring.RingId);
                    lesson.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == lesson.Auditorium.AuditoriumId);

                    context.Lessons.Add(lesson);
                }

                context.SaveChanges();
            }
        }

        public List<Lesson> GetGroupLessons(string groupName)
        {
            using (var context = new ScheduleContext())
            {
                return context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .Where(l => l.TeacherForDiscipline.Discipline.StudentGroup.Name == groupName && l.IsActive).ToList();
            }
        }

        public Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>> GetGroupedGroupLessons(int groupId, DateTime semesterStarts)
        {
            using (var context = new ScheduleContext())
            {
                // Понедельник 08:00 - {tfdId - {weeks + List<Lesson>}}
                var result = new Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>();

                var studentIds = context.StudentsInGroups
                    .Where(sig => sig.StudentGroup.StudentGroupId == groupId)
                    .ToList()
                    .Select(stig => stig.Student.StudentId);
                var groupsListIds = context.StudentsInGroups
                    .Where(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup.StudentGroupId);

                var primaryList = context.Lessons
                    .Include(l => l.TeacherForDiscipline.Teacher)
                    .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .Where(l => groupsListIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) && l.IsActive)
                    .ToList();
                
                var groupedLessons = primaryList.GroupBy(l => Constants.Constants.DOWRemap[(int)(l.Calendar.Date).DayOfWeek] * 2000 +
                    l.Ring.Time.Hour * 60 + l.Ring.Time.Minute,
                    (dow, lessons) =>
                    new
                    {
                        DOW = dow / 2000,
                        time = ((dow - (dow / 2000) * 2000) / 60).ToString("D2") + ":" + ((dow - (dow / 2000) * 2000) - ((dow - (dow / 2000) * 2000) / 60) * 60).ToString("D2"),
                        Groups = lessons.GroupBy(ls => ls.TeacherForDiscipline,
                            (tfd, tfdLessons) =>
                            new
                            {
                                TFDForLessonGroup = tfd,
                                Weeks = "",
                                Lessons = tfdLessons
                            }
                        )
                    }
                ).OrderBy(l => l.DOW * 2000 + int.Parse(l.time.Split(':')[0]) * 60 + int.Parse(l.time.Split(':')[1]));

                foreach (var dateTimeLessons in groupedLessons)
                {
                    var dowLocal = dateTimeLessons.DOW;

                    result.Add(dowLocal + " " + dateTimeLessons.time, new Dictionary<int, Tuple<string, List<Lesson>>>());

                    foreach (var lessonGroup in dateTimeLessons.Groups)
                    {
                        var weekList = lessonGroup.Lessons
                            .Select(lesson => lesson.Calendar.Date.Date - semesterStarts.Date)
                            .Select(diff => (diff.Days / 7) + 1)
                            .ToList();

                        var weekString = CombineWeeks(weekList);

                        result[dowLocal + " " + dateTimeLessons.time].Add(lessonGroup.TFDForLessonGroup.TeacherForDisciplineId, new Tuple<string, List<Lesson>>(weekString, new List<Lesson>()));

                        foreach (var lesson in lessonGroup.Lessons)
                        {
                            result[dowLocal + " " + dateTimeLessons.time][lessonGroup.TFDForLessonGroup.TeacherForDisciplineId].Item2.Add(lesson);
                        }
                    }
                }

                return result;
            }
        }

        public Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>>
            GetFacultyDOWSchedule(int facultyId, int dowRU)
        {
            using (var context = new ScheduleContext())
            {
                // GroupId - 08:00 - {tfdId - {weeks + List<Lesson>}}
                var result = new Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>>();

                var listGroupIds = context
                    .GroupsInFaculties
                    .Where(gif => gif.Faculty.FacultyId == facultyId)
                    .Select(gif => gif.StudentGroup.StudentGroupId)
                    .ToList();

                foreach (var gId in listGroupIds)
                {
                    var groupId = gId;

                    result.Add(groupId, new Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>());

                    var studentIds = context.StudentsInGroups
                        .Where(sig => sig.StudentGroup.StudentGroupId == groupId)
                        .Select(stig => stig.Student.StudentId)
                        .ToList();
                    var groupsListIds = context.StudentsInGroups
                        .Where(sig => studentIds.Contains(sig.Student.StudentId))
                        .Select(sig => sig.StudentGroup.StudentGroupId)
                        .ToList();

                    var primaryList =
                        context.Lessons
                        .Include(l => l.TeacherForDiscipline.Teacher)
                        .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                        .Include(l => l.Calendar)
                        .Include(l => l.Ring)
                        .Include(l => l.Auditorium)
                        .Where(
                            l =>
                            groupsListIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) &&
                            l.IsActive)
                        .ToList();

                    primaryList = primaryList
                        .Where(l => Constants.Constants.DOWRemap[(int)(l.Calendar.Date).DayOfWeek] == dowRU)
                        .ToList();

                    var groupedLessons = primaryList.GroupBy(
                        l => l.Ring.Time.Hour * 60 + l.Ring.Time.Minute,
                        (lTime, lessons) =>
                        new
                            {
                                time = (lTime / 60).ToString("D2") + ":" + (lTime % 60).ToString("D2"),
                                Groups = lessons.GroupBy(ls => ls.TeacherForDiscipline,
                                                           (tfd, tfdLessons) =>
                                                           new
                                                               {
                                                                   TFDForLessonGroup = tfd,
                                                                   Weeks = "",
                                                                   Lessons = tfdLessons
                                                               }
                                                         )
                            }
                        ).OrderBy(l => int.Parse(l.time.Split(':')[0]) * 60 + int.Parse(l.time.Split(':')[1]));


                    foreach (var dateTimeLessons in groupedLessons)
                    {

                        result[groupId].Add(dateTimeLessons.time, new Dictionary<int, Tuple<string, List<Lesson>>>());

                        foreach (var lessonGroup in dateTimeLessons.Groups)
                        {
                            var weekList = lessonGroup.Lessons
                                .Select(lesson => (lesson.Calendar.Date.Date - GetSemesterStarts().Date).Days / 7 + 1)
                                .ToList();

                            var weekString = CombineWeeks(weekList);

                            result[groupId][dateTimeLessons.time].Add(
                                lessonGroup.TFDForLessonGroup.TeacherForDisciplineId,
                                new Tuple<string, List<Lesson>>(weekString, new List<Lesson>()));

                            foreach (var lesson in lessonGroup.Lessons)
                            {
                                result[groupId][dateTimeLessons.time][
                                    lessonGroup.TFDForLessonGroup.TeacherForDisciplineId].Item2.Add(lesson);
                            }
                        }
                    }
                }

                return result;
            }
        }
        #endregion

        #region LessonLogEventRepository
        public List<LessonLogEvent> GetAllLessonLogEvents()
        {
            using (var context = new ScheduleContext())
            {
                return context.LessonLog
                    .Include(e => e.OldLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.OldLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.OldLesson.Calendar)
                    .Include(e => e.OldLesson.Ring)
                    .Include(e => e.OldLesson.Auditorium)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.NewLesson.Calendar)
                    .Include(e => e.NewLesson.Ring)
                    .Include(e => e.NewLesson.Auditorium)
                    .ToList();
            }
        }

        public List<LessonLogEvent> GetFiltredLessonLogEvents(Func<LessonLogEvent, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.LessonLog
                    .Include(e => e.OldLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.OldLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.OldLesson.Calendar)
                    .Include(e => e.OldLesson.Ring)
                    .Include(e => e.OldLesson.Auditorium)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.NewLesson.Calendar)
                    .Include(e => e.NewLesson.Ring)
                    .Include(e => e.NewLesson.Auditorium)
                    .ToList().Where(condition).ToList();
            }
        }

        public LessonLogEvent GetFirstFiltredLessonLogEvents(Func<LessonLogEvent, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.LessonLog
                    .Include(e => e.OldLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.OldLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.OldLesson.Calendar)
                    .Include(e => e.OldLesson.Ring)
                    .Include(e => e.OldLesson.Auditorium)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.NewLesson.Calendar)
                    .Include(e => e.NewLesson.Ring)
                    .Include(e => e.NewLesson.Auditorium)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public LessonLogEvent GetLessonLogEvent(int lessonLogEventId)
        {
            using (var context = new ScheduleContext())
            {
                return context.LessonLog
                    .Include(e => e.OldLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.OldLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.OldLesson.Calendar)
                    .Include(e => e.OldLesson.Ring)
                    .Include(e => e.OldLesson.Auditorium)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Teacher)
                    .Include(e => e.NewLesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(e => e.NewLesson.Calendar)
                    .Include(e => e.NewLesson.Ring)
                    .Include(e => e.NewLesson.Auditorium)
                    .FirstOrDefault(lle => lle.LessonLogEventId == lessonLogEventId);
            }
        }

        public void AddLessonLogEvent(LessonLogEvent lessonLogEvent)
        {
            using (var context = new ScheduleContext())
            {
                lessonLogEvent.LessonLogEventId = 0;
                lessonLogEvent.OldLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonLogEvent.OldLesson.LessonId);
                lessonLogEvent.NewLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonLogEvent.NewLesson.LessonId);

                context.LessonLog.Add(lessonLogEvent);
                context.SaveChanges();
            }
        }

        public void UpdateLessonLogEvent(LessonLogEvent lessonLogEvent)
        {
            using (var context = new ScheduleContext())
            {
                var curLessonLogEvent = context.LessonLog.FirstOrDefault(lle => lle.LessonLogEventId == lessonLogEvent.LessonLogEventId);

                curLessonLogEvent.DateTime = lessonLogEvent.DateTime;
                curLessonLogEvent.HiddenComment = lessonLogEvent.HiddenComment;
                curLessonLogEvent.NewLesson = lessonLogEvent.NewLesson;
                curLessonLogEvent.OldLesson = lessonLogEvent.OldLesson;
                curLessonLogEvent.PublicComment = lessonLogEvent.PublicComment;

                context.SaveChanges();
            }
        }

        public void RemoveLessonLogEvent(int lessonLogEventId)
        {
            using (var context = new ScheduleContext())
            {
                var lessonLogEvent = context.LessonLog.FirstOrDefault(lle => lle.LessonLogEventId == lessonLogEventId);

                context.LessonLog.Remove(lessonLogEvent);
                context.SaveChanges();
            }
        }

        public void AddLessonLogEventRange(IEnumerable<LessonLogEvent> lessonLogEventList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var lessonLogEvent in lessonLogEventList)
                {
                    lessonLogEvent.LessonLogEventId = 0;

                    lessonLogEvent.OldLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonLogEvent.OldLesson.LessonId);
                    lessonLogEvent.NewLesson = context.Lessons.FirstOrDefault(l => l.LessonId == lessonLogEvent.NewLesson.LessonId);

                    context.LessonLog.Add(lessonLogEvent);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region ConfigOptionRepository

        public DateTime GetSemesterStarts()
        {
            using (var context = new ScheduleContext())
            {
                var semesterStartsOption = context.Config
                    .FirstOrDefault(co => co.Key == "Semester Starts");
                if (semesterStartsOption == null)
                {
                    return new DateTime(2000, 1, 1);
                }

                return DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
        }

        public List<ConfigOption> GetAllConfigOptions()
        {
            using (var context = new ScheduleContext())
            {
                return context.Config.ToList();
            }
        }

        public List<ConfigOption> GetFiltredConfigOptions(Func<ConfigOption, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Config.ToList().Where(condition).ToList();
            }
        }

        public ConfigOption GetFirstFiltredConfigOption(Func<ConfigOption, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Config.ToList().FirstOrDefault(condition);
            }
        }

        public ConfigOption GetConfigOption(int configOptionId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Config.FirstOrDefault(co => co.ConfigOptionId == configOptionId);
            }
        }

        public ConfigOption GetConfigOptionByKey(string key)
        {
            using (var context = new ScheduleContext())
            {
                return context.Config.FirstOrDefault(co => co.Key == key);
            }
        }

        public void AddConfigOption(ConfigOption co)
        {
            using (var context = new ScheduleContext())
            {
                context.Config.Add(co);
                context.SaveChanges();
            }
        }

        public void UpdateConfigOption(ConfigOption co)
        {
            using (var context = new ScheduleContext())
            {
                var curCO = context.Config.FirstOrDefault(opt => opt.ConfigOptionId == co.ConfigOptionId);

                curCO.Key = co.Key;
                curCO.Value = co.Value;

                context.SaveChanges();
            }
        }

        public void RemoveConfigOption(int configOptionId)
        {
            using (var context = new ScheduleContext())
            {
                var co = context.Config.FirstOrDefault(opt => opt.ConfigOptionId == configOptionId);

                context.Config.Remove(co);
                context.SaveChanges();
            }
        }

        public void AddConfigOptionRange(IEnumerable<ConfigOption> coList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var co in coList)
                {
                    co.ConfigOptionId = 0;
                    context.Config.Add(co);
                }

                context.SaveChanges();
            }
        }

        public ConfigOption FindConfigOption(string key)
        {
            using (var context = new ScheduleContext())
            {
                return context.Config.FirstOrDefault(op => op.Key == key);
            }
        }
        #endregion
        
        #region FacultyRepository
        public List<Faculty> GetAllFaculties()
        {
            using (var context = new ScheduleContext())
            {
                return context.Faculties.ToList();
            }
        }

        public List<Faculty> GetFiltredFaculties(Func<Faculty, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Faculties.ToList().Where(condition).ToList();
            }
        }

        public Faculty GetFirstFiltredFaculty(Func<Faculty, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.Faculties.ToList().FirstOrDefault(condition);
            }
        }

        public Faculty GetFaculty(int facultyId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Faculties.FirstOrDefault(f => f.FacultyId == facultyId);
            }
        }

        public Faculty FindFaculty(string name)
        {
            using (var context = new ScheduleContext())
            {
                return context.Faculties.FirstOrDefault(f => f.Name == name);
            }
        }

        public void AddFaculty(Faculty faculty)
        {
            using (var context = new ScheduleContext())
            {
                faculty.FacultyId = 0;

                context.Faculties.Add(faculty);
                context.SaveChanges();
            }
        }

        public void UpdateFaculty(Faculty faculty)
        {
            using (var context = new ScheduleContext())
            {
                var curFaculty = context.Faculties.FirstOrDefault(f => f.FacultyId == faculty.FacultyId);

                curFaculty.Name = faculty.Name;
                curFaculty.Letter = faculty.Letter;
                curFaculty.SortingOrder = faculty.SortingOrder;

                context.SaveChanges();
            }
        }

        public void RemoveFaculty(int facultyId)
        {
            using (var context = new ScheduleContext())
            {
                var faculty = context.Faculties.FirstOrDefault(f => f.FacultyId == facultyId);

                context.Faculties.Remove(faculty);
                context.SaveChanges();
            }
        }

        public void AddFacultyRange(IEnumerable<Faculty> facultyList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var faculty in facultyList)
                {
                    faculty.FacultyId = 0;
                    context.Faculties.Add(faculty);
                }

                context.SaveChanges();
            }
        }

        public List<StudentGroup> GetFacultyGroups(int facultyId)
        {
            using (var context = new ScheduleContext())
            {
                return context.GroupsInFaculties
                    .Where(gif => gif.Faculty.FacultyId == facultyId)
                    .Select(gif => gif.StudentGroup)
                    .ToList();
            }
        }
        #endregion

        #region AuditoriumEventsRepository
        public List<AuditoriumEvent> GetAllAuditoriumEvents()
        {
            using (var context = new ScheduleContext())
            {
                return context.AuditoriumEvents
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .ToList();
            }
        }

        public List<AuditoriumEvent> GetFiltredAuditoriumEvents(Func<AuditoriumEvent, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.AuditoriumEvents
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .ToList().Where(condition).ToList();
            }
        }

        public AuditoriumEvent GetFirstFiltredAuditoriumEvent(Func<AuditoriumEvent, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.AuditoriumEvents
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public AuditoriumEvent GetAuditoriumEvent(int auditoriumEventId)
        {
            using (var context = new ScheduleContext())
            {
                return context.AuditoriumEvents
                    .Include(l => l.Calendar)
                    .Include(l => l.Ring)
                    .Include(l => l.Auditorium)
                    .FirstOrDefault(ae => ae.AuditoriumEventId == auditoriumEventId);
            }
        }

        public void AddAuditoriumEvent(AuditoriumEvent ae)
        {
            using (var context = new ScheduleContext())
            {
                ae.AuditoriumEventId = 0;

                ae.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == ae.Calendar.CalendarId);
                ae.Ring = context.Rings.FirstOrDefault(r => r.RingId == ae.Ring.RingId);
                ae.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == ae.Auditorium.AuditoriumId);

                context.AuditoriumEvents.Add(ae);
                context.SaveChanges();
            }
        }

        public void UpdateAuditoriumEvent(AuditoriumEvent ae)
        {
            using (var context = new ScheduleContext())
            {
                var curAE = context.AuditoriumEvents.FirstOrDefault(evt => evt.AuditoriumEventId == ae.AuditoriumEventId);

                curAE.Name = ae.Name;
                curAE.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == ae.Calendar.CalendarId);
                curAE.Ring = context.Rings.FirstOrDefault(r => r.RingId == ae.Ring.RingId);
                curAE.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == ae.Auditorium.AuditoriumId);

                context.SaveChanges();
            }
        }

        public void RemoveAuditoriumEvent(int auditoriumEventId)
        {
            using (var context = new ScheduleContext())
            {
                var aud = context.AuditoriumEvents.FirstOrDefault(evt => evt.AuditoriumEventId == auditoriumEventId);

                context.AuditoriumEvents.Remove(aud);
                context.SaveChanges();
            }
        }

        public void AddAuditoriumEventRange(IEnumerable<AuditoriumEvent> aeList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var ae in aeList)
                {
                    ae.AuditoriumEventId = 0;

                    ae.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == ae.Calendar.CalendarId);
                    ae.Ring = context.Rings.FirstOrDefault(r => r.RingId == ae.Ring.RingId);
                    ae.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == ae.Auditorium.AuditoriumId);

                    context.AuditoriumEvents.Add(ae);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region GroupsInFacultyRepository
        public List<GroupsInFaculty> GetAllGroupsInFaculty()
        {
            using (var context = new ScheduleContext())
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .ToList();
            }
        }

        public List<GroupsInFaculty> GetFiltredGroupsInFaculty(Func<GroupsInFaculty, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .ToList().Where(condition).ToList();
            }
        }

        public GroupsInFaculty GetFirstFiltredGroupsInFaculty(Func<GroupsInFaculty, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .ToList().FirstOrDefault(condition);
            }
        }

        public GroupsInFaculty GetGroupsInFaculty(int groupsInFaculty)
        {
            using (var context = new ScheduleContext())
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .FirstOrDefault(gif => gif.GroupsInFacultyId == groupsInFaculty);
            }
        }

        public GroupsInFaculty FindGroupsInFaculty(StudentGroup sg, Faculty f)
        {
            using (var context = new ScheduleContext())
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .FirstOrDefault(gif => gif.StudentGroup.StudentGroupId == sg.StudentGroupId &&
                                           gif.Faculty.FacultyId == f.FacultyId);
            }
        }

        public GroupsInFaculty FindGroupsInFaculty(string studentGroupName, string facultyName)
        {
            using (var context = new ScheduleContext())
            {
                return context.GroupsInFaculties
                    .Include(gif => gif.StudentGroup)
                    .Include(gif => gif.Faculty)
                    .FirstOrDefault(gif => gif.StudentGroup.Name == studentGroupName &&
                                           gif.Faculty.Name == facultyName);
            }
        }

        public void AddGroupsInFaculty(GroupsInFaculty groupsInFaculty)
        {
            using (var context = new ScheduleContext())
            {
                groupsInFaculty.GroupsInFacultyId = 0;

                groupsInFaculty.StudentGroup = context.StudentGroups.FirstOrDefault(gif => gif.StudentGroupId == groupsInFaculty.StudentGroup.StudentGroupId);
                groupsInFaculty.Faculty = context.Faculties.FirstOrDefault(gif => gif.FacultyId == groupsInFaculty.Faculty.FacultyId);

                context.GroupsInFaculties.Add(groupsInFaculty);
                context.SaveChanges();
            }
        }

        public void UpdateGroupsInFaculty(GroupsInFaculty groupsInFaculty)
        {
            using (var context = new ScheduleContext())
            {
                var curGroupsInFaculty = context.GroupsInFaculties.FirstOrDefault(gif => gif.GroupsInFacultyId == groupsInFaculty.GroupsInFacultyId);

                curGroupsInFaculty.StudentGroup = groupsInFaculty.StudentGroup;
                curGroupsInFaculty.Faculty = groupsInFaculty.Faculty;

                context.SaveChanges();
            }
        }

        public void RemoveGroupsInFaculty(int groupsInFacultyId)
        {
            using (var context = new ScheduleContext())
            {
                var groupsInFaculty = context.GroupsInFaculties.FirstOrDefault(gif => gif.GroupsInFacultyId == groupsInFacultyId);

                context.GroupsInFaculties.Remove(groupsInFaculty);
                context.SaveChanges();
            }
        }

        public void AddGroupsInFacultyRange(IEnumerable<GroupsInFaculty> groupsInFacultiesList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var groupsInFaculty in groupsInFacultiesList)
                {
                    groupsInFaculty.GroupsInFacultyId = 0;

                    groupsInFaculty.StudentGroup = context.StudentGroups.FirstOrDefault(gif => gif.StudentGroupId == groupsInFaculty.StudentGroup.StudentGroupId);
                    groupsInFaculty.Faculty = context.Faculties.FirstOrDefault(gif => gif.FacultyId == groupsInFaculty.Faculty.FacultyId);

                    context.GroupsInFaculties.Remove(groupsInFaculty);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region ScheduleNoteRepository
        public List<ScheduleNote> GetAllScheduleNotes()
        {
            using (var context = new ScheduleContext())
            {
                return context
                    .ScheduleNotes
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Teacher)
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(sn => sn.Lesson.Calendar)
                    .Include(sn => sn.Lesson.Ring)
                    .Include(sn => sn.Lesson.Auditorium)
                    .ToList();
            }
        }

        public List<ScheduleNote> GetFiltredScheduleNotes(Func<ScheduleNote, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context
                    .ScheduleNotes
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Teacher)
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(sn => sn.Lesson.Calendar)
                    .Include(sn => sn.Lesson.Ring)
                    .Include(sn => sn.Lesson.Auditorium)
                    .ToList()
                    .Where(condition)
                    .ToList();
            }
        }

        public ScheduleNote GetFirstFiltredScheduleNotes(Func<ScheduleNote, bool> condition)
        {
            using (var context = new ScheduleContext())
            {
                return context
                    .ScheduleNotes
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Teacher)
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(sn => sn.Lesson.Calendar)
                    .Include(sn => sn.Lesson.Ring)
                    .Include(sn => sn.Lesson.Auditorium)
                    .ToList()
                    .FirstOrDefault(condition);
            }
        }

        public ScheduleNote GetScheduleNote(int ScheduleNoteId)
        {
            using (var context = new ScheduleContext())
            {
                return context
                    .ScheduleNotes
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Teacher)
                    .Include(sn => sn.Lesson.TeacherForDiscipline.Discipline.StudentGroup)
                    .Include(sn => sn.Lesson.Calendar)
                    .Include(sn => sn.Lesson.Ring)
                    .Include(sn => sn.Lesson.Auditorium)
                    .FirstOrDefault(sn => sn.ScheduleNoteId == ScheduleNoteId);
            }
        }

        public void AddScheduleNote(ScheduleNote sNote)
        {
            using (var context = new ScheduleContext())
            {
                sNote.ScheduleNoteId = 0;

                sNote.Lesson.TeacherForDiscipline = context.TeacherForDiscipline.FirstOrDefault(tfd => tfd.TeacherForDisciplineId == sNote.Lesson.TeacherForDiscipline.TeacherForDisciplineId);
                sNote.Lesson.Calendar = context.Calendars.FirstOrDefault(c => c.CalendarId == sNote.Lesson.Calendar.CalendarId);
                sNote.Lesson.Ring = context.Rings.FirstOrDefault(r => r.RingId == sNote.Lesson.Ring.RingId);
                sNote.Lesson.Auditorium = context.Auditoriums.FirstOrDefault(a => a.AuditoriumId == sNote.Lesson.Auditorium.AuditoriumId);

                context.ScheduleNotes.Add(sNote);
                context.SaveChanges();
            }
        }

        public void UpdateScheduleNote(ScheduleNote sNote)
        {
            using (var context = new ScheduleContext())
            {
                var curNote = context.ScheduleNotes.FirstOrDefault(sn => sn.ScheduleNoteId == sNote.ScheduleNoteId);

                curNote.Lesson = sNote.Lesson;
                curNote.Text = sNote.Text;

                context.SaveChanges();
            }
        }

        public void RemoveScheduleNote(int ScheduleNoteId)
        {
            using (var context = new ScheduleContext())
            {
                var sNote = context.ScheduleNotes.FirstOrDefault(sn => sn.ScheduleNoteId == ScheduleNoteId);

                context.ScheduleNotes.Remove(sNote);
                context.SaveChanges();
            }
        }

        public void AddScheduleNoteRange(IEnumerable<ScheduleNote> scheduleNoteList)
        {
            using (var context = new ScheduleContext())
            {
                foreach (var sNote in scheduleNoteList)
                {
                    sNote.ScheduleNoteId = 0;

                    context.ScheduleNotes.Add(sNote);
                }

                context.SaveChanges();
            }
        }
        #endregion

        #region CommonFunctions
        public static string CombineWeeks(List<int> list)
        {
            var result = new List<string>();
            var boolWeeks = new bool[21];

            for (var i = 0; i <= 20; i++)
            {
                boolWeeks[i] = false;
            }

            foreach (var week in list)
            {
                boolWeeks[week] = true;
            }

            bool prev = false;
            int baseNum = 20;
            for (var i = 0; i <= 19; i++)
            {
                if (!prev && boolWeeks[i])
                {
                    baseNum = i;
                }

                if (!boolWeeks[i] && ((i - baseNum) > 2))
                {
                    result.Add(baseNum + "-" + (i - 1));

                    for (var k = baseNum; k < i; k++)
                    {
                        boolWeeks[k] = false;
                    }
                }

                if (!boolWeeks[i])
                {
                    baseNum = 20;
                }

                prev = boolWeeks[i];
            }

            prev = false;
            baseNum = 20;
            for (var i = 1; i <= 19; i += 2)
            {
                if (!prev && boolWeeks[i])
                {
                    baseNum = i;
                }

                if (!boolWeeks[i] && ((i - baseNum) > 4))
                {
                    result.Add(baseNum + "-" + (i - 2) + " (нечёт.)");

                    for (var k = baseNum; k < i; k += 2)
                    {
                        boolWeeks[k] = false;
                    }
                }

                if (!boolWeeks[i])
                {
                    baseNum = 20;
                }

                prev = boolWeeks[i];
            }

            prev = false;
            baseNum = 20;
            for (var i = 2; i <= 20; i += 2)
            {
                if (!prev && boolWeeks[i])
                {
                    baseNum = i;
                }

                if (!boolWeeks[i] && ((i - baseNum) > 4))
                {
                    result.Add(baseNum + "-" + (i - 2) + " (чёт.)");

                    for (var k = baseNum; k < i; k += 2)
                    {
                        boolWeeks[k] = false;
                    }
                }

                if (!boolWeeks[i])
                {
                    baseNum = 20;
                }

                prev = boolWeeks[i];
            }



            for (var i = 1; i <= 18; i++)
            {
                if (boolWeeks[i])
                {
                    result.Add(i.ToString(CultureInfo.InvariantCulture));
                }
            }

            result.Sort((a, b) =>
            {
                int aVal = -1, bVal = -1;

                if (a.Contains('-'))
                {
                    int.TryParse(a.Substring(0, a.IndexOf('-')), out aVal);
                }
                else
                {
                    int.TryParse(a, out aVal);
                }

                if (b.Contains('-'))
                {
                    int.TryParse(b.Substring(0, b.IndexOf('-')), out bVal);
                }
                else
                {
                    int.TryParse(b, out bVal);
                }

                if (aVal > bVal) return 1;
                if (bVal > aVal) return -1;
                return 0;
            });

            var final = result.Aggregate((current, str) => current + ", " + str);
            return final;
        }

        public static List<int> WeeksStringToList(string weeksString, bool removeParentheses = false)
        {
            var result = new List<int>();

            string str = weeksString;
            if (removeParentheses)
            {
                str = weeksString.Substring(0, weeksString.Length - 1);
                str = str.Substring(1, str.Length - 1);
            }

            int mods = 0; // 0 - нет; 1 - нечётные; 2 - чётные

            foreach (var item in str.Split(','))
            {
                var st = item.Trim(' ');

                if (!st.Contains('-'))
                {
                    result.Add(int.Parse(st));
                }
                else
                {
                    mods = 0;

                    if (st.EndsWith(" (нечёт.)"))
                    {
                        st = st.Substring(0, st.Length - 9);
                        mods = 1;
                    }

                    if (st.EndsWith(" (чёт.)"))
                    {
                        st = st.Substring(0, st.Length - 7);
                        mods = 2;
                    }

                    int start = int.Parse(st.Substring(0, st.IndexOf('-')));

                    int end = int.Parse(
                        st.Substring(st.IndexOf('-') + 1, st.Length - st.IndexOf('-') - 1));

                    for (int i = start; i <= end; i++)
                    {
                        switch (mods)
                        {
                            case 0:
                                result.Add(i);
                                break;
                            case 1:
                                if ((i % 2) == 1)
                                    result.Add(i);
                                break;
                            case 2:
                                if ((i % 2) == 0)
                                    result.Add(i);
                                break;
                        }
                    }
                }
            }

            return result;
        }

        public DateTime GetDateFromDowAndWeek(int dow, int week)
        {
            using (var context = new ScheduleContext())
            {
                var semesterStartsOption = context.Config
                    .FirstOrDefault(co => co.Key == "Semester Starts");
                if (semesterStartsOption == null)
                {
                    return new DateTime(2000, 1, 1);
                }

                var semesterStarts = DateTime.ParseExact(semesterStartsOption.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                return semesterStarts.AddDays((week - 1) * 7 + dow - 1);
            }
        }

        public List<Auditorium> GetFreeAuditoriumAtTime(Calendar calendar, Ring ring)
        {
            using (var context = new ScheduleContext())
            {
                var occupiedAudIds = context.Lessons
                    .Where(l =>
                        ((l.Calendar.CalendarId == calendar.CalendarId) &&
                         (l.Ring.RingId == ring.RingId)))
                     .Select(l => l.Auditorium.AuditoriumId)
                     .Distinct()
                     .ToList();

                var result = context.Auditoriums.Where(a => !occupiedAudIds.Contains(a.AuditoriumId)).ToList();

                return result;
            }
        }

        public List<Auditorium> GetFreeAuditoriumAtDOWTime(List<int> calendars, Ring ring)
        {
            using (var context = new ScheduleContext())
            {
                var occupiedAudIds = context.Lessons
                    .Where(l =>
                        ((calendars.Contains(l.Calendar.CalendarId)) &&
                         (l.Ring.RingId == ring.RingId)) &&
                         l.IsActive)
                     .Select(l => l.Auditorium.AuditoriumId)
                     .Distinct()
                     .ToList();

                var result = context.Auditoriums.Where(a => !occupiedAudIds.Contains(a.AuditoriumId)).ToList();

                return result;
            }
        }
                
        public int CalculateWeekNumber(DateTime dateTime)
        {
            return (dateTime - GetSemesterStarts()).Days / 7 + 1;
        }

        public Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>> GetGroupedGroupsLessons(List<int> groupListIds)
        {
            using (var context = new ScheduleContext())
            {
                var semesterStarts = GetSemesterStarts();

                // Понедельник 08:00 - {tfdId - {weeks + List<Lesson>}}
                var result = new Dictionary<int, Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>>();

                foreach (var groupId in groupListIds)
                {
                    result.Add(groupId, new Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>>());

                    var studentIds = context.StudentsInGroups
                        .Where(sig => sig.StudentGroup.StudentGroupId == groupId)
                        .ToList()
                        .Select(stig => stig.Student.StudentId);
                    var groupsListIds = context.StudentsInGroups
                        .Where(sig => studentIds.Contains(sig.Student.StudentId))
                        .ToList()
                        .Select(stig => stig.StudentGroup.StudentGroupId);

                    var primaryList = context.Lessons
                        .Include(l => l.TeacherForDiscipline.Teacher)
                        .Include(l => l.TeacherForDiscipline.Discipline.StudentGroup)
                        .Include(l => l.Calendar)
                        .Include(l => l.Ring)
                        .Include(l => l.Auditorium)
                        .Where(l => groupsListIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId) && l.IsActive)
                        .ToList();
                    
                    var groupedLessons = primaryList.GroupBy(l => Constants.Constants.DOWRemap[(int)(l.Calendar.Date).DayOfWeek] * 2000 +
                        l.Ring.Time.Hour * 60 + l.Ring.Time.Minute,
                        (dow, lessons) =>
                        new
                        {
                            DOW = dow / 2000,
                            time = ((dow - (dow / 2000) * 2000) / 60).ToString("D2") + ":" + ((dow - (dow / 2000) * 2000) - ((dow - (dow / 2000) * 2000) / 60) * 60).ToString("D2"),
                            Groups = lessons.GroupBy(ls => ls.TeacherForDiscipline,
                                (tfd, tfdLessons) =>
                                new
                                {
                                    TFDForLessonGroup = tfd,
                                    Weeks = "",
                                    Lessons = tfdLessons
                                }
                            )
                        }
                    ).OrderBy(l => l.DOW * 2000 + int.Parse(l.time.Split(':')[0]) * 60 + int.Parse(l.time.Split(':')[1]));

                    foreach (var dateTimeLessons in groupedLessons)
                    {
                        //var dowLocal = Constants.Constants.DOWLocal[DateTimeLessons.DOW];
                        var dowLocal = dateTimeLessons.DOW;

                        result[groupId].Add(dowLocal + " " + dateTimeLessons.time, new Dictionary<int, Tuple<string, List<Lesson>>>());

                        foreach (var lessonGroup in dateTimeLessons.Groups)
                        {
                            var weekList = lessonGroup.Lessons
                                .Select(lesson => lesson.Calendar.Date.Date - semesterStarts.Date)
                                .Select(diff => (diff.Days / 7) + 1)
                                .ToList();

                            var weekString = CombineWeeks(weekList);

                            result[groupId][dowLocal + " " + dateTimeLessons.time].Add(lessonGroup.TFDForLessonGroup.TeacherForDisciplineId, new Tuple<string, List<Lesson>>(weekString, new List<Lesson>()));

                            foreach (var lesson in lessonGroup.Lessons)
                            {
                                result[groupId][dowLocal + " " + dateTimeLessons.time][lessonGroup.TFDForLessonGroup.TeacherForDisciplineId].Item2.Add(lesson);
                            }
                        }
                    }
                }

                return result;
            }
        }

        private int AuditoriumBuilding(string auditoriumName)
        {
            if (auditoriumName.StartsWith("Корп № 3"))
            {
                return 3;
            }

            if (Char.IsDigit(auditoriumName[5]) || auditoriumName == "Ауд. ШКОЛА")
            {
                return 2;
            }

            return 0;
        }

        // data   - Dictionary<RingId, Dictionary <AuditoriumId, List<Dictionary<tfd, List<Lesson>>>>>
        // result - Dictionary<RingId, Dictionary <AuditoriumId, List<tfd/Event-string>>>
        public Dictionary<int, Dictionary<int, List<string>>> getDOWAuds(DayOfWeek dow, int weekNumber, int buildingNumber)
        {
            var data = new Dictionary<int, Dictionary<int, Dictionary<int, List<Lesson>>>>();

            List<Lesson> dowLessons;
            if (weekNumber == -1)
            {
                if (buildingNumber == -1)
                {
                    dowLessons = GetFiltredLessons(l => l.Calendar.Date.DayOfWeek == dow && l.IsActive).ToList();
                }
                else
                {
                    dowLessons = GetFiltredLessons(l => l.Calendar.Date.DayOfWeek == dow && l.IsActive && AuditoriumBuilding(l.Auditorium.Name) == buildingNumber).ToList();
                }
            }
            else
            {
                if (buildingNumber == -1)
                {
                    dowLessons = GetFiltredLessons(l =>
                            l.Calendar.Date.DayOfWeek == dow &&
                            l.IsActive &&
                            CalculateWeekNumber(l.Calendar.Date) == weekNumber)
                        .ToList();
                }
                else
                {
                    dowLessons = GetFiltredLessons(l =>
                            l.Calendar.Date.DayOfWeek == dow &&
                            l.IsActive &&
                            CalculateWeekNumber(l.Calendar.Date) == weekNumber && 
                            AuditoriumBuilding(l.Auditorium.Name) == buildingNumber) 
                        .ToList();
                }
            }

            foreach (var lesson in dowLessons)
            {
                if (!data.ContainsKey(lesson.Ring.RingId))
                {
                    data.Add(lesson.Ring.RingId, new Dictionary<int,Dictionary<int,List<Lesson>>>());
                }

                if (!data[lesson.Ring.RingId].ContainsKey(lesson.Auditorium.AuditoriumId))
                {
                    data[lesson.Ring.RingId].Add(lesson.Auditorium.AuditoriumId, new Dictionary<int, List<Lesson>>());
                }

                if (!data[lesson.Ring.RingId][lesson.Auditorium.AuditoriumId].ContainsKey(lesson.TeacherForDiscipline.TeacherForDisciplineId))
                {
                    data[lesson.Ring.RingId][lesson.Auditorium.AuditoriumId].Add(lesson.TeacherForDiscipline.TeacherForDisciplineId, new List<Lesson>());
                }

                data[lesson.Ring.RingId][lesson.Auditorium.AuditoriumId][lesson.TeacherForDiscipline.TeacherForDisciplineId].Add(lesson);                
            }

            var rings = GetAllRings().ToDictionary(r => r.RingId, r => r.Time);

            data = data
                .OrderBy(a => rings[a.Key].TimeOfDay)
                .ToDictionary(a => a.Key, a => a.Value);

            var result = new Dictionary<int, Dictionary<int, List<string>>>();

            foreach (var ring in data)
            {
                result.Add(ring.Key, new Dictionary<int, List<string>>());

                foreach (var aud in ring.Value)
                {
                    result[ring.Key].Add(aud.Key, new List<string>());

                    foreach (var tfd in aud.Value)
                    {                        
                        result[ring.Key][aud.Key].Add(tfd.Value[0].TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine + 
                            "(" + GetWeekStringFromLessons(tfd.Value) + ")@" + 
                            tfd.Value[0].TeacherForDiscipline.Teacher.FIO +"@"+ tfd.Value[0].TeacherForDiscipline.Discipline.Name);
                    }
                }
            }

            List<AuditoriumEvent> audEvents;
            if (weekNumber == -1)
            {
                if (buildingNumber == -1)
                {
                    audEvents = GetFiltredAuditoriumEvents(evt => evt.Calendar.Date.DayOfWeek == dow);
                }
                else
                {
                    audEvents = GetFiltredAuditoriumEvents(evt => evt.Calendar.Date.DayOfWeek == dow && AuditoriumBuilding(evt.Auditorium.Name) == buildingNumber);
                }
            }
            else
            {
                if (buildingNumber == -1)
                {
                    audEvents = GetFiltredAuditoriumEvents(evt => 
                        evt.Calendar.Date.DayOfWeek == dow && 
                        CalculateWeekNumber(evt.Calendar.Date) == weekNumber);
                }
                else
                {
                    audEvents = GetFiltredAuditoriumEvents(evt =>
                        evt.Calendar.Date.DayOfWeek == dow &&
                        CalculateWeekNumber(evt.Calendar.Date) == weekNumber &&
                        AuditoriumBuilding(evt.Auditorium.Name) == buildingNumber);
                }
            }

            var eventId = 0;
            int curEventId;
            var eventsIds = new Dictionary<int, string>();
            var eventsData = new Dictionary<int, Dictionary<int, Dictionary<int, List<AuditoriumEvent>>>>();
            
            foreach (var evt in audEvents)
            {
                if (!eventsData.ContainsKey(evt.Ring.RingId))
                {
                    eventsData.Add(evt.Ring.RingId, new Dictionary<int,Dictionary<int,List<AuditoriumEvent>>>());
                }

                if (!eventsData[evt.Ring.RingId].ContainsKey(evt.Auditorium.AuditoriumId))
                {
                    eventsData[evt.Ring.RingId].Add(evt.Auditorium.AuditoriumId, new Dictionary<int,List<AuditoriumEvent>>());
                }

                var eventFound = (eventsIds.Count(e => e.Value == evt.Name) > 0) ? true : false;
                if (eventFound)
                {
                    curEventId = eventsIds.First(e => e.Value == evt.Name).Key;                    
                }
                else
                {
                    eventsIds.Add(eventId, evt.Name);
                    curEventId = eventId;
                    eventId++;                    
                }

                if (!eventsData[evt.Ring.RingId][evt.Auditorium.AuditoriumId].ContainsKey(curEventId))
                {
                    eventsData[evt.Ring.RingId][evt.Auditorium.AuditoriumId].Add(curEventId, new List<AuditoriumEvent>());
                }
                
                eventsData[evt.Ring.RingId][evt.Auditorium.AuditoriumId][curEventId].Add(evt);
            }

            foreach (var ring in eventsData)
            {
                if (!result.ContainsKey(ring.Key))
                {
                    result.Add(ring.Key, new Dictionary<int, List<string>>());
                }

                foreach (var aud in ring.Value)
                {
                    if (!result[ring.Key].ContainsKey(aud.Key))
                    {
                        result[ring.Key].Add(aud.Key, new List<string>());
                    }

                    foreach (var eventPair in aud.Value)
                    {
                        if (eventPair.Value[0].Name.Contains('@'))
                        {
                            var evtName = eventPair.Value[0].Name.Split('@')[0];
                            var evtHint = eventPair.Value[0].Name.Substring(evtName.Length + 1);
                            result[ring.Key][aud.Key].Add(evtName + Environment.NewLine + "( " + GetWeekStringFromEvents(eventPair.Value) + " )@" + evtHint);
                        }
                        else
                        {
                            result[ring.Key][aud.Key].Add(eventPair.Value[0].Name + Environment.NewLine + "( " + GetWeekStringFromEvents(eventPair.Value) + " )");
                        }
                    }
                }
            }

            result = result.OrderBy(r => rings[r.Key].TimeOfDay).ToDictionary(r => r.Key, r => r.Value);
            
            return result;
        }

        // data   - Dictionary<RingId, Dictionary <dow, List<Dictionary<tfd, List<Lesson>>>>>
        // result - Dictionary<RingId, Dictionary <dow, List<tfd/Event-string>>>
        public Dictionary<int, Dictionary<int, List<string>>> getAud(int auditoriumId)
        {
            var data = new Dictionary<int, Dictionary<int, Dictionary<int, List<Lesson>>>>();

            var audLessons = GetFiltredLessons(l => l.Auditorium.AuditoriumId == auditoriumId && l.IsActive)
                .ToList();

            foreach (var lesson in audLessons)
            {
                if (!data.ContainsKey(lesson.Ring.RingId))
                {
                    data.Add(lesson.Ring.RingId, new Dictionary<int, Dictionary<int, List<Lesson>>>());
                }

                if (!data[lesson.Ring.RingId].ContainsKey(Constants.Constants.DOWRemap[(int)lesson.Calendar.Date.DayOfWeek]))
                {
                    data[lesson.Ring.RingId].Add(Constants.Constants.DOWRemap[(int)lesson.Calendar.Date.DayOfWeek], new Dictionary<int, List<Lesson>>());
                }

                if (!data[lesson.Ring.RingId][Constants.Constants.DOWRemap[(int)lesson.Calendar.Date.DayOfWeek]].ContainsKey(lesson.TeacherForDiscipline.TeacherForDisciplineId))
                {
                    data[lesson.Ring.RingId][Constants.Constants.DOWRemap[(int)lesson.Calendar.Date.DayOfWeek]].Add(lesson.TeacherForDiscipline.TeacherForDisciplineId, new List<Lesson>());
                }

                data[lesson.Ring.RingId][Constants.Constants.DOWRemap[(int)lesson.Calendar.Date.DayOfWeek]][lesson.TeacherForDiscipline.TeacherForDisciplineId].Add(lesson);
            }

            var rings = GetAllRings().ToDictionary(r => r.RingId, r => r.Time);

            data = data
                .OrderBy(a => rings[a.Key].TimeOfDay)
                .ToDictionary(a => a.Key, a => a.Value);

            var result = new Dictionary<int, Dictionary<int, List<string>>>();

            foreach (var ring in data)
            {
                result.Add(ring.Key, new Dictionary<int, List<string>>());

                foreach (var dow in ring.Value)
                {
                    result[ring.Key].Add(dow.Key, new List<string>());

                    foreach (var tfd in dow.Value)
                    {
                        result[ring.Key][dow.Key].Add(tfd.Value[0].TeacherForDiscipline.Discipline.StudentGroup.Name + Environment.NewLine +
                            "(" + GetWeekStringFromLessons(tfd.Value) + ")@" +
                            tfd.Value[0].TeacherForDiscipline.Teacher.FIO + "@" + tfd.Value[0].TeacherForDiscipline.Discipline.Name);
                    }
                }
            }

            // Непонятно почему, но следующая строчка без этого не работает
            // Аудитории или даты в event'е получаются == null
            var pick = this.GetAllAuditoriums();
            var pick2 = this.GetAllCalendars();

            var dowEvents = GetFiltredAuditoriumEvents(evt => evt.Auditorium.AuditoriumId == auditoriumId);

            var eventId = 0;
            int curEventId;
            var eventsIds = new Dictionary<int, string>();
            var eventsData = new Dictionary<int, Dictionary<int, Dictionary<int, List<AuditoriumEvent>>>>();

            foreach (var evt in dowEvents)
            {
                if (!eventsData.ContainsKey(evt.Ring.RingId))
                {
                    eventsData.Add(evt.Ring.RingId, new Dictionary<int, Dictionary<int, List<AuditoriumEvent>>>());
                }

                if (!eventsData[evt.Ring.RingId].ContainsKey(Constants.Constants.DOWRemap[(int)evt.Calendar.Date.DayOfWeek]))
                {
                    eventsData[evt.Ring.RingId].Add(Constants.Constants.DOWRemap[(int)evt.Calendar.Date.DayOfWeek], new Dictionary<int, List<AuditoriumEvent>>());
                }

                var eventFound = (eventsIds.Count(e => e.Value == evt.Name) > 0) ? true : false;
                if (eventFound)
                {
                    curEventId = eventsIds.First(e => e.Value == evt.Name).Key;
                }
                else
                {
                    eventsIds.Add(eventId, evt.Name);
                    curEventId = eventId;
                    eventId++;
                }

                if (!eventsData[evt.Ring.RingId][Constants.Constants.DOWRemap[(int)evt.Calendar.Date.DayOfWeek]].ContainsKey(curEventId))
                {
                    eventsData[evt.Ring.RingId][Constants.Constants.DOWRemap[(int)evt.Calendar.Date.DayOfWeek]].Add(curEventId, new List<AuditoriumEvent>());
                }

                eventsData[evt.Ring.RingId][Constants.Constants.DOWRemap[(int)evt.Calendar.Date.DayOfWeek]][curEventId].Add(evt);
            }

            foreach (var ring in eventsData)
            {
                if (!result.ContainsKey(ring.Key))
                {
                    result.Add(ring.Key, new Dictionary<int, List<string>>());
                }

                foreach (var dow in ring.Value)
                {
                    if (!result[ring.Key].ContainsKey(dow.Key))
                    {
                        result[ring.Key].Add(dow.Key, new List<string>());
                    }

                    foreach (var eventPair in dow.Value)
                    {
                        if (eventPair.Value[0].Name.Contains('@'))
                        {
                            var evtName = eventPair.Value[0].Name.Split('@')[0];
                            var evtHint = eventPair.Value[0].Name.Substring(evtName.Length + 1);
                            result[ring.Key][dow.Key].Add(evtName + Environment.NewLine + "( " + GetWeekStringFromEvents(eventPair.Value) + " )@" + evtHint);
                        }
                        else
                        {
                            result[ring.Key][dow.Key].Add(eventPair.Value[0].Name + Environment.NewLine + "( " + GetWeekStringFromEvents(eventPair.Value) + " )");
                        }
                    }
                }
            }

            result = result.OrderBy(r => rings[r.Key].TimeOfDay).ToDictionary(r => r.Key, r => r.Value);

            return result;
        }

        public string GetWeekStringFromLessons(List<Lesson> list)
        {
            var weeksList = new List<int>();

            foreach (var lesson in list)
            {
                weeksList.Add(CalculateWeekNumber(lesson.Calendar.Date));
            }

            string result = CombineWeeks(weeksList);

            return result;
        }

        public string GetWeekStringFromEvents(List<AuditoriumEvent> list)
        {
            var weeksList = new List<int>();

            foreach (var lesson in list)
            {
                weeksList.Add(CalculateWeekNumber(lesson.Calendar.Date));
            }

            string result = CombineWeeks(weeksList);

            return result;
        }

        public int getTFDHours(int tfdId)
        {
            using (var context = new ScheduleContext())
            {
                return context.Lessons.Count(l => l.IsActive && l.TeacherForDiscipline.TeacherForDisciplineId == tfdId) * 2;
            }
        }
        #endregion
    }
}
