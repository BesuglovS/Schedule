using Schedule.ImportFromDoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Schedule
{
    public partial class MainWindow
    {
        void bw_ImportDocToDB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Title = ("Database load completed!");
        }

        private void bw_ImportDocToDB_DoWork(object sender, DoWorkEventArgs e)
        {
            string ttCopy = Path.GetDirectoryName(
                System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) +
                "\\Test.doc";
            File.Copy("\\\\uch-otd-disp\\Расписание\\12-13(2)\\Расписание (12-13-2).doc",
                ttCopy, true);

            ttdata = ScheduleDocImport.ReadTT(ttCopy, new DateTime(2013, 2, 4));

            var les = ttdata.Lessons.Where(l => l.Calendar.Date == "16.02.2013" && l.Ring.Time == "14:00 – 15:20" &&
                l.TeacherForDiscipline.Discipline.StudentGroup.Name == "12 А").ToList();

            var dis = ttdata.Disciplines.Where(d => d.Name == "Социология").ToList();


            _repo.RecreateDB();

            BackgroundWorker worker = sender as BackgroundWorker;
            foreach (var l in ttdata.Lessons)
            {
                l.IsActive = true;
            }
            _repo.AddLessonRange(ttdata.Lessons);
        }

        private void bw_ImportDocToDB_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Title = (e.ProgressPercentage.ToString() + "%");
        }
    }
}
