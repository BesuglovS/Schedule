namespace Schedule.Forms.DBLists
{
    partial class DisciplineList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.Paste = new System.Windows.Forms.Button();
            this.removeWithTFD = new System.Windows.Forms.Button();
            this.Attestation = new System.Windows.Forms.ComboBox();
            this.Group = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.PracticalHours = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LectureHours = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AuditoriumHours = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.remove = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.DisciplineName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.DiscipineListView = new System.Windows.Forms.DataGridView();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.filterButton = new System.Windows.Forms.Button();
            this.filter = new System.Windows.Forms.TextBox();
            this.controlsPanel.SuspendLayout();
            this.ListPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DiscipineListView)).BeginInit();
            this.filterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.Paste);
            this.controlsPanel.Controls.Add(this.removeWithTFD);
            this.controlsPanel.Controls.Add(this.Attestation);
            this.controlsPanel.Controls.Add(this.Group);
            this.controlsPanel.Controls.Add(this.label6);
            this.controlsPanel.Controls.Add(this.PracticalHours);
            this.controlsPanel.Controls.Add(this.label5);
            this.controlsPanel.Controls.Add(this.LectureHours);
            this.controlsPanel.Controls.Add(this.label4);
            this.controlsPanel.Controls.Add(this.AuditoriumHours);
            this.controlsPanel.Controls.Add(this.label3);
            this.controlsPanel.Controls.Add(this.label2);
            this.controlsPanel.Controls.Add(this.remove);
            this.controlsPanel.Controls.Add(this.update);
            this.controlsPanel.Controls.Add(this.add);
            this.controlsPanel.Controls.Add(this.DisciplineName);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(233, 506);
            this.controlsPanel.TabIndex = 27;
            // 
            // Paste
            // 
            this.Paste.Location = new System.Drawing.Point(209, 25);
            this.Paste.Name = "Paste";
            this.Paste.Size = new System.Drawing.Size(18, 20);
            this.Paste.TabIndex = 1;
            this.Paste.Text = "*";
            this.Paste.UseVisualStyleBackColor = true;
            this.Paste.Click += new System.EventHandler(this.PasteClick);
            // 
            // removeWithTFD
            // 
            this.removeWithTFD.Location = new System.Drawing.Point(9, 334);
            this.removeWithTFD.Name = "removeWithTFD";
            this.removeWithTFD.Size = new System.Drawing.Size(194, 43);
            this.removeWithTFD.TabIndex = 42;
            this.removeWithTFD.Text = "Удалить с назначением преподавателю";
            this.removeWithTFD.UseVisualStyleBackColor = true;
            this.removeWithTFD.Click += new System.EventHandler(this.RemoveWithTFDClick);
            // 
            // Attestation
            // 
            this.Attestation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Attestation.FormattingEnabled = true;
            this.Attestation.Location = new System.Drawing.Point(6, 63);
            this.Attestation.Name = "Attestation";
            this.Attestation.Size = new System.Drawing.Size(197, 21);
            this.Attestation.TabIndex = 1;
            // 
            // Group
            // 
            this.Group.FormattingEnabled = true;
            this.Group.Location = new System.Drawing.Point(7, 220);
            this.Group.Name = "Group";
            this.Group.Size = new System.Drawing.Size(197, 21);
            this.Group.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Группа";
            // 
            // PracticalHours
            // 
            this.PracticalHours.Location = new System.Drawing.Point(6, 181);
            this.PracticalHours.Name = "PracticalHours";
            this.PracticalHours.Size = new System.Drawing.Size(198, 20);
            this.PracticalHours.TabIndex = 4;
            this.PracticalHours.Enter += new System.EventHandler(this.HoursEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Семинары/Практические часы";
            // 
            // LectureHours
            // 
            this.LectureHours.Location = new System.Drawing.Point(6, 142);
            this.LectureHours.Name = "LectureHours";
            this.LectureHours.Size = new System.Drawing.Size(198, 20);
            this.LectureHours.TabIndex = 3;
            this.LectureHours.Enter += new System.EventHandler(this.HoursEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Лекционные часы";
            // 
            // AuditoriumHours
            // 
            this.AuditoriumHours.Location = new System.Drawing.Point(6, 103);
            this.AuditoriumHours.Name = "AuditoriumHours";
            this.AuditoriumHours.Size = new System.Drawing.Size(198, 20);
            this.AuditoriumHours.TabIndex = 2;
            this.AuditoriumHours.Enter += new System.EventHandler(this.HoursEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Аудиторные часы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Форма отчётности";
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(8, 305);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 31;
            this.remove.Text = "Удалить";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.RemoveClick);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(8, 276);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 30;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.UpdateClick);
            // 
            // add
            // 
            this.add.Location = new System.Drawing.Point(7, 247);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(75, 23);
            this.add.TabIndex = 6;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.AddClick);
            // 
            // DisciplineName
            // 
            this.DisciplineName.Location = new System.Drawing.Point(6, 25);
            this.DisciplineName.Name = "DisciplineName";
            this.DisciplineName.Size = new System.Drawing.Size(197, 20);
            this.DisciplineName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Наименование дисциплины";
            // 
            // ListPanel
            // 
            this.ListPanel.Controls.Add(this.viewPanel);
            this.ListPanel.Controls.Add(this.filterPanel);
            this.ListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListPanel.Location = new System.Drawing.Point(233, 0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Size = new System.Drawing.Size(769, 506);
            this.ListPanel.TabIndex = 28;
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.DiscipineListView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 55);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(769, 451);
            this.viewPanel.TabIndex = 2;
            // 
            // DiscipineListView
            // 
            this.DiscipineListView.AllowUserToAddRows = false;
            this.DiscipineListView.AllowUserToDeleteRows = false;
            this.DiscipineListView.AllowUserToResizeColumns = false;
            this.DiscipineListView.AllowUserToResizeRows = false;
            this.DiscipineListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DiscipineListView.ColumnHeadersVisible = false;
            this.DiscipineListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiscipineListView.Location = new System.Drawing.Point(0, 0);
            this.DiscipineListView.Name = "DiscipineListView";
            this.DiscipineListView.ReadOnly = true;
            this.DiscipineListView.RowHeadersVisible = false;
            this.DiscipineListView.Size = new System.Drawing.Size(769, 451);
            this.DiscipineListView.TabIndex = 0;
            this.DiscipineListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DiscipineListViewCellClick);
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.filterButton);
            this.filterPanel.Controls.Add(this.filter);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(769, 55);
            this.filterPanel.TabIndex = 1;
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(620, 13);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(135, 23);
            this.filterButton.TabIndex = 1;
            this.filterButton.Text = "Отфильтровать";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.FilterButtonClick);
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(12, 15);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(602, 20);
            this.filter.TabIndex = 0;
            // 
            // DisciplineList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 506);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "DisciplineList";
            this.Text = "Дисциплины";
            this.Load += new System.EventHandler(this.DisciplineListLoad);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.ListPanel.ResumeLayout(false);
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DiscipineListView)).EndInit();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button removeWithTFD;
        private System.Windows.Forms.ComboBox Attestation;
        private System.Windows.Forms.ComboBox Group;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox PracticalHours;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox LectureHours;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AuditoriumHours;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.TextBox DisciplineName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.DataGridView DiscipineListView;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.TextBox filter;
        private System.Windows.Forms.Button Paste;
    }
}