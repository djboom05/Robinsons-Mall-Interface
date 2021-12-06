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
            this.btnCancel.Location = new System.Drawing.Point(587, 26);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 100;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(466, 26);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 35);
            this.btnSave.TabIndex = 99;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlConfigs
            // 
            this.pnlConfigs.AutoScroll = true;
            this.pnlConfigs.Location = new System.Drawing.Point(14, 358);
            this.pnlConfigs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlConfigs.Name = "pnlConfigs";
            this.pnlConfigs.Size = new System.Drawing.Size(684, 15);
            this.pnlConfigs.TabIndex = 40;
            // 
            // txtTransightDBName
            // 
            this.txtTransightDBName.Location = new System.Drawing.Point(195, 129);
            this.txtTransightDBName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTransightDBName.MaxLength = 20;
            this.txtTransightDBName.Name = "txtTransightDBName";
            this.txtTransightDBName.Size = new System.Drawing.Size(148, 26);
            this.txtTransightDBName.TabIndex = 3;
            // 
            // lblTDBN
            // 
            this.lblTDBN.AutoSize = true;
            this.lblTDBN.Location = new System.Drawing.Point(18, 134);
            this.lblTDBN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTDBN.Name = "lblTDBN";
            this.lblTDBN.Size = new System.Drawing.Size(152, 20);
            this.lblTDBN.TabIndex = 46;
            this.lblTDBN.Text = "Transight DB Name:";
            // 
            // txtTransightDBServer
            // 
            this.txtTransightDBServer.Location = new System.Drawing.Point(195, 89);
            this.txtTransightDBServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTransightDBServer.MaxLength = 20;
            this.txtTransightDBServer.Name = "txtTransightDBServer";
            this.txtTransightDBServer.Size = new System.Drawing.Size(148, 26);
            this.txtTransightDBServer.TabIndex = 2;
            // 
            // lblTDBS
            // 
            this.lblTDBS.AutoSize = true;
            this.lblTDBS.Location = new System.Drawing.Point(18, 94);
            this.lblTDBS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTDBS.Name = "lblTDBS";
            this.lblTDBS.Size = new System.Drawing.Size(156, 20);
            this.lblTDBS.TabIndex = 44;
            this.lblTDBS.Text = "Transight DB Server:";
            // 
            // rdBtnConnectionString
            // 
            this.rdBtnConnectionString.AutoSize = true;
            this.rdBtnConnectionString.Location = new System.Drawing.Point(126, 51);
            this.rdBtnConnectionString.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdBtnConnectionString.Name = "rdBtnConnectionString";
            this.rdBtnConnectionString.Size = new System.Drawing.Size(161, 24);
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
            this.lblTIS.Location = new System.Drawing.Point(18, 14);
            this.lblTIS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTIS.Name = "lblTIS";
            this.lblTIS.Size = new System.Drawing.Size(243, 20);
            this.lblTIS.TabIndex = 42;
            this.lblTIS.Text = "Transight Interface Settings";
            // 
            // rdBtnRegistry
            // 
            this.rdBtnRegistry.AutoSize = true;
            this.rdBtnRegistry.Location = new System.Drawing.Point(22, 51);
            this.rdBtnRegistry.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdBtnRegistry.Name = "rdBtnRegistry";
            this.rdBtnRegistry.Size = new System.Drawing.Size(92, 24);
            this.rdBtnRegistry.TabIndex = 0;
            this.rdBtnRegistry.TabStop = true;
            this.rdBtnRegistry.Text = "Registry";
            this.rdBtnRegistry.UseVisualStyleBackColor = true;
            this.rdBtnRegistry.CheckedChanged += new System.EventHandler(this.rdBtnRegistry_CheckedChanged);
            // 
            // btnBrowseLogFolder
            // 
            this.btnBrowseLogFolder.Location = new System.Drawing.Point(504, 166);
            this.btnBrowseLogFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBrowseLogFolder.Name = "btnBrowseLogFolder";
            this.btnBrowseLogFolder.Size = new System.Drawing.Size(38, 35);
            this.btnBrowseLogFolder.TabIndex = 5;
            this.btnBrowseLogFolder.TabStop = false;
            this.btnBrowseLogFolder.Text = "...";
            this.btnBrowseLogFolder.UseVisualStyleBackColor = true;
            this.btnBrowseLogFolder.Click += new System.EventHandler(this.btnBrowseLogFolder_Click);
            // 
            // lblD1
            // 
            this.lblD1.AutoSize = true;
            this.lblD1.Location = new System.Drawing.Point(279, 214);
            this.lblD1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblD1.Name = "lblD1";
            this.lblD1.Size = new System.Drawing.Size(42, 20);
            this.lblD1.TabIndex = 52;
            this.lblD1.Text = "days";
            // 
            // nudLogCleanUp
            // 
            this.nudLogCleanUp.Location = new System.Drawing.Point(196, 211);
            this.nudLogCleanUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.nudLogCleanUp.Size = new System.Drawing.Size(75, 26);
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
            this.lblLCU.Location = new System.Drawing.Point(18, 214);
            this.lblLCU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLCU.Name = "lblLCU";
            this.lblLCU.Size = new System.Drawing.Size(110, 20);
            this.lblLCU.TabIndex = 50;
            this.lblLCU.Text = "Log Clean Up:";
            // 
            // txtLogPath
            // 
            this.txtLogPath.Location = new System.Drawing.Point(195, 169);
            this.txtLogPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLogPath.MaxLength = 1000;
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.ReadOnly = true;
            this.txtLogPath.Size = new System.Drawing.Size(298, 26);
            this.txtLogPath.TabIndex = 4;
            // 
            // lblLP
            // 
            this.lblLP.AutoSize = true;
            this.lblLP.Location = new System.Drawing.Point(18, 174);
            this.lblLP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLP.Name = "lblLP";
            this.lblLP.Size = new System.Drawing.Size(77, 20);
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
            this.lblD2.Location = new System.Drawing.Point(278, 255);
            this.lblD2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblD2.Name = "lblD2";
            this.lblD2.Size = new System.Drawing.Size(42, 20);
            this.lblD2.TabIndex = 55;
            this.lblD2.Text = "days";
            // 
            // nudBusinessDateOffset
            // 
            this.nudBusinessDateOffset.Location = new System.Drawing.Point(195, 251);
            this.nudBusinessDateOffset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.nudBusinessDateOffset.Size = new System.Drawing.Size(75, 26);
            this.nudBusinessDateOffset.TabIndex = 7;
            // 
            // lblBDO
            // 
            this.lblBDO.AutoSize = true;
            this.lblBDO.Location = new System.Drawing.Point(16, 255);
            this.lblBDO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBDO.Name = "lblBDO";
            this.lblBDO.Size = new System.Drawing.Size(165, 20);
            this.lblBDO.TabIndex = 56;
            this.lblBDO.Text = "Business Date Offset:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(351, 217);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 20);
            this.label1.TabIndex = 101;
            this.label1.Text = "Reading start date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(503, 214);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(142, 26);
            this.dtpStartDate.TabIndex = 102;
            // 
            // txtPC
            // 
            this.txtPC.Location = new System.Drawing.Point(503, 247);
            this.txtPC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPC.MaxLength = 20;
            this.txtPC.Name = "txtPC";
            this.txtPC.Size = new System.Drawing.Size(148, 26);
            this.txtPC.TabIndex = 103;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(426, 250);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 104;
            this.label2.Text = "PC Seq:";
            // 
            // txtDBPassword
            // 
            this.txtDBPassword.Location = new System.Drawing.Point(548, 128);
            this.txtDBPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDBPassword.MaxLength = 20;
            this.txtDBPassword.Name = "txtDBPassword";
            this.txtDBPassword.PasswordChar = '*';
            this.txtDBPassword.Size = new System.Drawing.Size(150, 26);
            this.txtDBPassword.TabIndex = 106;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(374, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 20);
            this.label3.TabIndex = 108;
            this.label3.Text = "Transight DB Password:";
            // 
            // txtDBUserName
            // 
            this.txtDBUserName.Location = new System.Drawing.Point(548, 88);
            this.txtDBUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDBUserName.MaxLength = 20;
            this.txtDBUserName.Name = "txtDBUserName";
            this.txtDBUserName.Size = new System.Drawing.Size(150, 26);
            this.txtDBUserName.TabIndex = 105;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(367, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 20);
            this.label4.TabIndex = 107;
            this.label4.Text = "Transight DB UserName:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(465, 25);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 99;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(586, 25);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 100;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkTimerEnabled
            // 
            this.chkTimerEnabled.AutoSize = true;
            this.chkTimerEnabled.Location = new System.Drawing.Point(36, 287);
            this.chkTimerEnabled.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkTimerEnabled.Name = "chkTimerEnabled";
            this.chkTimerEnabled.Size = new System.Drawing.Size(213, 24);
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
            this.grpSchedule.Location = new System.Drawing.Point(22, 295);
            this.grpSchedule.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpSchedule.Size = new System.Drawing.Size(669, 71);
            this.grpSchedule.TabIndex = 125;
            this.grpSchedule.TabStop = false;
            this.grpSchedule.Visible = false;
            // 
            // txtSchedule
            // 
            this.txtSchedule.Location = new System.Drawing.Point(481, 28);
            this.txtSchedule.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSchedule.MaxLength = 20;
            this.txtSchedule.Name = "txtSchedule";
            this.txtSchedule.Size = new System.Drawing.Size(115, 26);
            this.txtSchedule.TabIndex = 122;
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.Location = new System.Drawing.Point(353, 33);
            this.lblSchedule.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(118, 20);
            this.lblSchedule.TabIndex = 118;
            this.lblSchedule.Text = "Time Schedule:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 34);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 20);
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
            this.cmbSchedule.Location = new System.Drawing.Point(138, 28);
            this.cmbSchedule.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbSchedule.Name = "cmbSchedule";
            this.cmbSchedule.Size = new System.Drawing.Size(115, 28);
            this.cmbSchedule.TabIndex = 120;
            this.cmbSchedule.SelectedIndexChanged += new System.EventHandler(this.cmbSchedule_SelectedIndexChanged);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 376);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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