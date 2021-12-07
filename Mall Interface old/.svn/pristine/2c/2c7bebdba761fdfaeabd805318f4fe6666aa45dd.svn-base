using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Transight.Interface.Common;
using Transight.Interface.Config;

namespace TransightInterface
{
    public partial class FormMain : Form
    {
        public FormMain(bool Initialize)
        {
            if (Initialize)
            {
                InitializeComponent();

                //set title
                try
                {
                    object[] assemblyAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(false);

                    for (int i = 0; i < assemblyAttributes.Length; i++)
                    {
                        if (assemblyAttributes[i].GetType() == typeof(AssemblyTitleAttribute))
                        {
                            this.Text = ((AssemblyTitleAttribute)assemblyAttributes[i]).Title;
                            break;
                        }
                    }
                }
                catch
                {
                    this.Text = string.Empty;
                } 

                //set version
                lblVersion.Text = "V " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

                //set controls
                dtpStartDate.Value = DateTime.Today;
                dtpStartDate.MaxDate = DateTime.Today;
                dtpEndDate.Value = DateTime.Today;
                dtpEndDate.MaxDate = DateTime.Today;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            EnableControls(false);
            this.Show();

            try
            {
                //dtpStartDate.Value = new DateTime(2009, 9, 9);
                //currently nothing to check
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured:\r\n\r\n" + ex.Message + "\r\n\r\nInterface will now exit.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Close();
            }
            finally
            {
                EnableControls(true);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            EnableControls(false);

            try
            {
                //test db conn
                if (!Data.CheckConnection())
                {
                    MessageBox.Show("Fail to open DB connection.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                //set dates
                Program.BusinessDateStart = dtpStartDate.Value;

                if (dtpEndDate.Checked)
                    Program.BusinessDateEnd = dtpEndDate.Value;
                else
                    Program.BusinessDateEnd = Program.BusinessDateStart;

                string sResult = Business.Export(Program.BusinessDateStart, Program.BusinessDateEnd, this);

                if (sResult == string.Empty)
                    MessageBox.Show("Export completed.", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                else if (sResult.Substring(0, 3) != "ERR") //custom error
                    MessageBox.Show(sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                else //"ERR" == exception
                    MessageBox.Show("An error has occured during export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                SetStatus(string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured:\r\n\r\n" + ex.Message + "\r\n\r\nInterface will now exit.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Func.Log("An error has occured during export. Export aborted.");
                ErrorTracking.Log("[FormMain/btnExport_Click] Error during export.");
                ErrorTracking.Log(ex);
                this.Close();
            }
            finally
            {
                EnableControls(true);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            FormConfig formConfig = new FormConfig();
            if (formConfig.ShowDialog() == DialogResult.Yes)
            {
                //set conn string
                if (!Program.SetConnectionString())
                {
                    MessageBox.Show("Error setting connection string.\r\n\r\nInterface will now exit.", "Set Conn String", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    Func.Log("Error getting connection string.");
                    this.Close();
                    return;
                }

                //set global config
                if (!Program.SetGlobalConfigs())
                {
                    MessageBox.Show("Error setting new config(s).\r\n\r\nInterface will now exit.", "Set New Config(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    Func.Log("Error setting new config(s).");
                    this.Close();
                    return;
                }
            }
            formConfig.Dispose();
            formConfig = null;
        }

        public void SetStatus(string s)
        {
            lblStatus.Text = s;
            lblStatus.Refresh();
            this.Refresh();
            Application.DoEvents();
        }

        private void EnableControls(bool Enable)
        {
            btnConfig.Enabled = Enable;
            dtpStartDate.Enabled = Enable;
            dtpEndDate.Enabled = Enable;
            btnExport.Enabled = Enable;
            btnClose.Enabled = Enable;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close Transight Interface.", "Close Interface", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                this.Close();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndDate.Checked)
            {
                if (dtpEndDate.Value < dtpStartDate.Value)
                    dtpEndDate.Value = dtpStartDate.Value;
                dtpEndDate.MinDate = dtpStartDate.Value;
            }
        }

        private void dtpEndDate_MouseDown(object sender, MouseEventArgs e)
        {
            CheckEndDate();
        }

        private void dtpEndDate_KeyDown(object sender, KeyEventArgs e)
        {
            CheckEndDate();
        }

        private void CheckEndDate()
        {
            if (dtpEndDate.Checked)
            {
                if (dtpEndDate.Value < dtpStartDate.Value)
                    dtpEndDate.Value = dtpStartDate.Value;
                dtpEndDate.MinDate = dtpStartDate.Value;
            }
        }
    }
}