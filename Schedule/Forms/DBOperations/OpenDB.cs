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
    public partial class OpenDB : Form
    {
        MainForm mainForm;

        public OpenDB(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openDatabase_Click(object sender, EventArgs e)
        {
            if (sqlExpressDB.Checked)
            {
                var connectionString = "data source=tcp:127.0.0.1,1433; Database=" + SQLExpressDatabaseName.Text + ";User ID = sa;Password = ghjuhfvvf";

                mainForm._repo.ConnectionString = connectionString;

                Close();
            }
        }

        private void SQLExpressDatabaseName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                openDatabase.PerformClick();
            }
        }
    }
}
