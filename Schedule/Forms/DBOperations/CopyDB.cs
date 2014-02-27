using Schedule.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule.Forms.DBOperations
{
    public partial class CopyDB : Form
    {
        MainForm mainForm;

        public CopyDB(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
        }

        private void makeACopy_Click(object sender, EventArgs e)
        {
            if (sqlExpressDB.Checked)
            {
                var dbName = SQLExpressDatabaseName.Text;
                var connectionString = "data source=tcp:127.0.0.1,1433; Database=" + dbName + 
                    ";User ID = " + Schedule.Properties.Settings.Default.DBUserName + 
                    ";Password = " + Schedule.Properties.Settings.Default.DBPassword;

                var newRepo = new ScheduleRepository(connectionString);                
                newRepo.CreateDB();

                newRepo.cloneDB(mainForm._repo);

                Close();
            }
        }
    }
}
