using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Schedule.Views.DBListViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule.Forms.DBLists
{
    public partial class DisciplineList : Form
    {
        private readonly ScheduleRepository _repo;

        public DisciplineList(ScheduleRepository repo)
        {
            InitializeComponent();

            _repo = repo;
        }

        private void DisciplineListLoad(object sender, EventArgs e)
        {
            Attestation.Items.Clear();
            foreach (var attestationForm in Constants.Constants.Attestation)
            {
                Attestation.Items.Add(attestationForm.Value);
            }

            Group.Items.Clear();
            var groups = _repo.GetAllStudentGroups();
            foreach (var gr in groups.OrderBy(g => g.Name))
            {
                Group.Items.Add(gr.Name);
            }

            RefreshView();
        }

        private void RefreshView()
        {
            List<Discipline> discList;

            if (filter.Text == "")
            {
                discList = _repo.GetAllDisciplines();
            }
            else
            {
                discList = _repo.GetFiltredDisciplines(disc => disc.Name.Contains(filter.Text));
            }

            var discView = DisciplineView.DisciplinesToView(discList);

            DiscipineListView.DataSource = discView;

            //DiscipineListView.Columns["DisciplineId"].Visible = false;
            DiscipineListView.Columns["DisciplineId"].Width = 40;
            DiscipineListView.Columns["Name"].Width = 270;
            DiscipineListView.Columns["Attestation"].Width = 80;
            DiscipineListView.Columns["AuditoriumHours"].Width = 80;
            DiscipineListView.Columns["LectureHours"].Width = 80;
            DiscipineListView.Columns["PracticalHours"].Width = 80;
            DiscipineListView.Columns["StudentGroupName"].Width = 120;

            DiscipineListView.ClearSelection();
        }

        private void DiscipineListViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[e.RowIndex];
            var discipline = _repo.GetDiscipline(discView.DisciplineId);

            DisciplineName.Text = discipline.Name;
            Attestation.SelectedIndex = discipline.Attestation;
            AuditoriumHours.Text = discipline.AuditoriumHours.ToString();
            LectureHours.Text = discipline.LectureHours.ToString();
            PracticalHours.Text = discipline.PracticalHours.ToString();
            Group.Text = discipline.StudentGroup.Name;
        }

        private void AddClick(object sender, EventArgs e)
        {
            if (_repo.FindDiscipline(DisciplineName.Text, Attestation.SelectedIndex, int.Parse(AuditoriumHours.Text), 
                int.Parse(LectureHours.Text), int.Parse(PracticalHours.Text), Group.Text) != null)
            {
                MessageBox.Show("Такая дисциплина уже есть.");
                return;
            }

            var disciplineGroup = _repo.FindStudentGroup(Group.Text);
            if (disciplineGroup == null)
            {
                disciplineGroup = new StudentGroup { Name = Group.Text };
                _repo.AddStudentGroup(disciplineGroup);
            }

            var newDiscipline = new Discipline { Attestation = Attestation.SelectedIndex, AuditoriumHours = int.Parse(AuditoriumHours.Text),
                LectureHours = int.Parse(LectureHours.Text), PracticalHours = int.Parse(PracticalHours.Text), Name = DisciplineName.Text,
                StudentGroup = disciplineGroup};

            _repo.AddDiscipline(newDiscipline);

            RefreshView();
        }

        private void UpdateClick(object sender, EventArgs e)
        {
            if (DiscipineListView.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[DiscipineListView.SelectedCells[0].RowIndex];
                var discipline = _repo.GetDiscipline(discView.DisciplineId);

                discipline.Name = DisciplineName.Text;
                discipline.Attestation = Attestation.SelectedIndex;
                discipline.AuditoriumHours = int.Parse(AuditoriumHours.Text);
                discipline.LectureHours = int.Parse(LectureHours.Text);
                discipline.PracticalHours = int.Parse(PracticalHours.Text);

                var disciplineGroup = _repo.FindStudentGroup(Group.Text);
                if (disciplineGroup == null)
                {
                    disciplineGroup = new StudentGroup { Name = Group.Text };
                    _repo.AddStudentGroup(disciplineGroup);
                }
                discipline.StudentGroup = disciplineGroup;

                _repo.UpdateDiscipline(discipline);

                RefreshView();
            }
        }

        private void RemoveClick(object sender, EventArgs e)
        {
            if (DiscipineListView.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[DiscipineListView.SelectedCells[0].RowIndex];

                if (_repo.GetFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discView.DisciplineId).Count > 0)
                {
                    MessageBox.Show("Эта дисциплина определена преподавателю.");
                    return;
                }

                _repo.RemoveDiscipline(discView.DisciplineId);

                RefreshView();
            }
        }

        private void RemoveWithTFDClick(object sender, EventArgs e)
        {
            if (DiscipineListView.SelectedCells.Count > 0)
            {
                var discView = ((List<DisciplineView>)DiscipineListView.DataSource)[DiscipineListView.SelectedCells[0].RowIndex];

                var disciplineTFDs = _repo.GetFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == discView.DisciplineId);

                foreach (var tfd in disciplineTFDs)
                {
                    _repo.RemoveTeacherForDiscipline(tfd.TeacherForDisciplineId);
                }

                _repo.RemoveDiscipline(discView.DisciplineId);

                RefreshView();
            }
        }

        private void FilterButtonClick(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void PasteClick(object sender, EventArgs e)
        {
            DisciplineName.Text = Clipboard.GetText();
        }

        private void HoursEnter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
