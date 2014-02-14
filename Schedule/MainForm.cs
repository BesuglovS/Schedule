using Schedule.DomainClasses.Main;
using Schedule.Forms.DBLists;
using Schedule.Forms.DBLists.Lessons;
using Schedule.Repositories;
using Schedule.TxtImport;
using Schedule.Views.DBListViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Schedule.wnu;
using Schedule.wnu.MySQLViews;
using System.Globalization;
using Schedule.Core;
using System.IO;
using Application = Microsoft.Office.Interop.Word.Application;
using Schedule.Forms;
using Schedule.Forms.DBOperations;

namespace Schedule
{
    public partial class MainForm : Form
    {
        public ScheduleRepository _repo = new ScheduleRepository();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            LoadLists();
        }

        private void LoadLists()
        {
            var groups = _repo
                .GetAllStudentGroups()
                .OrderBy(g => g.Name)
                .ToList();

            groupList.ValueMember = "StudentGroupId";
            groupList.DisplayMember = "Name";
            groupList.DataSource = groups;            

            var faculties = _repo
                .GetAllFaculties()
                .OrderBy(f => f.SortingOrder)
                .ToList();

            FacultyList.DisplayMember = "Letter";
            FacultyList.ValueMember = "FacultyId";
            FacultyList.DataSource = faculties;

            DOWList.Items.Clear();
            foreach (var dow in Constants.Constants.DOWLocal.Values)
            {
                DOWList.Items.Add(dow);
            }
            DOWList.SelectedIndex = 0;
        }

        private void ShowGroupLessonsClick(object sender, EventArgs e)
        {
            var sStarts = _repo.GetSemesterStarts();

            var groupLessons = _repo.GetGroupedGroupLessons((int)groupList.SelectedValue, sStarts);
                        
            List<GroupTableView> groupEvents = CreateGroupTableView(groupLessons);
            
            ScheduleView.DataSource = groupEvents;

            ScheduleView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            UpdateViewWidth();
        }

        private void UpdateViewWidth()
        {
            if (ScheduleView.DataSource == null)
            {
                return;
            }

            for (int i = 1; i <= 7; i++)
            {
                ScheduleView.Columns[i].HeaderText = Constants.Constants.DOWLocal[i];
                if (i < 7)
                {
                    ScheduleView.Columns[i].Width = (ScheduleView.Width - ScheduleView.Columns[0].Width - 20) / 6;
                }

            }
        }

        private List<GroupTableView> CreateGroupTableView(Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>> groupLessons)
        {
            var result = new List<GroupTableView>();

            var groupView = CreateGroupView(groupLessons);
            foreach (var gv in groupView)
            {
                var time = gv.Datetime.Substring(2, gv.Datetime.Length - 2);

                switch (gv.Datetime.Substring(0,1))
                {
                    case "1":
                        var gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, MonEvents = gv.Events });
                        }                            
                        else
                        {
                            result[gtv].MonEvents = gv.Events;
                        }
                        break;
                    case "2":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, TueEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].TueEvents = gv.Events;
                        }
                        break;
                    case "3":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, WenEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].WenEvents = gv.Events;
                        }
                        break;
                    case "4":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, ThuEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].ThuEvents = gv.Events;
                        }
                        break;
                    case "5":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, FriEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].FriEvents = gv.Events;
                        }
                        break;
                    case "6":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, SatEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].SatEvents = gv.Events;
                        }
                        break;
                    case "7":
                        gtv = result.FindIndex(grtv => grtv.Time == time);
                        if (gtv == -1)
                        {
                            result.Add(new GroupTableView { Time = time, SunEvents = gv.Events });
                        }
                        else
                        {
                            result[gtv].SunEvents = gv.Events;
                        }
                        break;
                }
            }

            result = result.OrderBy(ge => DateTime.ParseExact(ge.Time, "H:mm", CultureInfo.InvariantCulture)).ToList();

            return result;
        }

        private IEnumerable<GroupView> CreateGroupView(Dictionary<string, Dictionary<int, Tuple<string, List<Lesson>>>> data)
        {
            var result = new List<GroupView>();

            foreach (var dt in data)
            {
                var eventString = "";

                for (int i = 0; i < dt.Value.Count; i++)
                {
                    var item = dt.Value.ElementAt(i);
                    var tfd = item.Value.Item2[0].TeacherForDiscipline;

                    eventString += tfd.Discipline.Name + Environment.NewLine;
                    eventString += tfd.Teacher.FIO + Environment.NewLine;
                    eventString += "(" + item.Value.Item1 + ")" + Environment.NewLine;

                    var audWeekList = item.Value.Item2.ToDictionary(l => _repo.CalculateWeekNumber(l.Calendar.Date), l => l.Auditorium.Name);
                    var grouped = audWeekList.GroupBy(a => a.Value);

                    var enumerable = grouped as List<IGrouping<string, KeyValuePair<int, string>>> ?? grouped.ToList();
                    var gcount = enumerable.Count();
                    if (gcount == 1)
                    {
                        eventString += enumerable.ElementAt(0).Key;
                    }
                    else
                    {
                        for (int j = 0; j < gcount; j++)
                        {
                            var jItem = enumerable.ElementAt(j);
                            eventString += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

                            if (j != gcount - 1)
                            {
                                eventString += Environment.NewLine;
                            }
                        }
                    }

                    if (i != dt.Value.Count - 1)
                    {
                        eventString += Environment.NewLine;
                    }

                }

                result.Add(new GroupView { Datetime = dt.Key, Events = eventString });
            }

            return result;
        }
        
        private void BigRedButtonClick(object sender, EventArgs e)
        {            
            // Oops            
            var ll = _repo.GetFiltredLessons(l => l.Ring == null);
            var eprst = 999;
        }

        private List<Lesson> SchoolAudLessons()
        {
            var aSchool = _repo.GetFiltredAuditoriums(a => a.Name.Contains("ШКОЛА"))[0];
            var ll = _repo.GetFiltredLessons(l => l.Auditorium.AuditoriumId == aSchool.AuditoriumId && l.Calendar.Date > DateTime.Now && l.IsActive);
            return ll;
        }

        private void LogDoubledLessons(string filename)
        {
            StreamWriter sw = new StreamWriter(filename);
            sw.Close();
            var students = _repo.GetFiltredStudents(s => !s.Expelled);
            foreach (var student in students)
            {
                var studentGroupIds = _repo
                    .GetFiltredStudentsInGroups(sig => sig.Student.StudentId == student.StudentId)
                    .Select(sig => sig.StudentGroup.StudentGroupId);

                var studentLessons = _repo.GetFiltredLessons(l => l.IsActive && studentGroupIds.Contains(l.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId));

                var grouped = studentLessons
                    .GroupBy(l => l.Calendar.CalendarId + " " + l.Ring.RingId)
                    .Where(g => g.Count() > 1)
                    .ToList();

                foreach (var group in grouped)
                {
                    var gg = group.ToList();
                    foreach (var lesson in gg)
                    {
                        sw = new StreamWriter(filename, true);
                        sw.Write(lesson.Calendar.Date.ToShortDateString() + "\t" + lesson.Ring.Time.ToString("H:mm") + "\t");
                        sw.Write(student.F + " " + student.I + " " + student.O + "\t");
                        sw.Write(lesson.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t");
                        sw.Write(lesson.TeacherForDiscipline.Discipline.Name + "\t");
                        sw.Write(lesson.TeacherForDiscipline.Teacher.FIO + "\t");
                        sw.Write(lesson.Auditorium.Name);
                        sw.WriteLine();                        
                        sw.Close();
                    }
                }
            }            
        }

        private void LogDayLessons(string filename)
        {
            var sw = new StreamWriter(filename);
            var date = new DateTime(2013, 11, 18);
            var ll = _repo.GetFiltredLessons(l => l.IsActive && l.Calendar.Date == date);
            foreach (var l in ll)
            {
                sw.WriteLine(l.Ring.Time.ToString("H:mm") + "\t" +
                    l.TeacherForDiscipline.Discipline.Name + "\t" +
                    l.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t" +
                    l.TeacherForDiscipline.Teacher.FIO + "\t" +
                    l.Auditorium.Name);
            }
            sw.Close();
        }

        private void AuditoriumCollisions()
        {

            var activeLessons = _repo.GetAllActiveLessons();

            var sw = new StreamWriter("kaput.txt");
            foreach (var i in activeLessons)
            {
                foreach (var j in activeLessons)
                {
                    if ((i.Calendar.CalendarId == j.Calendar.CalendarId) && (i.Ring.RingId == j.Ring.RingId) && (i.Auditorium.AuditoriumId == j.Auditorium.AuditoriumId) &&
                        (i.LessonId != j.LessonId))
                    {
                        if ((i.TeacherForDiscipline.Teacher.FIO == "Хенкин Валерий Анатольевич") && ((j.TeacherForDiscipline.Teacher.FIO == "Хенкин Валерий Анатольевич")))
                        {
                            break;
                        }
                        sw.WriteLine(
                            i.Calendar.Date.Date + "\t" + i.Ring.Time.TimeOfDay + "\t" +
                            i.Auditorium.Name + "\t" +
                            i.LessonId + "\t" +
                            i.TeacherForDiscipline.Discipline.Name + "\t" + i.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t" + i.TeacherForDiscipline.Teacher.FIO + "\t" +
                            j.LessonId + "\t" +
                            j.TeacherForDiscipline.Discipline.Name + "\t" + j.TeacherForDiscipline.Discipline.StudentGroup.Name + "\t" + j.TeacherForDiscipline.Teacher.FIO);
                    }
                }
            }
            sw.Close();
        }

        private void АудиторииToolStripMenuItemClick(object sender, EventArgs e)
        {
            var audForm = new AuditoriumList(_repo);
            audForm.ShowDialog();
        }
       
        private void ДниСеместраToolStripMenuItemClick(object sender, EventArgs e)
        {
            var calendarForm = new CalendarList(_repo);
            calendarForm.ShowDialog();
        }

        private void ЗвонкиToolStripMenuItemClick(object sender, EventArgs e)
        {
            var ringForm = new RingList(_repo);
            ringForm.ShowDialog();
        }

        private void СтудентыToolStripMenuItemClick(object sender, EventArgs e)
        {
            var studentForm = new StudentList(_repo);
            studentForm.ShowDialog();
        }

        private void ГруппыToolStripMenuItemClick(object sender, EventArgs e)
        {
            var studentGroupForm = new StudentGroupList(_repo);
            studentGroupForm.ShowDialog();
        }

        private void ПреподавателиToolStripMenuItemClick(object sender, EventArgs e)
        {
            var teacherForm = new TeacherList(_repo);
            teacherForm.ShowDialog();
        }

        private void ДисциплиныToolStripMenuItemClick(object sender, EventArgs e)
        {
            var disciplineForm = new DisciplineList(_repo);
            disciplineForm.ShowDialog();
        }

        private void ImportFromTextClick(object sender, EventArgs e)
        {
            _repo.RecreateDB();
            _repo.Dispose();

            _repo = new ScheduleRepository();

            const string basePath = @"D:\BS\csprogs\Schedule\Schedule.TxtImport\bin\Debug\Import\old\";
            //const string basePath = @"E:\csprogs\Schedule\Schedule.TxtImport\bin\Debug\Import\old\";

            var auds = ScheduleTxtImport.ImportAuditoriums(basePath);
            _repo.AddAuditoriumRange(auds);

            var studentGroups = ScheduleTxtImport.ImportStudentsWithBaseGroups(basePath);
            foreach (var group in studentGroups)
            {
                var groupToAdd = _repo.FindStudentGroup(group.Key);
                if (groupToAdd == null)
                {
                    groupToAdd = new StudentGroup { Name = group.Key };
                    _repo.AddStudentGroup(groupToAdd);
                }

                foreach (var student in group.Value)
                {
                    _repo.AddStudent(student);

                    _repo.AddStudentsInGroups(new StudentsInGroups { Student = student, StudentGroup = groupToAdd });
                }
            }

            var disciplines = ScheduleTxtImport.ImportDisciplines(basePath);
            foreach (var disc in disciplines)
            {
                var group = _repo.FindStudentGroup(disc.StudentGroup.Name);

                if (group == null)
                {
                    group = new StudentGroup { Name = disc.StudentGroup.Name };
                    _repo.AddStudentGroup(group);
                }

                disc.StudentGroup = group;
            }
            _repo.AddDisciplineRange(disciplines);

            var rings = ScheduleTxtImport.ImportRings(basePath);
            _repo.AddRingRange(rings);

            /*
            var teachers = ScheduleTxtImport.ImportTeacherList();
            _repo.AddTeacherRange(teachers);
             */
            
        }

        private void Button1Click(object sender, EventArgs e)
        {
            var addLessonForm = new AddLesson(_repo);
            addLessonForm.ShowDialog();
        }

        private void LoadToSiteClick(object sender, EventArgs e)
        {
            var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var auditoriums = _repo.GetAllAuditoriums();
            var wud = new WnuUploadData { tableSelector = "auditoriums", data = jsonSerializer.Serialize(auditoriums) };
            string json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);
            

            var calendars = _repo.GetAllCalendars();
            var mySqlCalendars = MySQLCalendar.FromCalendarList(calendars);
            wud = new WnuUploadData { tableSelector = "calendars", data = jsonSerializer.Serialize(mySqlCalendars) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var rings = _repo.GetAllRings();
            var mySqlRings = MySQLRing.FromRingList(rings);
            wud = new WnuUploadData { tableSelector = "rings", data = jsonSerializer.Serialize(mySqlRings) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var students = _repo.GetAllStudents();
            var mySqlStudents = MySQLStudent.FromStudentList(students);
            wud = new WnuUploadData { tableSelector = "students", data = jsonSerializer.Serialize(mySqlStudents) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var studentGroups = _repo.GetAllStudentGroups();
            wud = new WnuUploadData { tableSelector = "studentGroups", data = jsonSerializer.Serialize(studentGroups) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var teachers = _repo.GetAllTeachers();
            wud = new WnuUploadData { tableSelector = "teachers", data = jsonSerializer.Serialize(teachers) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var disciplines = _repo.GetAllDisciplines();
            var mySqlDisciplines = MySQLDiscipline.FromDisciplineList(disciplines);
            wud = new WnuUploadData { tableSelector = "disciplines", data = jsonSerializer.Serialize(mySqlDisciplines) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var studentsInGroups = _repo.GetAllStudentsInGroups();
            var mySqlStudentsInGroups = MySQLStudentsInGroups.FromStudentsInGroupsList(studentsInGroups);
            wud = new WnuUploadData { tableSelector = "studentsInGroups", data = jsonSerializer.Serialize(mySqlStudentsInGroups) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var teacherForDisciplines = _repo.GetAllTeacherForDiscipline();
            var mySqlTeacherForDisciplines = MySQLTeacherForDiscipline.FromTeacherForDisciplineList(teacherForDisciplines);
            wud = new WnuUploadData { tableSelector = "teacherForDisciplines", data = jsonSerializer.Serialize(mySqlTeacherForDisciplines) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var lessons = _repo.GetAllLessons();
            var mySqlLessons = MySQLLesson.FromLessonList(lessons);
            wud = new WnuUploadData { tableSelector = "lessons", data = jsonSerializer.Serialize(mySqlLessons) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var configs = _repo.GetAllConfigOptions();
            wud = new WnuUploadData { tableSelector = "configs", data = jsonSerializer.Serialize(configs) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var lessonsLog = _repo.GetAllLessonLogEvents();
            var mySqlLogEvent = MySQLLessonLogEvent.FromLessonLogList(lessonsLog);
            wud = new WnuUploadData { tableSelector = "lessonLogEvents", data = jsonSerializer.Serialize(mySqlLogEvent) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);
            
            var auditoriumEvents = _repo.GetAllAuditoriumEvents();
            var mySqlauditoriumEvents = MySQLAuditoriumEvent.FromAuditoriumEventList(auditoriumEvents);
            wud = new WnuUploadData { tableSelector = "auditoriumEvents", data = jsonSerializer.Serialize(mySqlauditoriumEvents) };
            json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);
        }

        private void RemovelessonClick(object sender, EventArgs e)
        {
            var removeLessonForm = new RemoveLesson(_repo);
            removeLessonForm.ShowDialog();
        }

        private void MainViewCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var source = (List<GroupTableView>)ScheduleView.DataSource;
            var time = source[e.RowIndex].Time;

            var editLessonForm = new EditLesson(_repo, (int)groupList.SelectedValue, e.ColumnIndex, time);
            editLessonForm.ShowDialog();
        }

        private void ОпцииToolStripMenuItemClick(object sender, EventArgs e)
        {
            var configOptionsForm = new ConfigOptionsList(_repo);
            configOptionsForm.ShowDialog();
        }

        private void NotEnoughClick(object sender, EventArgs e)
        {
            var stat = new Dictionary<int, int>();
            var result = new Dictionary<string, int>();
            var resByTeacher = new Dictionary<string, int>();

            foreach (var disc in _repo.GetAllDisciplines())
            {
                var disc1 = disc;
                var disctfd = _repo.GetFiltredTeacherForDiscipline(tefd => tefd.Discipline.DisciplineId == disc1.DisciplineId).FirstOrDefault();
                if (disctfd == null)
                {
                    continue;
                }
                var tfd = disctfd;

                var tfdLessons = _repo.GetFiltredLessons(l => l.IsActive && l.TeacherForDiscipline.TeacherForDisciplineId == tfd.TeacherForDisciplineId).ToList();

                var hoursDiff = disc.AuditoriumHours - tfdLessons.Count * 2;

                result.Add(tfd.TeacherForDisciplineId + "\t" + tfd.Discipline.StudentGroup.Name +"\t" + disc.Name + "\t" + tfd.Teacher.FIO, hoursDiff);

                if (!resByTeacher.ContainsKey(tfd.Teacher.FIO))
                {
                    resByTeacher.Add(tfd.Teacher.FIO, 0);
                }

                resByTeacher[tfd.Teacher.FIO] += hoursDiff;

                if (!stat.ContainsKey(hoursDiff))
                {
                    stat.Add(hoursDiff, 0);
                }
                stat[hoursDiff]++;
            }

            var sr = new StreamWriter("stat.txt");
            foreach (var kvp in stat.OrderByDescending(kvp => kvp.Key))
            {
                sr.WriteLine(kvp.Key + " - " + kvp.Value);
            }
            sr.Close();

            var sr2 = new StreamWriter("stat2.txt");
            foreach (var kvp in result.Where(r => r.Value != 0).OrderByDescending(r => r.Value))
            {
                sr2.WriteLine(kvp.Key + "\t" + kvp.Value);
            }
            sr2.Close();

            var sr3 = new StreamWriter("statByTeacher.txt");
            foreach (var kvp in resByTeacher.Where(r => r.Value != 0).OrderByDescending(r => r.Value))
            {
                sr3.WriteLine(kvp.Key + "\t" + kvp.Value);
            }
            sr3.Close();
        }

        private void AuditoriumKaputClick(object sender, EventArgs e)
        {
            AuditoriumCollisions();
        }

        private void ФакультетыгруппыToolStripMenuItemClick(object sender, EventArgs e)
        {
            var facultyListForm = new FacultyList(_repo);
            facultyListForm.ShowDialog();
        }

        private void WordItClick(object sender, EventArgs e)
        {
            var facultyId = (int) FacultyList.SelectedValue;
            var facultyName = _repo.GetFaculty(facultyId).Name;
            var ruDOW = DOWList.SelectedIndex + 1;

            var facultyDOWLessons = _repo.GetFacultyDOWSchedule(facultyId, ruDOW);
            WordExport.ExportSchedulePage(facultyDOWLessons, facultyName, "Export.docx", DOWList.Text, _repo, false, false, false);

            var eprst = 999;
        }

        private void Print_Click(object sender, EventArgs e)
        {
            var facultyId = (int)FacultyList.SelectedValue;
            var facultyName = _repo.GetFaculty(facultyId).Name;
            var ruDOW = DOWList.SelectedIndex + 1;

            var facultyDOWLessons = _repo.GetFacultyDOWSchedule(facultyId, ruDOW);
            WordExport.ExportSchedulePage(facultyDOWLessons, facultyName, "Export.docx", DOWList.Text, _repo, false, true, true);
        }

        private void ExportWholeWord_Click(object sender, EventArgs e)
        {
            WordExport.ExportWholeSchedule(_repo, "Расписание.docx", false, false);
        }

        private void ActiveLessonsCount_Click(object sender, EventArgs e)
        {
            var allDiscLessonCount = _repo.GetAllDisciplines().Select(d => d.AuditoriumHours).Sum() / 2;
            var activeLessonsCount = _repo.GetAllActiveLessons().Count();
            var diff = allDiscLessonCount - activeLessonsCount;
            MessageBox.Show(activeLessonsCount + " / " + allDiscLessonCount + " => " + diff, "Пар в расписании/плане");
        }

        private void ManyGroups_Click(object sender, EventArgs e)
        {
            var manyGroupsForm = new MultipleView(_repo);
            manyGroupsForm.ShowDialog();
        }

        private void занятостьАудиторийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var audEventsForm = new AuditoriumEventsList(_repo);
            audEventsForm.ShowDialog();
        }

        private void teachersHours_Click(object sender, EventArgs e)
        {
            var teacherHoursForm = new teacherHours(_repo);
            teacherHoursForm.ShowDialog();
        }

        private void oneAuditorium_Click(object sender, EventArgs e)
        {
            var oneAudForm = new OneAuditorium(_repo);
            oneAudForm.ShowDialog();
        }

        private void auditoriums_Click(object sender, EventArgs e)
        {
            var audsForm = new Auditoriums(_repo);
            audsForm.Show();
        }

        private void allChanges_Click(object sender, EventArgs e)
        {
            var allChangesForm = new AllChanges();
            allChangesForm.Show();
        }

        private void scheduleHours_Click(object sender, EventArgs e)
        {
            var result = new Dictionary<DateTime, int>();

            var curDate = new DateTime(2014, 1, 13);

            int lessonsInSchedule = 0;

            do
            {
                int delta = 0;
                var evts = _repo.GetFiltredLessonLogEvents(evt => evt.DateTime.Date == curDate.Date);
                foreach (var ev in evts)
                {
                    if ((ev.OldLesson == null) && (ev.NewLesson != null))
                    {
                        delta++;
                    }
                    if ((ev.OldLesson != null) && (ev.NewLesson == null))
                    {
                        delta--;
                    }
                }
                lessonsInSchedule += delta;

                result.Add(curDate.Date, lessonsInSchedule);

                curDate = curDate.AddDays(1);
            } while (curDate <= DateTime.Now.Date.Date);

            var sw = new StreamWriter("LessonsByDay.txt");
            foreach (var r in result)
            {
                sw.WriteLine(r.Key.ToString("dd.MM.yyyy") + "\t" + r.Value);
            }
            sw.Close();
        }

        private void dayDelta_Click(object sender, EventArgs e)
        {
            // facultyId + DOW
            var result = new List<Tuple<int, DayOfWeek>>();

            var evts = _repo.GetFiltredLessonLogEvents(lle => lle.DateTime.Date == DateTime.Now.Date);

            var fg = _repo.GetAllGroupsInFaculty()
                .GroupBy(gif => gif.Faculty.FacultyId, 
                         gif => gif.StudentGroup.StudentGroupId)
                .ToList();

            foreach (var ev in evts)
            {
                int studentGroupId = -1;
                if (ev.OldLesson != null)
                {
                    studentGroupId = ev.OldLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;

                    var studentIds = _repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroupId)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();

                    var facultyScheduleChanged = new List<int>();

                    foreach (var faculty in fg)
                    {
                        if (_repo.GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId) && faculty.Contains(sig.StudentGroup.StudentGroupId)).Any())
                        {
                            facultyScheduleChanged.Add(faculty.Key);
                        }
                    }

                    foreach (var faculty in facultyScheduleChanged)
                    {
                        var dowFacTuple = Tuple.Create(faculty, ev.OldLesson.Calendar.Date.DayOfWeek);
                        if (!result.Contains(dowFacTuple))
                        {
                            result.Add(dowFacTuple);
                        }
                    }
                }

                

                if (ev.NewLesson != null)
                {
                    studentGroupId = ev.NewLesson.TeacherForDiscipline.Discipline.StudentGroup.StudentGroupId;

                    var studentIds = _repo
                    .GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == studentGroupId)
                    .Select(sig => sig.Student.StudentId)
                    .ToList();

                    var facultyScheduleChanged = new List<int>();

                    foreach (var faculty in fg)
                    {
                        if (_repo.GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId) && faculty.Contains(sig.StudentGroup.StudentGroupId)).Any())
                        {
                            facultyScheduleChanged.Add(faculty.Key);
                        }
                    }

                    foreach (var faculty in facultyScheduleChanged)
                    {
                        var dowFacTuple = Tuple.Create(faculty, ev.NewLesson.Calendar.Date.DayOfWeek);
                        if (!result.Contains(dowFacTuple))
                        {
                            result.Add(dowFacTuple);
                        }
                    }
                }                
            }

            var messageString = "";

            foreach (var dowFac in result.OrderBy(df => df.Item1).ThenBy(df => df.Item2))
            {
                messageString += _repo.GetFaculty(dowFac.Item1).Letter + " - " + Constants.Constants.DOWLocal[Constants.Constants.DOWRemap[(int)dowFac.Item2]] + Environment.NewLine;
            }

            MessageBox.Show(messageString, "Изменения на сегодня");
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            UpdateViewWidth();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                UpdateViewWidth();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openDBForm = new OpenDB(this);
            openDBForm.ShowDialog();

            LoadLists();
        }

        private void CreateNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newDBForm = new CreateDB(this);
            newDBForm.ShowDialog();

            LoadLists();
        }

        private void MakeACopyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
