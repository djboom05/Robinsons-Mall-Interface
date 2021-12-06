using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Transight.Interface.Config;
using Transight.Interface.Common;

namespace TransightInterface
{
    static class Program
    {
        //set app path
        private static string appPath = Application.StartupPath;
        private static bool isAutoMode = false;
        private static bool isUnsendMode = false;
        private static string connString;

        public static DateTime BusinessDateStart;
        public static DateTime BusinessDateEnd;

        public static string AppPath
        {
            get { return appPath; }
        }

        public static bool IsAutoMode
        {
            get { return isAutoMode; }
        }

        public static bool IsUnsendMode
        {
            get { return isUnsendMode; }
        }

        public static string ConnString
        {
            get { return connString; }
        }

        //golbal configs
        private static string exportFolder;
        private static string sentFolder;
        private static string storeNo;
        private static string posterminalNo;

        public static string Dialer;
        public static string Username;
        public static string Password;
        private static string fTPIP;
        private static string fTPUserName;
        private static string fTPPassword;

        //New Added
        private static string BranchNo;
        private static string Site;
        private static string Contract;
        private static string Floor;
        private static string StoreNumber;
        private static bool zFlag;

        private static List<int> transTypeNoList;
        public static string POSTerminalNo
        {
            get { return posterminalNo; }
        }

        public static string ExportFolder
        {
            get { return exportFolder; }
        }

        public static string SentFolder
        {
            get { return sentFolder; }
        }

        public static string StoreNo
        {
            get { return storeNo; }
        }

        public static string FTPIP
        {
            get { return fTPIP; }
        }

        public static string FTPUserName
        {
            get { return fTPUserName; }
        }

        public static string FTPPassword
        {
            get { return fTPPassword; }
        }


        public static List<int> TransTypeNoList
        {
            get { return transTypeNoList; }
        }

        public static string getStoreNumber
        {
            get { return StoreNumber; }
        }

        public static string getBranchNo
        {
            get { return BranchNo; }
        }
        public static string getSite
        {
            get { return Site; }
        }

        public static string getContract
        {
            get { return Contract; }
        }

        public static string getFloor
        {
            get { return Floor; }
        }


        public static bool ZFlag
        {
            get { return zFlag; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //check running instance, allow only one at a time
            //if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            //    return;

            //check if auto mode
            

            //else if (args[0] == "X")
            //    isUnsendMode = true;

            //isAutoMode = true;
            //load app config
            //isUnsendMode = true;

            try
            {
                AppConfig.Load(true);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error loading configs.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Application.Exit();
                return;
            }

            //set log path
            Func.LogPath = AppConfig.LogPath;

            //perform clean up logs
            Func.ClearLogs(AppConfig.LogCleanUp);

            //set configs
            try
            {
                Program.SetGlobalConfigs();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error setting configs.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Application.Exit();
                return;
            }


            

            

            if (AppConfig._autoTimer == "1")
            {
                isAutoMode = true;
                Func.Log("Running auto mode.");
            }
            else
            { 
                Func.Log("Running manual mode.");
            }
            //set conn string
            if (!SetConnectionString())
            {
                if (!isAutoMode) System.Windows.Forms.MessageBox.Show("Error getting connection string.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Func.Log("Error getting connection string.");
                Application.Exit();
                return;
            }


            if (args.Length == 1)
            {
                if (args[0] == "A")
                {
                    RunAutoMode();
                    Application.Exit();
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain((AppConfig._autoTimer == "1") ? true : false));
            }




        }

        public static void RunAutoMode()
        {
            if (!Data.CheckConnection())
            {
                Func.Log("Fail to open DB connection.");
                return;
            }

            try
            {
                //set process date
                Program.BusinessDateStart = Data.GetBusinessDate().AddDays(AppConfig.BusinessDateOffset);
                Program.BusinessDateEnd = Program.BusinessDateStart;
                

                string sResult = Business.ExportX(Program.BusinessDateStart, Program.BusinessDateEnd);

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
                else if (sResult == "No record(s) to export.") { }
                //no message
                else if (sResult.Substring(0, 3) != "ERR")
                    MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor. " + sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                else //"ERR" == exception
                    MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                


            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[Program/RunAutoMode] Error during auto export.");
                ErrorTracking.Log(ex);
            }
        }

        public static void RunUnsendMode()
        {
            if (!Data.CheckConnection())
            {
                Func.Log("Fail to open DB connection.");
                return;
            }

            try
            {
                //set process date
               
                string sResult = Business.ExportUnsend();


                if (sResult == string.Empty)
                    MessageBox.Show("Trying to send unsent files…successful.", "Export", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                else if (sResult == "No unsend file")
                    return;
                //custom error
                //MessageBox.Show("FTP sending to server not configured. Please update config. " + sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                else if (sResult == "FTP not activated")
                    //custom error
                    MessageBox.Show("FTP sending to server not configured. Please update config. " + sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                else if (sResult.Substring(0, 3) != "ERR")
                    MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor. " + sResult, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                else //"ERR" == exception
                    MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[Program/RunUnsendMode] Error during auto export.");
                ErrorTracking.Log(ex);
            }
        }

        public static bool SetConnectionString()
        {
            try
            {
                if (AppConfig.DBOption == TransightDBOption.Registry)
                    connString = Func.GetPOSRegistryConnectionString();
                else
                    connString = "Initial Catalog=" + AppConfig.DBName + ";Data Source=" + AppConfig.DBServer + ";user ID=" + AppConfig.DBUserName + ";password=" + AppConfig.DBPassword + ";";

                return true;
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[Program/SetConnectionString] Error getting connection string.");
                ErrorTracking.Log(ex);

                return false;
            }
        }

        public static bool SetGlobalConfigs()
        {
            try
            {
                exportFolder = AppConfig.GetConfig("ExportFolder").ToString();
                sentFolder = AppConfig.GetConfig("SentFolder").ToString();
                storeNo = AppConfig.GetConfig("StoreNo").ToString();

                fTPIP = AppConfig.GetConfig("FTPIP").ToString();
                fTPUserName = AppConfig.GetConfig("FTPUserName").ToString();
                fTPPassword = AppConfig.GetConfig("FTPPassword").ToString();

                //New Added
                BranchNo = AppConfig.GetConfig("BranchNo").ToString();
                Site = AppConfig.GetConfig("Site").ToString();
                Contract = AppConfig.GetConfig("Contract").ToString();
                Floor = AppConfig.GetConfig("Floor").ToString();
                StoreNumber = AppConfig.GetConfig("StoreNumber").ToString();



                transTypeNoList = new List<int> { };
                int i;
                string[] types;

                //get trans type list
                types = AppConfig.GetConfig("TransTypeNo").ToString().Split(new char[] { ',' });

                if (types.Length > 0)
                {
                    for (i = 0; i < types.Length; i++)
                    {
                        if (types[i].Trim() != string.Empty && Func.IsInt32(types[i]))
                            transTypeNoList.Add(Convert.ToInt32(types[i]));
                    }
                }
 

                return true;
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[Program/SetGlobalConfig] Error setting configs.");
                ErrorTracking.Log(ex);

                return false;
            }
        }
    }
}
