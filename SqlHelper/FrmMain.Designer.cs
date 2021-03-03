namespace SqlHelper
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.RichTextBox();
            this.btnCreateDatabase = new System.Windows.Forms.Button();
            this.bgWorkerDbCreator = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(12, 21);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(106, 15);
            this.lblConnectionString.TabIndex = 1;
            this.lblConnectionString.Text = "Connection String:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(124, 18);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtConnectionString.Size = new System.Drawing.Size(234, 85);
            this.txtConnectionString.TabIndex = 2;
            this.txtConnectionString.Text = "Server=.;Database=PasswordReset;User Id=sa;Password=god";
            // 
            // btnCreateDatabase
            // 
            this.btnCreateDatabase.Image = global::SqlHelper.Properties.Resources.DBCreate;
            this.btnCreateDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateDatabase.Location = new System.Drawing.Point(225, 109);
            this.btnCreateDatabase.Name = "btnCreateDatabase";
            this.btnCreateDatabase.Size = new System.Drawing.Size(133, 24);
            this.btnCreateDatabase.TabIndex = 3;
            this.btnCreateDatabase.Text = "Create Sql Database";
            this.btnCreateDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCreateDatabase.UseVisualStyleBackColor = true;
            this.btnCreateDatabase.Click += new System.EventHandler(this.btnCreateDatabase_Click);
            // 
            // bgWorkerDbCreator
            // 
            this.bgWorkerDbCreator.WorkerReportsProgress = true;
            this.bgWorkerDbCreator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerDbCreator_DoWork);
            this.bgWorkerDbCreator.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerDbCreator_ProgressChanged);
            this.bgWorkerDbCreator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerDbCreator_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 153);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(370, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(26, 17);
            this.lblStatus.Text = "Idle";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 175);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCreateDatabase);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.lblConnectionString);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Sql Database";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.RichTextBox txtConnectionString;
        private System.Windows.Forms.Button btnCreateDatabase;
        private System.ComponentModel.BackgroundWorker bgWorkerDbCreator;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}

