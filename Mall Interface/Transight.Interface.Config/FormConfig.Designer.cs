namespace Transight.Interface.Config
{
    partial class FormConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlConfigs = new System.Windows.Forms.Panel();
            this.txtTransightDBName = new System.Windows.Forms.TextBox();
            this.lblTDBN = new System.Windows.Forms.Label();
            this.txtTransightDBServer = new System.Windows.Forms.TextBox();
            this.lblTDBS = new System.Windows.Forms.Label();
            this.rdBtnConnectionString = new System.Windows.Forms.RadioButton();
            this.lblTIS = new System.Windows.Forms.Label();
            this.rdBtnRegistry = new System.Windows.Forms.RadioButton();
            this.btnBrowseLogFolder = new System.Windows.Forms.Button();
            this.lblD1 = new System.Windows.Forms.Label();
            this.nudLogCleanUp = new System.Windows.Forms.NumericUpDown();
            this.lblLCU = new System.Windows.Forms.Label();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.lblLP = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblD2 = new System.Windows.Forms.Label();
            this.nudBusinessDateOffset = new System.Windows.Forms.NumericUpDown();
            this.lblBDO = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtPC = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDBPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDBUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chkTimerEnabled = new System.Windows.Forms.CheckBox();
            this.grpSchedule = new System.Windows.Forms.GroupBox();
            this.txtSchedule = new System.Windows.Forms.TextBox();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSchedule = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogCleanUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBusinessDateOffset)).BeginInit();
            this.grpSchedule.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(391, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 100;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(311, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 99;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlConfigs
            // 
            this.pnlConfigs.AutoScroll = true;
            this.pnlConfigs.Location = new System.Drawing.Point(9, 233);
            this.pnlConfigs.Name = "pnlConfigs";
            this.pnlConfigs.Size = new System.Drawing.Size(456, 10);
            this.pnlConfigs.TabIndex = 40;
            // 
            // txtTransightDBName
            // 
            this.txtTransightDBName.Location = new System.Drawing.Point(130, 84);
            this.txtTransightDBName.MaxLength = 20;
            this.txtTransightDBName.Name = "txtTransightDBName";
            this.txtTransightDBName.Size = new System.Drawing.Size(100, 20);
            this.txtTransightDBName.TabIndex = 3;
            // 
            // lblTDBN
            // 
            this.lblTDBN.AutoSize = true;
            this.lblTDBN.Location = new System.Drawing.Point(12, 87);
            this.lblTDBN.Name = "lblTDBN";
            this.lblTDBN.Size = new System.Drawing.Size(103, 13);
            this.lblTDBN.TabIndex = 46;
            this.lblTDBN.Text = "Transight DB Name:";
            // 
            // txtTransightDBServer
            // 
            this.txtTransightDBServer.Location = new System.Drawing.Point(130, 58);
            this.txtTransightDBServer.MaxLength = 20;
            this.txtTransightDBServer.Name = "txtTransightDBServer";
            this.txtTransightDBServer.Size = new System.Drawing.Size(100, 20);
            this.txtTransightDBServer.TabIndex = 2;
            // 
            // lblTDBS
            // 
            this.lblTDBS.AutoSize = true;
            this.lblTDBS.Location = new System.Drawing.Point(12, 61);
            this.lblTDBS.Name = "lblTDBS";
            this.lblTDBS.Size = new System.Drawing.Size(106, 13);
            this.lblTDBS.TabIndex = 44;
            this.lblTDBS.Text = "Transight DB Server:";
            // 
            // rdBtnConnectionString
            // 
            this.rdBtnConnectionString.AutoSize = true;
            this.rdBtnConnectionString.Location = new System.Drawing.Point(84, 33);
            this.rdBtnConnectionString.Name = "rdBtnConnectionString";
            this.rdBtnConnectionString.Size = new System.Drawing.Size(109, 17);
            this.rdBtnConnectionString.TabIndex = 1;
            this.rdBtnConnectionString.TabStop = true;
            this.rdBtnConnectionString.Text = "Connection String";
            this.rdBtnConnectionString.UseVisualStyleBackColor = true;
            this.rdBtnConnectionString.CheckedChanged += new System.EventHandler(this.rdBtnConnectionString_CheckedChanged);
            // 
            // lblTIS
            // 
            this.lblTIS.AutoSize = true;
            this.lblTIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTIS.Location = new System.Drawing.Point(12, 9);
            this.lblTIS.Name = "lblTIS";
            this.lblTIS.Size = new System.Drawing.Size(165, 13);
            this.lblTIS.TabIndex = 42;
            this.lblTIS.Text = "Transight Interface Settings";
            // 
            // rdBtnRegistry
            // 
            this.rdBtnRegistry.AutoSize = true;
            this.rdBtnRegistry.Location = new System.Drawing.Point(15, 33);
            this.rdBtnRegistry.Name = "rdBtnRegistry";
            this.rdBtnRegistry.Size = new System.Drawing.Size(63, 17);
            this.rdBtnRegistry.TabIndex = 0;
            this.rdBtnRegistry.TabStop = true;
            this.rdBtnRegistry.Text = "Registry";
            this.rdBtnRegistry.UseVisualStyleBackColor = true;
            this.rdBtnRegistry.CheckedChanged += new System.EventHandler(this.rdBtnRegistry_CheckedChanged);
            // 
            // btnBrowseLogFolder
            // 
            this.btnBrowseLogFolder.Location = new System.Drawing.Point(336, 108);
            this.btnBrowseLogFolder.Name = "btnBrowseLogFolder";
            this.btnBrowseLogFolder.Size = new System.Drawing.Size(25, 23);
            this.btnBrowseLogFolder.TabIndex = 5;
            this.btnBrowseLogFolder.TabStop = false;
            this.btnBrowseLogFolder.Text = "...";
            this.btnBrowseLogFolder.UseVisualStyleBackColor = true;
            this.btnBrowseLogFolder.Click += new System.EventHandler(this.btnBrowseLogFolder_Click);
            // 
            // lblD1
            // 
            this.lblD1.AutoSize = true;
            this.lblD1.Location = new System.Drawing.Point(186, 139);
            this.lblD1.Name = "lblD1";
            this.lblD1.Size = new System.Drawing.Size(29, 13);
            this.lblD1.TabIndex = 52;
            this.lblD1.Text = "days";
            // 
            // nudLogCleanUp
            // 
            this.nudLogCleanUp.Location = new System.Drawing.Point(131, 137);
            this.nudLogCleanUp.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudLogCleanUp.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudLogCleanUp.Name = "nudLogCleanUp";
            this.nudLogCleanUp.Size = new System.Drawing.Size(50, 20);
            this.nudLogCleanUp.TabIndex = 6;
            this.nudLogCleanUp.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblLCU
            // 
            this.lblLCU.AutoSize = true;
            this.lblLCU.Location = new System.Drawing.Point(12, 139);
            this.lblLCU.Name = "lblLCU";
            this.lblLCU.Size = new System.Drawing.Size(75, 13);
            this.lblLCU.TabIndex = 50;
            this.lblLCU.Text = "Log Clean Up:";
            // 
            // txtLogPath
            // 
            this.txtLogPath.Location = new System.Drawing.Point(130, 110);
            this.txtLogPath.MaxLength = 1000;
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.ReadOnly = true;
            this.txtLogPath.Size = new System.Drawing.Size(200, 20);
            this.txtLogPath.TabIndex = 4;
            // 
            // lblLP
            // 
            this.lblLP.AutoSize = true;
            this.lblLP.Location = new System.Drawing.Point(12, 113);
            this.lblLP.Name = "lblLP";
            this.lblLP.Size = new System.Drawing.Size(53, 13);
            this.lblLP.TabIndex = 48;
            this.lblLP.Text = "Log Path:";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Interface Log Path";
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // lblD2
            // 
            this.lblD2.AutoSize = true;
            this.lblD2.Location = new System.Drawing.Point(185, 166);
            this.lblD2.Name = "lblD2";
            this.lblD2.Size = new System.Drawing.Size(29, 13);
            this.lblD2.TabIndex = 55;
            this.lblD2.Text = "days";
            // 
            // nudBusinessDateOffset
            // 
            this.nudBusinessDateOffset.Location = new System.Drawing.Point(130, 163);
            this.nudBusinessDateOffset.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBusinessDateOffset.Minimum = new decimal(new int[] {
            120,
            0,
            0,
            -2147483648});
            this.nudBusinessDateOffset.Name = "nudBusinessDateOffset";
            this.nudBusinessDateOffset.Size = new System.Drawing.Size(50, 20);
            this.nudBusinessDateOffset.TabIndex = 7;
            // 
            // lblBDO
            // 
            this.lblBDO.AutoSize = true;
            this.lblBDO.Location = new System.Drawing.Point(11, 166);
            this.lblBDO.Name = "lblBDO";
            this.lblBDO.Size = new System.Drawing.Size(109, 13);
            this.lblBDO.TabIndex = 56;
            this.lblBDO.Text = "Business Date Offset:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 101;
            this.label1.Text = "Reading start date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(335, 139);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(96, 20);
            this.dtpStartDate.TabIndex = 102;
            // 
            // txtPC
            // 
            this.txtPC.Location = new System.Drawing.Point(335, 161);
            this.txtPC.MaxLength = 20;
            this.txtPC.Name = "txtPC";
            this.txtPC.Size = new System.Drawing.Size(100, 20);
            this.txtPC.TabIndex = 103;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 104;
            this.label2.Text = "PC Seq:";
            // 
            // txtDBPassword
            // 
            this.txtDBPassword.Location = new System.Drawing.Point(365, 83);
            this.txtDBPassword.MaxLength = 20;
            this.txtDBPassword.Name = "txtDBPassword";
            this.txtDBPassword.PasswordChar = '*';
            this.txtDBPassword.Size = new System.Drawing.Size(101, 20);
            this.txtDBPassword.TabIndex = 106;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 108;
            this.label3.Text = "Transight DB Password:";
            // 
            // txtDBUserName
            // 
            this.txtDBUserName.Location = new System.Drawing.Point(365, 57);
            this.txtDBUserName.MaxLength = 20;
            this.txtDBUserName.Name = "txtDBUserName";
            this.txtDBUserName.Size = new System.Drawing.Size(101, 20);
            this.txtDBUserName.TabIndex = 105;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 107;
            this.label4.Text = "Transight DB UserName:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(310, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 99;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(391, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 100;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkTimerEnabled
            // 
            this.chkTimerEnabled.AutoSize = true;
            this.chkTimerEnabled.Location = new System.Drawing.Point(24, 187);
            this.chkTimerEnabled.Name = "chkTimerEnabled";
            this.chkTimerEnabled.Size = new System.Drawing.Size(144, 17);
            this.chkTimerEnabled.TabIndex = 124;
            this.chkTimerEnabled.Text = "Automatic Timer Enabled";
            this.chkTimerEnabled.UseVisualStyleBackColor = true;
            this.chkTimerEnabled.CheckedChanged += new System.EventHandler(this.chkTimerEnabled_CheckedChanged);
            // 
            // grpSchedule
            // 
            this.grpSchedule.Controls.Add(this.txtSchedule);
            this.grpSchedule.Controls.Add(this.lblSchedule);
            this.grpSchedule.Controls.Add(this.label5);
            this.grpSchedule.Controls.Add(this.cmbSchedule);
            this.grpSchedule.Location = new System.Drawing.Point(15, 192);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Size = new System.Drawing.Size(446, 46);
            this.grpSchedule.TabIndex = 125;
            this.grpSchedule.TabStop = false;
            this.grpSchedule.Visible = false;
            // 
            // txtSchedule
            // 
            this.txtSchedule.Location = new System.Drawing.Point(321, 18);
            this.txtSchedule.MaxLength = 20;
            this.txtSchedule.Name = "txtSchedule";
            this.txtSchedule.Size = new System.Drawing.Size(78, 20);
            this.txtSchedule.TabIndex = 122;
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.Location = new System.Drawing.Point(235, 21);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(81, 13);
            this.lblSchedule.TabIndex = 118;
            this.lblSchedule.Text = "Time Schedule:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 121;
            this.label5.Text = "Schedule Type:";
            // 
            // cmbSchedule
            // 
            this.cmbSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSchedule.FormattingEnabled = true;
            this.cmbSchedule.Items.AddRange(new object[] {
            "DAILY",
            "HOURLY",
            "INTERVAL"});
            this.cmbSchedule.Location = new System.Drawing.Point(92, 18);
            this.cmbSchedule.Name = "cmbSchedule";
            this.cmbSchedule.Size = new System.Drawing.Size(78, 21);
            this.cmbSchedule.TabIndex = 120;
            this.cmbSchedule.SelectedIndexChanged += new System.EventHandler(this.cmbSchedule_SelectedIndexChanged);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 244);
            this.ControlBox = false;
            this.Controls.Add(this.chkTimerEnabled);
            this.Controls.Add(this.grpSchedule);
            this.Controls.Add(this.txtDBPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDBUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblD2);
            this.Controls.Add(this.nudBusinessDateOffset);
            this.Controls.Add(this.lblBDO);
            this.Controls.Add(this.btnBrowseLogFolder);
            this.Controls.Add(this.lblD1);
            this.Controls.Add(this.nudLogCleanUp);
            this.Controls.Add(this.lblLCU);
            this.Controls.Add(this.txtLogPath);
            this.Controls.Add(this.lblLP);
            this.Controls.Add(this.txtTransightDBName);
            this.Controls.Add(this.lblTDBN);
            this.Controls.Add(this.txtTransightDBServer);
            this.Controls.Add(this.lblTDBS);
            this.Controls.Add(this.rdBtnConnectionString);
            this.Controls.Add(this.lblTIS);
            this.Controls.Add(this.rdBtnRegistry);
            this.Controls.Add(this.pnlConfigs);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transight Interface Settings";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudLogCleanUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBusinessDateOffset)).EndInit();
            this.grpSchedule.ResumeLayout(false);
            this.grpSchedule.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlConfigs;
        private System.Windows.Forms.TextBox txtTransightDBName;
        private System.Windows.Forms.Label lblTDBN;
        private System.Windows.Forms.TextBox txtTransightDBServer;
        private System.Windows.Forms.Label lblTDBS;
        private System.Windows.Forms.RadioButton rdBtnConnectionString;
        private System.Windows.Forms.Label lblTIS;
        private System.Windows.Forms.RadioButton rdBtnRegistry;
        private System.Windows.Forms.Button btnBrowseLogFolder;
        private System.Windows.Forms.Label lblD1;
        private System.Windows.Forms.NumericUpDown nudLogCleanUp;
        private System.Windows.Forms.Label lblLCU;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.Label lblLP;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblD2;
        private System.Windows.Forms.NumericUpDown nudBusinessDateOffset;
        private System.Windows.Forms.Label lblBDO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.TextBox txtPC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDBPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDBUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkTimerEnabled;
        private System.Windows.Forms.GroupBox grpSchedule;
        private System.Windows.Forms.TextBox txtSchedule;
        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSchedule;
    }
}