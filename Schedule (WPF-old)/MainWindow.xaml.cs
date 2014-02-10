using System.Windows;
using Schedule.Repositories;
using System.IO;
using System;
using Schedule.ImportFromDoc;
using System.ComponentModel;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Schedule.Core.MainWindow.Views;
using Schedule.DomainClasses.Main;
using Calendar = Schedule.DomainClasses.Main.Calendar;

namespace Schedule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ScheduleRepository _repo = new ScheduleRepository();
        TTData ttdata;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var groupName = "12 А";

            var gl = _repo.GetGroupedGroupLessons(groupName, Constants.Constants.semesterStarts);

            List<GroupView> groupEvents = CreateGroupView(gl);

            MainView.DataContext = groupEvents;
        }

        private List<GroupView> CreateGroupView(Dictionary<string, Dictionary<int, Tuple<string, List<DomainClasses.Main.Lesson>>>> data)
        {
            var result = new List<GroupView>();

            foreach (var dt in data)
            {
                var eventString = "";

                for (int i = 0; i < dt.Value.Count; i++)
                {
                    var item = dt.Value.ElementAt(i);
                    var tfd = item.Value.Item2[0].TeacherForDiscipline;

                    eventString += tfd.Discipline.Name + "\n";
                    eventString += tfd.Teacher.FIO + "\n";
                    eventString += "(" + item.Value.Item1 + ")\n";

                    var audWeekList = item.Value.Item2.ToDictionary(l => CalculateWeekNumber(DateTime.ParseExact(l.Calendar.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture)), l => l.Auditorium.Name);
                    var grouped = audWeekList.GroupBy(a => a.Value);

                    var gcount = grouped.Count();
                    for (int j = 0; j < gcount; j++)
                    {
                        var jItem = grouped.ElementAt(j);
                        eventString += ScheduleRepository.CombineWeeks(jItem.Select(ag => ag.Key).ToList()) + " - " + jItem.Key;

                        if (j != gcount - 1)
                        {
                            eventString += "\n";
                        }
                    }

                }

                result.Add(new GroupView { Datetime = dt.Key, Events = eventString });
            }

            return result;
        }

        private int CalculateWeekNumber(DateTime dateTime)
        {
            return (dateTime - Constants.Constants.semesterStarts).Days / 7 + 1;
        }

        private void ImportButtonClick(object sender, RoutedEventArgs e)
        {
            var bw = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            bw.DoWork += new DoWorkEventHandler(bw_ImportDocToDB_DoWork);
            bw.RunWorkerCompleted += bw_ImportDocToDB_RunWorkerCompleted;
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ImportDocToDB_ProgressChanged);

            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _repo.RecreateDB();

            // BRB2         
            var l = new Lesson
            {
                Auditorium = new Auditorium { Name = "Ауд. 304" },
                Calendar = new Calendar { Date = "11.02.2013" },
                IsActive = true,
                Ring = new Ring { Time = "08:00 - 09:25" },
                TeacherForDiscipline = new TeacherForDiscipline
                {
                    Teacher = new Teacher { FIO = "Подклетнова Светлана Владимировна" },
                    Discipline = new Discipline { Name = "Дискретная математика", StudentGroup = new StudentGroup { Name = "12 А" } }
                }
            };

            _repo.AddLesson(l);
        }



    }
}
