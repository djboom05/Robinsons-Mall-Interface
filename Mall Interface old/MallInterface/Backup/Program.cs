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

        public static string ConnString
        {
            get { return connString; }
        }

        //golbal configs
        private static string exportFolder;
        private static string storeNo;

        //New Added
        private static string BranchNo;
        private static string Site;
        private static string Contract;
        private static string Floor;
        private static string StoreNumber;


        private static List<int> transTypeNoList;

        public static string ExportFolder
        {
            get { return exportFolder; }
        }

        public static string StoreNo
        {
            get { return storeNo; }
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


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //check running instance, allow only one at a time
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
                return;

            //check if auto mode
            if (args.Length == 1)
                if (args[0] == "A")
                    isAutoMode = true;

            //load app config
            try
            {
                AppConfig.Load(!isAutoMode);
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

            if (isAutoMode)
                Func.Log("Running auto mode.");
            else
                Func.Log("Running manual mode.");

            //set conn string
            if (!SetConnectionString())
            {
                if (!isAutoMode) System.Windows.Forms.MessageBox.Show("Error getting connection string.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Func.Log("Error getting connection string.");
                Application.Exit();
                return;
            }

            if (isAutoMode)
                RunAutoMode();
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain(true));
            }
        }

        private static void RunAutoMode()
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
                Business.Export(Program.BusinessDateStart, Program.BusinessDateEnd);
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[Program/RunAutoMode] Error during auto export.");
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
                    connString = "Initial Catalog=" + AppConfig.DBName + ";Data Source=" + AppConfig.DBServer + ";user ID=datascan;password=dtsbsd7188228;";

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
                storeNo = AppConfig.GetConfig("StoreNo").ToString();

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
