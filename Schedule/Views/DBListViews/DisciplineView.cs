using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Views.DBListViews
{
    class DisciplineView
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public string StudentGroupName { get; set; }
        public string Attestation { get; set; } // 0 - ничего; 1 - зачёт; 2 - экзамен; 3 - зачёт и экзамен
        public int AuditoriumHours { get; set; }
        public int LectureHours { get; set; }
        public int PracticalHours { get; set; }        

        public DisciplineView()
        {

        }

        public DisciplineView(Discipline discipline)
        {
            DisciplineId = discipline.DisciplineId;
            Name = discipline.Name;
            Attestation = Constants.Constants.Attestation[discipline.Attestation];
            AuditoriumHours = discipline.AuditoriumHours;
            LectureHours = discipline.LectureHours;
            PracticalHours = discipline.PracticalHours;
            StudentGroupName = discipline.StudentGroup.Name;
        }

        public static List<DisciplineView> DisciplinesToView(List<Discipline> list)
        {
            var result = new List<DisciplineView>();

            foreach (var disc in list)
            {
                result.Add(new DisciplineView(disc));
            }

            return result;
        }     
    }
}
