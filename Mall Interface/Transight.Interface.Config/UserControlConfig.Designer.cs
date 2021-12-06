namespace Transight.Interface.Config
{
    partial class UserControlConfig
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtBox = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lbl2 = new System.Windows.Forms.Label();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.nudNumeric = new System.Windows.Forms.NumericUpDown();
            this.chkBox = new System.Windows.Forms.CheckBox();
            this.cmbBox = new System.Windows.Forms.ComboBox();
            this.rdButton1 = new System.Windows.Forms.RadioButton();
            this.rdButton2 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(32, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            this.lblTitle.Visible = false;
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(125, 0);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(200, 20);
            this.txtBox.TabIndex = 1;
            this.txtBox.Visible = false;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(3, 3);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(39, 13);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "Label1";
            this.lbl1.Visible = false;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(331, 0);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 22);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.TabStop = false;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Visible = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(331, 5);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(39, 13);
            this.lbl2.TabIndex = 7;
            this.lbl2.Text = "Label2";
            this.lbl2.Visible = false;
            // 
            // dtPicker
            // 
            this.dtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPicker.Location = new System.Drawing.Point(125, 0);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(150, 20);
            this.dtPicker.TabIndex = 8;
            this.dtPicker.Visible = false;
            // 
            // nudNumeric
            // 
            this.nudNumeric.Location = new System.Drawing.Point(125, 0);
            this.nudNumeric.Name = "nudNumeric";
            this.nudNumeric.Size = new System.Drawing.Size(100, 20);
            this.nudNumeric.TabIndex = 9;
            this.nudNumeric.Visible = false;
            // 
            // chkBox
            // 
            this.chkBox.AutoSize = true;
            this.chkBox.Location = new System.Drawing.Point(125, 2);
            this.chkBox.Name = "chkBox";
            this.chkBox.Size = new System.Drawing.Size(15, 14);
            this.chkBox.TabIndex = 10;
            this.chkBox.UseVisualStyleBackColor = true;
            this.chkBox.Visible = false;
            // 
            // cmbBox
            // 
            this.cmbBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox.FormattingEnabled = true;
            this.cmbBox.Location = new System.Drawing.Point(125, 0);
            this.cmbBox.Name = "cmbBox";
            this.cmbBox.Size = new System.Drawing.Size(125, 21);
            this.cmbBox.TabIndex = 11;
            this.cmbBox.Visible = false;
            // 
            // rdButton1
            // 
            this.rdButton1.Location = new System.Drawing.Point(125, 1);
            this.rdButton1.Name = "rdButton1";
            this.rdButton1.Size = new System.Drawing.Size(125, 18);
            this.rdButton1.TabIndex = 12;
            this.rdButton1.TabStop = true;
            this.rdButton1.Text = "Radio Button1";
            this.rdButton1.UseVisualStyleBackColor = true;
            this.rdButton1.Visible = false;
            // 
            // rdButton2
            // 
            this.rdButton2.Location = new System.Drawing.Point(256, 1);
            this.rdButton2.Name = "rdButton2";
            this.rdButton2.Size = new System.Drawing.Size(125, 18);
            this.rdButton2.TabIndex = 13;
            this.rdButton2.TabStop = true;
            this.rdButton2.Text = "Radio Button2";
            this.rdButton2.UseVisualStyleBackColor = true;
            this.rdButton2.Visible = false;
            // 
            // UserControlConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.nudNumeric);
            this.Controls.Add(this.chkBox);
            this.Controls.Add(this.cmbBox);
            this.Controls.Add(this.rdButton2);
            this.Controls.Add(this.rdButton1);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.dtPicker);
            this.Name = "UserControlConfig";
            this.Size = new System.Drawing.Size(430, 25);
            ((System.ComponentModel.ISupportInitialize)(this.nudNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private System.Windows.Forms.NumericUpDown nudNumeric;
        private System.Windows.Forms.CheckBox chkBox;
        private System.Windows.Forms.ComboBox cmbBox;
        private System.Windows.Forms.RadioButton rdButton1;
        private System.Windows.Forms.RadioButton rdButton2;
    }
}
