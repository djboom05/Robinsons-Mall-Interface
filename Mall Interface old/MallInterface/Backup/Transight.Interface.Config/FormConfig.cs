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
                txtLogPath.Text = AppConfig.LogPath;

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
            if (txtLogPath.Text != AppConfig.LogPath) return true;
            if (nudLogCleanUp.Value != AppConfig.LogCleanUp) return true;
            if (nudBusinessDateOffset.Value != AppConfig.BusinessDateOffset) return true;

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
            AppConfig.SaveAll(rdBtnRegistry.Checked, txtTransightDBServer.Text, txtTransightDBName.Text, txtLogPath.Text, Convert.ToInt32(nudLogCleanUp.Value), Convert.ToInt32(nudBusinessDateOffset.Value), newConfigList);

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
    }
}
