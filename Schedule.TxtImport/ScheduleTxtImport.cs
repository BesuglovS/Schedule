using Schedule.DomainClasses.Main;
using System;
using System.Collections.Generic;
using System.IO;

namespace Schedule.TxtImport
{
    public static class ScheduleTxtImport
    {
        public static List<Auditorium> ImportAuditoriums(string basePath)
        {
            var result = new List<Auditorium>();

            var sr = new StreamReader(basePath + "Auditoriums.txt");
            string line;
            while((line = sr.ReadLine()) != null)
            {
                result.Add(new Auditorium { Name = line });
            }
            sr.Close();

            return result;
        }

        public static List<Discipline> ImportDisciplines(string basePath)
        {
            var result = new List<Discipline>();

            var sr = new StreamReader(basePath + "Disciplines.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var lineParted = line.Split('@');
                var discName = lineParted[0];
                var groupName = lineParted[1];
                var AudHours = int.Parse(lineParted[2]);
                var LecHours = int.Parse(lineParted[3]);
                var PraHours = int.Parse(lineParted[4]);
                
                // 0 - ничего; 1 - зачёт; 2 - экзамен; 3 - зачёт и экзамен
                int Otch = -1;
                string stOtch = lineParted[5];
                if (stOtch == "-") Otch = 0;
                if (stOtch == "З") Otch = 1;
                if (stOtch == "Э") Otch = 2;
                if (stOtch == "З+Э") Otch = 3;

                result.Add(new Discipline
                {
                    Attestation = Otch,
                    AuditoriumHours = AudHours,
                    LectureHours = LecHours,
                    Name = discName,
                    PracticalHours = PraHours,
                    StudentGroup = new StudentGroup { Name = groupName }
                });
            }
            sr.Close();

            return result;
        }

        public static List<Ring> ImportRings(string basePath)
        {
            var result = new List<Ring>();

            var sr = new StreamReader(basePath + "Rings.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var hour = int.Parse(line.Split(':')[0]);
                var min  = int.Parse(line.Split(':')[1]);
                
                result.Add(new Ring { Time = DateTime.Now.Date.Add(new TimeSpan(hour, min, 0)) });
            }
            sr.Close();

            return result;
        }

        public static List<Teacher> ImportTeacherList(string basePath = "")
        {
            var result = new List<Teacher>();

            StreamReader sr;
            if (basePath == "")
            {
                sr = new StreamReader(@"D:\BS\Расписание\phonebook.txt");
            }
            else
            {
                sr = new StreamReader(basePath + "teachers.txt");
            }
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var fio = line.Split('*')[0];
                var phone = line.Split('*')[1];
                result.Add(new Teacher { FIO = fio, Phone = phone });
            }
            sr.Close();
            return result;
        }

        public static Dictionary<string, List<Student>> ImportStudentsWithBaseGroups(string basePath)
        {
            var result = new Dictionary<string, List<Student>>();

            int state = 0; // 0 - new group; 1 - people
            string currentGroup = "";
            var sr = new StreamReader(basePath + "StudentGroups.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                {
                    state = 0;
                    continue;
                }

                switch (state)
                {
                    case 0:
                        currentGroup = line;
                        result.Add(line, new List<Student>());

                        state = 1;
                        break;
                    case 1:                        
                        var data = line.Split('@');
                        var newStudent = new Student { 
                            F = data[0], 
                            I = data[1],
                            O = data[2],
                            ZachNumber = data[3],
                            BirthDate = (data[4] == "") ? new DateTime(1980, 1, 1) : new DateTime(int.Parse(data[4].Substring(6, 4)), int.Parse(data[4].Substring(3,2)), int.Parse(data[4].Substring(0,2))),
                            Address = data[5],
                            Phone = data[6],
                            Orders = data[7],
                            Starosta = (data[8] == "1") ? true : false,
                            NFactor = (data[9] == "1") ? true : false,
                            PaidEdu = (data[10] == "1") ? true : false,
                            Expelled = (data[11] == "1") ? true : false
                        };

                        result[currentGroup].Add(newStudent);
                        break;
                    default:
                        break;
                }

               
            }
            sr.Close();

            return result;
        }
    }
}
