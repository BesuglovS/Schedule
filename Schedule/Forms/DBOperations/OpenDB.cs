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
        public OpenDB()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openDatabase_Click(object sender, EventArgs e)
        {
            if (sqlExpressDB.Checked)
            {
                var connectionString = "data source=tcp:uch-otd-disp,1433; Database=" + SQLExpressDatabaseName.Text + ";User ID = sa;Password = ghjuhfvvf";

            }
        }

    }
}
