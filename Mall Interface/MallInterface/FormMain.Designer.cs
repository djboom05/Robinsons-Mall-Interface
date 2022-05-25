namespace TransightInterface
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnClose = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnConfig = new System.Windows.Forms.Button();
            this.lblBD = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblT = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.dgvPOS = new System.Windows.Forms.DataGridView();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.businessdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_sent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SendCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnResend = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkAutomode = new System.Windows.Forms.CheckBox();
            this.btnTestFTP = new System.Windows.Forms.Button();
            this.btnTestSend = new System.Windows.Forms.Button();
            this.btnLoadSFTP = new System.Windows.Forms.Button();
            this.dgvFTP = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFTP)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(409, 59);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Cl&ose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(301, 17);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(100, 13);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblVersion.Click += new System.EventHandler(this.lblVersion_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(407, 12);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(75, 23);
            this.btnConfig.TabIndex = 2;
            this.btnConfig.TabStop = false;
            this.btnConfig.Text = "Con&fig";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // lblBD
            // 
            this.lblBD.AutoSize = true;
            this.lblBD.Location = new System.Drawing.Point(12, 36);
            this.lblBD.Name = "lblBD";
            this.lblBD.Size = new System.Drawing.Size(78, 13);
            this.lblBD.TabIndex = 12;
            this.lblBD.Text = "Business Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(96, 33);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(105, 20);
            this.dtpStartDate.TabIndex = 2;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblT
            // 
            this.lblT.AutoSize = true;
            this.lblT.Location = new System.Drawing.Point(207, 36);
            this.lblT.Name = "lblT";
            this.lblT.Size = new System.Drawing.Size(16, 13);
            this.lblT.TabIndex = 17;
            this.lblT.Text = "to";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(229, 33);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowCheckBox = true;
            this.dtpEndDate.Size = new System.Drawing.Size(123, 20);
            this.dtpEndDate.TabIndex = 3;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            this.dtpEndDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpEndDate_KeyDown);
            this.dtpEndDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtpEndDate_MouseDown);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(326, 59);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "&Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Location = new System.Drawing.Point(141, 64);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 18;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(12, 708);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(117, 23);
            this.btnView.TabIndex = 19;
            this.btnView.Text = "&View Sent Files Folder";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Visible = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // dgvPOS
            // 
            this.dgvPOS.AllowUserToAddRows = false;
            this.dgvPOS.AllowUserToResizeColumns = false;
            this.dgvPOS.AllowUserToResizeRows = false;
            this.dgvPOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPOS.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filename,
            this.businessdate,
            this.date_sent,
            this.SendCount});
            this.dgvPOS.Location = new System.Drawing.Point(12, 525);
            this.dgvPOS.Name = "dgvPOS";
            this.dgvPOS.ReadOnly = true;
            this.dgvPOS.RowHeadersVisible = false;
            this.dgvPOS.RowHeadersWidth = 51;
            this.dgvPOS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPOS.Size = new System.Drawing.Size(469, 177);
            this.dgvPOS.TabIndex = 115;
            // 
            // filename
            // 
            this.filename.DataPropertyName = "filename";
            this.filename.HeaderText = "File Name";
            this.filename.MinimumWidth = 6;
            this.filename.Name = "filename";
            this.filename.ReadOnly = true;
            this.filename.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.filename.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.filename.Width = 140;
            // 
            // businessdate
            // 
            this.businessdate.DataPropertyName = "businessdate";
            this.businessdate.HeaderText = "Business Date";
            this.businessdate.MinimumWidth = 6;
            this.businessdate.Name = "businessdate";
            this.businessdate.ReadOnly = true;
            this.businessdate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.businessdate.Width = 150;
            // 
            // date_sent
            // 
            this.date_sent.DataPropertyName = "LastSent";
            this.date_sent.HeaderText = "Last sent";
            this.date_sent.MinimumWidth = 6;
            this.date_sent.Name = "date_sent";
            this.date_sent.ReadOnly = true;
            this.date_sent.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.date_sent.Width = 120;
            // 
            // SendCount
            // 
            this.SendCount.DataPropertyName = "SendCount";
            this.SendCount.HeaderText = "Send Count";
            this.SendCount.MinimumWidth = 8;
            this.SendCount.Name = "SendCount";
            this.SendCount.ReadOnly = true;
            this.SendCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SendCount.Width = 55;
            // 
            // btnResend
            // 
            this.btnResend.Location = new System.Drawing.Point(15, 306);
            this.btnResend.Name = "btnResend";
            this.btnResend.Size = new System.Drawing.Size(105, 23);
            this.btnResend.TabIndex = 116;
            this.btnResend.Text = "Re-send text file";
            this.btnResend.UseVisualStyleBackColor = true;
            this.btnResend.Visible = false;
            this.btnResend.Click += new System.EventHandler(this.btnResend_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(379, 306);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(105, 23);
            this.btnDelete.TabIndex = 117;
            this.btnDelete.Text = "Delete logs data";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Mall Interface";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 94);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(469, 23);
            this.progressBar1.TabIndex = 119;
            this.progressBar1.Visible = false;
            // 
            // chkAutomode
            // 
            this.chkAutomode.AutoSize = true;
            this.chkAutomode.Checked = true;
            this.chkAutomode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutomode.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkAutomode.Location = new System.Drawing.Point(19, 11);
            this.chkAutomode.Name = "chkAutomode";
            this.chkAutomode.Size = new System.Drawing.Size(103, 17);
            this.chkAutomode.TabIndex = 118;
            this.chkAutomode.Text = "Automatic Mode";
            this.chkAutomode.UseVisualStyleBackColor = true;
            this.chkAutomode.CheckedChanged += new System.EventHandler(this.chkAutomode_CheckedChanged_1);
            // 
            // btnTestFTP
            // 
            this.btnTestFTP.Location = new System.Drawing.Point(19, 59);
            this.btnTestFTP.Name = "btnTestFTP";
            this.btnTestFTP.Size = new System.Drawing.Size(126, 23);
            this.btnTestFTP.TabIndex = 121;
            this.btnTestFTP.Text = "Test Connection";
            this.btnTestFTP.UseVisualStyleBackColor = true;
            this.btnTestFTP.Visible = false;
            this.btnTestFTP.Click += new System.EventHandler(this.btnTestFTP_Click);
            // 
            // btnTestSend
            // 
            this.btnTestSend.Location = new System.Drawing.Point(165, 59);
            this.btnTestSend.Name = "btnTestSend";
            this.btnTestSend.Size = new System.Drawing.Size(110, 23);
            this.btnTestSend.TabIndex = 122;
            this.btnTestSend.Text = "Test SFTP SEND";
            this.btnTestSend.UseVisualStyleBackColor = true;
            this.btnTestSend.Click += new System.EventHandler(this.btnTestSend_Click);
            // 
            // btnLoadSFTP
            // 
            this.btnLoadSFTP.Location = new System.Drawing.Point(190, 306);
            this.btnLoadSFTP.Name = "btnLoadSFTP";
            this.btnLoadSFTP.Size = new System.Drawing.Size(102, 23);
            this.btnLoadSFTP.TabIndex = 123;
            this.btnLoadSFTP.Text = "Refresh";
            this.btnLoadSFTP.UseVisualStyleBackColor = true;
            this.btnLoadSFTP.Click += new System.EventHandler(this.btnLoadSFTP_Click);
            // 
            // dgvFTP
            // 
            this.dgvFTP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFTP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFTP.Location = new System.Drawing.Point(12, 123);
            this.dgvFTP.Name = "dgvFTP";
            this.dgvFTP.RowHeadersVisible = false;
            this.dgvFTP.Size = new System.Drawing.Size(470, 168);
            this.dgvFTP.TabIndex = 124;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 337);
            this.Controls.Add(this.dgvFTP);
            this.Controls.Add(this.btnLoadSFTP);
            this.Controls.Add(this.btnTestSend);
            this.Controls.Add(this.btnTestFTP);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chkAutomode);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnResend);
            this.Controls.Add(this.dgvPOS);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblT);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblBD);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFTP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Label lblBD;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblT;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnView;
        internal System.Windows.Forms.DataGridView dgvPOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn businessdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_sent;
        private System.Windows.Forms.DataGridViewTextBoxColumn SendCount;
        private System.Windows.Forms.Button btnResend;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        public System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chkAutomode;
        private System.Windows.Forms.Button btnTestFTP;
        private System.Windows.Forms.Button btnTestSend;
        private System.Windows.Forms.Button btnLoadSFTP;
        private System.Windows.Forms.DataGridView dgvFTP;
    }
}

