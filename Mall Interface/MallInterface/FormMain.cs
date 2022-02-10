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
using System.Configuration;
using System.IO;
using WinSCP;


namespace TransightInterface
{
    public partial class FormMain : Form
    {
        public bool isAutoMode;
        public decimal _SchedTime;

        string DtLibConfig = "DtLibConfig";
        DataView DtView = new DataView();
        public FormMain(bool Initialize)
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
            dtpStartDate.Value = Data.GetLastResetDate();
            dtpStartDate.MaxDate = Data.GetLastResetDate();
            dtpEndDate.Value = Data.GetLastResetDate();
            dtpEndDate.MaxDate = Data.GetLastResetDate();


            isAutoMode = Initialize;
        }
        
        private void FormMain_Load(object sender, EventArgs e)
        {

            this.Show();
            //isAllMode = false;

            if (!(AppConfig._autoTimer == "1" ? true : false))
            {
                isAutoMode = false;
                chkAutomode.Visible = false;
                timer1.Enabled = false;
            }


            string sched = "";
            timer1.Interval = 100;
            if (AppConfig._autoTimer == "1")
            {
                if (AppConfig._schedType == "DAILY")
                {
                    timer1.Interval = 45000; //every 45 seconds
                    sched = AppConfig._schedInterval;

                    if (IsValidTimeFormat(sched))
                    {
                        Func.Log("Daily Scheduler is running..Sched:" + sched);
                        _SchedTime = Convert.ToDecimal(sched.Replace(":", "."));
                    }
                    else
                    {
                        Func.Log("Daily Scheduler is not running..Sched:" + sched);
                        _SchedTime = -1;
                    }

                }
                else if (AppConfig._schedType == "HOURLY")
                {
                    timer1.Interval = 45000; //every 45 seconds
                    sched = AppConfig._schedInterval;

                    if (Func.IsInt32(sched) && (Convert.ToInt32(sched) >= 0 || Convert.ToInt32(sched) <= 59))
                    {
                        Func.Log("Hourly Schedule is running..Sched:" + sched);
                        _SchedTime = Convert.ToInt32(sched);
                    }
                    else
                    {
                        Func.Log("Hourly Schedule is not running..Sched:" + sched);
                        _SchedTime = -1;
                    }
                }
                else
                {
                    if (Func.IsInt32(AppConfig._schedInterval))
                    {
                        timer1.Interval = Convert.ToInt32(AppConfig._schedInterval) * 60000;
                    }
                    else
                    {
                        timer1.Interval = 60000;
                    }
                }
            }

            if (isAutoMode)
            {
                if (chkAutomode.Visible)
                {
                    chkAutomode.Checked = true;
                }
                timer1.Enabled = true;
            }


            EnableControls(!isAutoMode);
            this.Show();

            try
            {
                DataFunctions.LoadDataToGrid(DtLibConfig, dgvPOS, DtView, "select max([filename]) as filename,businessdate, max(date_sent) as LastSent, count(businessdate) as SendCount from mallinterface_Batchlogs group by businessdate");

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured:\r\n\r\n" + ex.Message + "\r\n\r\nInterface will now exit.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Close();
            }
             
        }

        private bool IsValidTimeFormat(string timeString)
        {
            DateTime time = new DateTime();
            return DateTime.TryParseExact(timeString, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out time);
        }
        public string sResult = "";
        private void btnExport_Click(object sender, EventArgs e)
        {


            sResult = "";
            lblStatus.Text = "";
            btnExport.Enabled = false;
            //chkAutomode.Enabled = false;

            EnableControls(false);
            timer1.Enabled = false;
            // Kickoff the worker thread to begin it's DoWork function.
            //bgWorker = new BackgroundWorker(); /* added this line will fix problem */
            //bgWorker.WorkerReportsProgress = true;
            //bgWorker.WorkerSupportsCancellation = true;
            //bgWorker.DoWork += bgWorker_DoWork;
            //bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            //bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            //bgWorker.RunWorkerAsync();



            string sConfig = ConfigurationManager.AppSettings["DBServer"];
            string DBServer = ConfigurationManager.AppSettings["DBName"];

            EnableControls(false);

            try
            {
                //test db conn
                if (!Data.CheckConnection())
                {
                    MessageBox.Show("Fail to open DB connection.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }


                DateTime prevdate = Data.GetPrevDate();
                if (prevdate < Convert.ToDateTime(AppConfig.StartDate))
                {
                    prevdate = Convert.ToDateTime(AppConfig.StartDate);
                    prevdate = prevdate.AddDays(-1);
                }

                //set dates
                int datediff = Convert.ToInt32((dtpStartDate.Value - prevdate).TotalDays);
                datediff = datediff - 1;
                if (datediff > 0)
                    Program.BusinessDateStart = dtpStartDate.Value.AddDays(-datediff);
                else
                    Program.BusinessDateStart = dtpStartDate.Value;


                if (dtpEndDate.Checked)
                    Program.BusinessDateEnd = dtpEndDate.Value;
                else
                    Program.BusinessDateEnd = dtpStartDate.Value;

                if (chkAutomode.Checked)
                {
                    Program.BusinessDateStart = dtpStartDate.Value.AddDays(-1);
                    Program.BusinessDateEnd = Program.BusinessDateStart;
                }






                string sResult = Business.ExportNew(Program.BusinessDateStart, Program.BusinessDateEnd, this);

                if (sResult == string.Empty)
                    MessageBox.Show("Sales file successfully sent to RLC server.", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                else if (sResult == "Export completed.")
                    MessageBox.Show("Sales file successfully sent to RLC server.", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                else if (sResult == "Pending sent successfully.")
                { 
                    MessageBox.Show("Trying to send unsent files…successful", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    MessageBox.Show("Sales file successfully sent to RLC server.", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                }
                else if (sResult == "Export folder not found.")
                    //custom error
                    MessageBox.Show("Export folder not found. Please update config Export folder. " + sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                else if (sResult.Substring(0, 3) != "ERR")
                    MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor. " + sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                else //"ERR" == exception
                    MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

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
                DataFunctions.LoadDataToGrid(DtLibConfig, dgvPOS, DtView, "select max([filename]) as filename,businessdate, max(date_sent) as LastSent, count(businessdate) as SendCount from mallinterface_Batchlogs group by businessdate");

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

        public void SendSFTP(string url, string username, string password,int port,string fingerprint, string filepath, string destinatonpath)
        {
            SessionOptions sessionOptions = new SessionOptions();
            sessionOptions.Protocol = Protocol.Sftp;
            sessionOptions.HostName = url;
            sessionOptions.UserName = username;
            sessionOptions.Password = password;
            sessionOptions.PortNumber = port;
            sessionOptions.SshHostKeyFingerprint = fingerprint;
            Session session = new Session();
            session.Open(sessionOptions);
            TransferOptions transferOptions = new TransferOptions();
            transferOptions.TransferMode = TransferMode.Binary;
            TransferOperationResult transferResult;
            //This is for Getting/Downloading files from SFTP  
            //transferResult = session.GetFiles(filepath, destinatonpath, false, transferOptions);

            //This is for Putting/Uploading file on SFTP  
            transferResult = session.PutFiles(filepath, destinatonpath, false, transferOptions);
            transferResult.Check();

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
            btnResend.Enabled = Enable;
            btnDelete.Enabled = Enable;

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

        private void btnView_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(AppConfig.Sent_Logs))
            {
                System.Diagnostics.Process.Start(AppConfig.Sent_Logs);
            }
            else
            {
                System.IO.Directory.CreateDirectory(AppConfig.Sent_Logs);
                System.Diagnostics.Process.Start(AppConfig.Sent_Logs);
            }
            
        }


 
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            

        }


        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            string s = e.UserState as String;
            SetStatus(s);
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (e.Cancelled)
            {
                SetStatus("Task Cancelled.");
            }
            // Check to see if an error occurred in the background process.
            else if (e.Error != null)
            {
                SetStatus("Error while performing background operation. " + e.Error.Message);
            }
            else
            {
                // Everything completed normally.
                SetStatus("Task Completed...");
                progressBar1.Value = 0;
            }

            //Change the status of the buttons on the UI accordingly
            btnExport.Enabled = true;
            EnableControls(true);
            chkAutomode.Enabled = true;
            if (isAutoMode)
            {
                timer1.Enabled = true;
            }

            if (Program.ZFlag)
            {
                this.Close();
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void chkAutomode_CheckedChanged(object sender, EventArgs e)
        {
            isAutoMode = chkAutomode.Checked;
            EnableControls(!chkAutomode.Checked);
            timer1.Enabled = chkAutomode.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!chkAutomode.Visible && WindowState != FormWindowState.Minimized)
            {
                timer1.Enabled = false;
                return;
            }

            if (_SchedTime >= 0) //Execute daily/hourly scheduled time
            {
                if (AppConfig._schedType == "DAILY")
                {
                    decimal currtime = -1;
                    currtime = Convert.ToDecimal(DateTime.Now.ToString("HH.mm"));
                    if (_SchedTime == currtime)
                    {
                        Program.RunUnsendMode();
                        Program.RunAutoMode();
                        EnableControls(true);
                        DataFunctions.LoadDataToGrid(DtLibConfig, dgvPOS, DtView, "select max([filename]) as filename,businessdate, max(date_sent) as LastSent, count(businessdate) as SendCount from mallinterface_Batchlogs group by businessdate");


                    }
                }
                else if (AppConfig._schedType == "HOURLY")
                {
                    decimal currtime = -1;
                    currtime = Convert.ToDecimal(DateTime.Now.ToString("mm"));
                    if (_SchedTime == currtime)
                    {
                        Program.RunUnsendMode();
                        Program.RunAutoMode();
                        EnableControls(true);
                        DataFunctions.LoadDataToGrid(DtLibConfig, dgvPOS, DtView, "select max([filename]) as filename,businessdate, max(date_sent) as LastSent, count(businessdate) as SendCount from mallinterface_Batchlogs group by businessdate");


                    }
                }
                else
                {
                    if (isAutoMode)
                    {
                        Program.RunUnsendMode();
                        Program.RunAutoMode();
                        EnableControls(true);
                        DataFunctions.LoadDataToGrid(DtLibConfig, dgvPOS, DtView, "select max([filename]) as filename,businessdate, max(date_sent) as LastSent, count(businessdate) as SendCount from mallinterface_Batchlogs group by businessdate");


                    }
                }
            }
            else
            {
                if (isAutoMode)
                {
                    Program.RunAutoMode();
                    Program.RunUnsendMode();
                    EnableControls(true);
                    DataFunctions.LoadDataToGrid(DtLibConfig, dgvPOS, DtView, "select max([filename]) as filename,businessdate, max(date_sent) as LastSent, count(businessdate) as SendCount from mallinterface_Batchlogs group by businessdate");

                }
            }

        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            if ((Program.ZFlag) && (isAutoMode))
            {
                Func.Log("Running EOD Process...");
                btnExport_Click(null, null);
            }

            if (isAutoMode)
            {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void btnResend_Click(object sender, EventArgs e)
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
                DateTime prevdate = Data.GetPrevDate();
                if (prevdate < Convert.ToDateTime(AppConfig.StartDate))
                {
                    prevdate = Convert.ToDateTime(AppConfig.StartDate);
                    prevdate = prevdate.AddDays(-1);
                }

                
                if (dgvPOS.SelectedCells.Count > 0)
                {

                    Program.BusinessDateStart = Convert.ToDateTime(dgvPOS.SelectedRows[0].Cells[1].Value.ToString());
                    Program.BusinessDateEnd = Convert.ToDateTime(dgvPOS.SelectedRows[0].Cells[1].Value.ToString());
                    string sResult = Business.ExportNew(Program.BusinessDateStart, Program.BusinessDateEnd, this);

                    if (sResult == string.Empty)
                        MessageBox.Show("Sales file successfully sent to RLC server.", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    else if (sResult == "Export completed.")
                        MessageBox.Show("Sales file successfully sent to RLC server.", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    else if (sResult == "Pending sent successfully.")
                    {
                        MessageBox.Show("Trying to send unsent files…successful", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                        MessageBox.Show("Sales file successfully sent to RLC server.", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    }
                    else if (sResult == "Export folder not found.")
                        //custom error
                        MessageBox.Show("Export folder not found. Please update config Export folder. " + sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    else if (sResult.Substring(0, 3) != "ERR")
                        MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor. " + sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    else //"ERR" == exception
                        MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    SetStatus(string.Empty);
                }


                
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
                DataFunctions.LoadDataToGrid(DtLibConfig, dgvPOS, DtView, "select max([filename]) as filename,businessdate, max(date_sent) as LastSent, count(businessdate) as SendCount from mallinterface_Batchlogs group by businessdate");

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
               

                if (dgvPOS.SelectedCells.Count > 0)
                {

                    Program.BusinessDateStart = Convert.ToDateTime(dgvPOS.SelectedRows[0].Cells[1].Value.ToString());
                    string filename = dgvPOS.SelectedRows[0].Cells[0].Value.ToString();
                    string sResult = Business.DeleteLogs(Program.BusinessDateStart, filename);

                    if (sResult == string.Empty)
                        MessageBox.Show("Log successfully deleted.", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    else if (sResult.Substring(0, 3) != "ERR") //custom error
                        MessageBox.Show("Error deleting. Please contact your POS vendor. " + sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    else //"ERR" == exception
                        MessageBox.Show("Error connecting to DB. Please contact your POS vendor.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    SetStatus(string.Empty);
                }



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
                DataFunctions.LoadDataToGrid(DtLibConfig, dgvPOS, DtView, "select max([filename]) as filename,businessdate, max(date_sent) as LastSent, count(businessdate) as SendCount from mallinterface_Batchlogs group by businessdate");

            }
        }
       


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void chkAutomode_CheckedChanged_1(object sender, EventArgs e)
        {
            isAutoMode = chkAutomode.Checked;
            
            EnableControls(!chkAutomode.Checked);
            timer1.Enabled = chkAutomode.Checked;
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}