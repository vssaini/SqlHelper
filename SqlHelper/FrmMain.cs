using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SqlHelper.Code;
using SqlHelper.Properties;

namespace SqlHelper
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            string errorMsg;
            var conString = txtConnectionString.Text;
            lblStatus.ForeColor = Color.Black;

            if (SqlDataManager.IsConStringValid(conString, out errorMsg))
            {
                btnCreateDatabase.Enabled = false;
                bgWorkerDbCreator.RunWorkerAsync(conString);
            }
            else
            {
                var eMsg = string.Format("Invalid connection string. Reason - {0}", errorMsg);
                MessageBox.Show(eMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bgWorkerDbCreator_DoWork(object sender, DoWorkEventArgs e)
        {
            CreateDatabase((string)e.Argument);
        }

        private void bgWorkerDbCreator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStatus.Text = (string)e.UserState;
        }

        private void bgWorkerDbCreator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCreateDatabase.Enabled = true;
            if (e.Error == null) return;

            lblStatus.ForeColor = Color.DarkRed;
            lblStatus.Text = "Error occured";

            if (e.Error.Message.Contains("Database already exists"))
            {
                lblStatus.Text = "Error - Database already exists";
            }
            else
            {
                MessageBox.Show("Error occured- \n\n" + e.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Create database on Sql Server as per connection string
        /// </summary>
        /// <param name="conString">End user provided connection string</param>
        private void CreateDatabase(string conString)
        {
            if (conString.ToLower().Contains("sdf")) return;

            bgWorkerDbCreator.ReportProgress(0, "Checking if database already exists...");
            if (!SqlDataManager.IsDatabaseExists(conString))
            {
                SqlDataManager.DataDirectory = Utility.DataDirectory;
                SqlDataManager.CreateDatabase(conString, Resources.DBScript, bgWorkerDbCreator);
            }
            else
            {
                throw new Exception("Database already exists");
            }
        }
    }
}
