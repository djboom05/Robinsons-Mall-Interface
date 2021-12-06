using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transight.Interface.Common;

namespace Transight.Interface.Config
{
    public partial class FormConfig : Form
    {
        public FormConfig(int BusinessDateOffsetMax)
            : this()
        {
            nudBusinessDateOffset.Maximum = BusinessDateOffsetMax;
        }

        public FormConfig()
        {
            InitializeComponent();

            //change form size & move controls according to number of other configs
            int pixels = (AppConfig.ConfigList.Count * 26) + (AppConfig.ConfigTitleCount * 26);
            if (pixels > 400) pixels = 400;

            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(this.Size.Width, this.Size.Height + pixels);
            
            pnlConfigs.Height += pixels;
            btnSave.Location = new System.Drawing.Point(btnSave.Location.X, btnSave.Location.Y + pixels);
            btnCancel.Location = new System.Drawing.Point(btnCancel.Location.X, btnCancel.Location.Y + pixels);
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            try
            {
                //assign value
                if (AppConfig.DBOption == TransightDBOption.Registry)
                    rdBtnRegistry.Checked = true;
                else
                    rdBtnConnectionString.Checked = true;

                txtTransightDBServer.Text = AppConfig.DBServer;
                txtTransightDBName.Text = AppConfig.DBName;
                txtDBUserName.Text = AppConfig.DBUserName;
                txtDBPassword.Text = AppConfig.DBPassword;
                txtLogPath.Text = AppConfig.LogPath;

                if (Func.IsDateTime(AppConfig.StartDate, "MM/dd/yyyy"))
                {
                    dtpStartDate.Value = Convert.ToDateTime(AppConfig.StartDate);
                }
                txtPC.Text = AppConfig.PC_Seq.ToString();

                if (AppConfig.LogCleanUp < nudLogCleanUp.Minimum)
                    nudLogCleanUp.Value = nudLogCleanUp.Minimum;
                else if (AppConfig.LogCleanUp > nudLogCleanUp.Maximum)
                    nudLogCleanUp.Value = nudLogCleanUp.Maximum;
                else
                    nudLogCleanUp.Value = AppConfig.LogCleanUp;

                if (AppConfig.BusinessDateOffset < nudBusinessDateOffset.Minimum)
                    nudBusinessDateOffset.Value = nudBusinessDateOffset.Minimum;
                else if (AppConfig.BusinessDateOffset > nudBusinessDateOffset.Maximum)
                    nudBusinessDateOffset.Value = nudBusinessDateOffset.Maximum;
                else
                    nudBusinessDateOffset.Value = AppConfig.BusinessDateOffset;

                chkTimerEnabled.Checked = AppConfig._autoTimer == "1" ? true : false;
                cmbSchedule.Text = AppConfig._schedType;
                txtSchedule.Text = AppConfig._schedInterval;
                //load other configs
                int ctrlY = 0;
                UserControlConfig ucConfig;

                foreach (XMLConfig xmlConfig in AppConfig.ConfigList)
                {
                    ucConfig = new UserControlConfig(xmlConfig);

                    if (xmlConfig.CtrlSetting.Type == ControlType.None)
                        ctrlY += 26;

                    ucConfig.Location = new System.Drawing.Point(3, ctrlY);
                    pnlConfigs.Controls.Add(ucConfig);

                    ctrlY += 26;
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[FormConfig/FormConfig_Load] Error loading configs.");
                ErrorTracking.Log(ex);
                this.Close();
            }
        }

        private void btnBrowseLogFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                txtLogPath.Text = folderBrowserDialog.SelectedPath;
        }

        private bool CheckInput()
        {
            if (rdBtnConnectionString.Checked)
            {
                txtTransightDBServer.Text = txtTransightDBServer.Text.Trim();
                if (txtTransightDBServer.Text == string.Empty)
                {
                    MessageBox.Show("Please enter Transight DB server.", "Interface Setting");
                    txtTransightDBServer.Focus();
                    return false;
                }

                txtTransightDBName.Text = txtTransightDBName.Text.Trim();
                if (txtTransightDBName.Text == string.Empty)
                {
                    MessageBox.Show("Please enter Transight DB name.", "Interface Setting");
                    txtTransightDBName.Focus();
                    return false;
                }

                txtDBUserName.Text = txtDBUserName.Text.Trim();
                if (txtDBUserName.Text == string.Empty)
                {
                    MessageBox.Show("Please enter DB User Name.", "Interface Setting");
                    txtDBUserName.Focus();
                    return false;
                }

                txtDBPassword.Text = txtDBPassword.Text.Trim();
                if (txtDBPassword.Text == string.Empty)
                {
                    MessageBox.Show("Please enter DB Password.", "Interface Setting");
                    txtDBPassword.Focus();
                    return false;
                }

                txtPC.Text = txtPC.Text.Trim();
                if (txtPC.Text == string.Empty)
                {
                    MessageBox.Show("Please enter POS PC Sequence.", "Interface Setting");
                    txtPC.Focus();
                    return false;
                }
            }

            if (chkTimerEnabled.Checked)
            {
                if (cmbSchedule.SelectedIndex < 0)
                {
                    MessageBox.Show("Please enter Schedule Type", "Interface Setting");
                    cmbSchedule.DroppedDown = true;
                    return false;
                }
                else if (cmbSchedule.SelectedIndex == 0)
                {
                    if (txtSchedule.Text == "")
                    {
                        MessageBox.Show("Please enter Daily Schedule", "Interface Setting");
                        txtSchedule.Focus();
                        return false;
                    }
                }
                else if (cmbSchedule.SelectedIndex == 1)
                {
                    if (txtSchedule.Text == "")
                    {
                        MessageBox.Show("Please enter Hourly Schedule", "Interface Setting");
                        txtSchedule.Focus();
                        return false;
                    }
                }
                else if (cmbSchedule.SelectedIndex == 2)
                {
                    if (txtSchedule.Text == "")
                    {
                        MessageBox.Show("Please enter Interval Schedule", "Interface Setting");
                        txtSchedule.Focus();
                        return false;
                    }
                }
            }
            else
            {
                cmbSchedule.SelectedIndex = 0;
                txtSchedule.Text = "00:00";
            }

            string errString;

            foreach (UserControlConfig ucConfig in pnlConfigs.Controls)
            { 
                errString = ucConfig.ValidateInput();

                if (errString != string.Empty)
                {
                    MessageBox.Show(errString, "Interface Setting");
                    return false;
                }
            }

            return true;
        }

        private bool CheckChanges()
        {
            if (rdBtnRegistry.Checked && AppConfig.DBOption != TransightDBOption.Registry) return true;
            if (rdBtnConnectionString.Checked && AppConfig.DBOption != TransightDBOption.ConnString) return true;
            if (txtTransightDBServer.Text != AppConfig.DBServer) return true;
            if (txtTransightDBName.Text != AppConfig.DBName) return true;
            if (txtDBUserName.Text != AppConfig.DBUserName) return true;
            if (txtDBPassword.Text != AppConfig.DBPassword) return true;
            if (txtLogPath.Text != AppConfig.LogPath) return true;
            if (nudLogCleanUp.Value != AppConfig.LogCleanUp) return true;
            if (nudBusinessDateOffset.Value != AppConfig.BusinessDateOffset) return true;
            if (dtpStartDate.Value.ToString("MM/dd/yyyy") != AppConfig.StartDate) return true;
            if (txtPC.Text != AppConfig.PC_Seq.ToString()) return true;
            if ((chkTimerEnabled.Checked ? "1" : "0") != AppConfig._autoTimer) return true;
            if (cmbSchedule.Text != AppConfig._schedType) return true;
            if (txtSchedule.Text != AppConfig._schedInterval) return true;

            foreach (UserControlConfig ucConfig in pnlConfigs.Controls)
            {
                if (ucConfig.Changed())
                    return true;
            }

            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckInput()) return;

            if (!CheckChanges())
            {
                MessageBox.Show("No changes made.", "Interface Settings", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Close();
                return;
            }

            //get other configs
            List<XMLConfig> newConfigList = new List<XMLConfig> { };

            foreach (UserControlConfig ucConfig in pnlConfigs.Controls)
            {
                ucConfig.SetChanges();
                newConfigList.Add(ucConfig.GetConfig());
            }

            //save config
            AppConfig.SaveAll(rdBtnRegistry.Checked, txtTransightDBServer.Text, txtTransightDBName.Text, txtDBUserName.Text,
                txtDBPassword.Text,  txtLogPath.Text, Convert.ToInt32(nudLogCleanUp.Value), Convert.ToInt32(nudBusinessDateOffset.Value), dtpStartDate.Value.ToString("MM/dd/yyyy"), Convert.ToInt32(txtPC.Text),
                chkTimerEnabled.Checked ? "1" : "0", cmbSchedule.Text, txtSchedule.Text, newConfigList);

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CheckChanges())
            {
                if (MessageBox.Show("Changes made. Do you want to save the changes?", "Interface Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    btnSave_Click(sender, e);
                else
                    this.Close();
            }
            else
                this.Close();
        }

        private void rdBtnRegistry_CheckedChanged(object sender, EventArgs e)
        {
            DisableTransightDBOption(rdBtnRegistry.Checked);
        }

        private void rdBtnConnectionString_CheckedChanged(object sender, EventArgs e)
        {
            DisableTransightDBOption(rdBtnRegistry.Checked);
        }

        private void DisableTransightDBOption(bool Disable)
        {
            if (Disable)
            {
                txtTransightDBServer.ReadOnly = true;
                txtTransightDBServer.Enabled = false;
                txtTransightDBName.ReadOnly = true;
                txtTransightDBName.Enabled = false;
            }
            else
            {
                txtTransightDBServer.ReadOnly = false;
                txtTransightDBServer.Enabled = true;
                txtTransightDBName.ReadOnly = false;
                txtTransightDBName.Enabled = true;
            }
        }

        private void chkTimerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            grpSchedule.Visible = chkTimerEnabled.Checked;
        }

        private void cmbSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSchedule.Text = "Time Schedule";
            txtSchedule.Text = "";
            if (cmbSchedule.SelectedIndex == 0)
            {
                lblSchedule.Text = "Daily Schedule(hh:mm):";
                txtSchedule.Text = "00:00"; //every 12:00 am in the morning
            }
            else if (cmbSchedule.SelectedIndex == 1)
            {
                lblSchedule.Text = "Hourly Schedule(00-59):";
                txtSchedule.Text = "45"; //every hour in 45 mins
            }
            else if (cmbSchedule.SelectedIndex == 2)
            {
                lblSchedule.Text = "Interval Time(Mins):";
                txtSchedule.Text = "3"; //every 3 mins interval
            }
        }

        
    }
}
