using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using Transight.Interface.Common;
using Transight.Interface.Config;


namespace TransightInterface
{
    public static class Business
    {
        public static string Export(DateTime StartDate, DateTime EndDate)
        {
            FormMain FrmMain = new FormMain(false);

            return Export(StartDate, EndDate, FrmMain);
        }

        public static string Export(DateTime StartDate, DateTime EndDate, FormMain FrmMain)
        {
            try
            {
                string s = string.Empty;

                #region check export folder
                if (Program.ExportFolder == string.Empty || !Directory.Exists(Program.ExportFolder))
                {
                    if (Program.ExportFolder == string.Empty)
                        s = "Export folder not set.";
                    else
                        s = "Export folder not found.";

                    if (!Program.IsAutoMode) 
                        FrmMain.SetStatus(s);
                    else
                        Func.Log(s);

                    return s;
                }
                #endregion check export folder

                //get export data
                int exportCount = 0;
                string storeNum;
                DateTime businessDate = StartDate;
                string fileName;
                List<StoreList> StoreList;
         

                Data.PrepareTempTable();
                

                

                //get storenum
                storeNum = Program.StoreNo; 

                //process each business date
                while (businessDate <= EndDate)
                {
                    int serialnumber = Data.GetSequenceNumber(businessDate);

                    List<StoreList> FinalStoreList = new List<StoreList>();

                    //get gch
                    if (!Program.IsAutoMode) FrmMain.SetStatus("Getting check list for [" + businessDate.ToString(Const.DateFormat) + "]..." );


                    //New added
                    StoreList = Data.GetStoreList(businessDate);  
                    serialnumber = serialnumber + 1;

                    foreach (StoreList SL in StoreList)
                    {
                        DateTime mydate = SL.getbusDate;
                        decimal mySalesAmount = SL.getSalesAmount;
                        int myTotalTransaction = SL.getTotaltransaction;
                        FinalStoreList.Add(new StoreList(mydate, mySalesAmount, myTotalTransaction, serialnumber));            
                    }

         

                if (FinalStoreList.Count == 0)
                {
                    s = "No record(s) to export on [" + businessDate.ToString(Const.DateFormat) + "].";
                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);
                }
                 else
                    {
                       #region write file
                        //Get Sequence Number
                        String SequenceAlpha = Data.GetSequenceSwitch(serialnumber);

                        //set file name
                        fileName = Program.StoreNo.ToString() + "_" + businessDate.ToString("yyyyMMdd") + "_" + SequenceAlpha + ".txt";

                        //delete file
                        try { File.Delete(Program.ExportFolder + @"\" + fileName); }
                        catch { }

                        //check if stil exist
                        if (!File.Exists(Program.ExportFolder + @"\" + fileName))
                        {
                            //writing file
                            if (!Program.IsAutoMode) FrmMain.SetStatus("Writing text file [" + fileName + "]...");

                            if (WriteText(Program.ExportFolder + @"\" + fileName, FinalStoreList))
                            {
                                s = "File [" + fileName + "] export successful.";

                                if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                                Func.Log(s);
                                exportCount++;

                                Data.GetSequenceNumberUpdate(serialnumber, businessDate);

                            }
                            else
                            {
                                s = "Error writing [" + fileName + "].";

                                if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                                Func.Log(s);
                            }
                        }
                        else
                        {
                            //file exist. will not overwrite
                            s = "File [" + fileName + "] already exist.";
                            if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                            Func.Log(s);
                        }
                        #endregion write file
                    }

                //set next day
                businessDate = businessDate.AddDays(1);
                }

                #region set message
                if (exportCount == 0)
                {
                    s = "No record(s) to export.";
                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);
                }
                else
                {
                    s = "Export completed.";
                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);
                    s = string.Empty;
                }

                return s;
                #endregion set message
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[Business/Export] Error during export.");
                ErrorTracking.Log(ex);
                Func.Log("Error during export.");
                return "ERR";
            }
        }

        private static bool WriteText(string FullName, List<StoreList> StoreList)
        {
            try
            {
                FileStream fs = new FileStream(FullName, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);




                foreach (StoreList SL in StoreList)
                {
              
                    //string SalesAmount = SL.getSalesAmount.ToString("0.00").Replace(".","").PadLeft(12,'^');
                    ///string TotalTransaction = SL.getTotaltransaction.ToString().PadLeft(10,'^');
                    string SalesAmount = SL.getSalesAmount.ToString("0.00");

                    sw.WriteLine(Program.StoreNo.ToString() + "," + SL.getbusDate.ToString("dd/MM/yyyy")+ "," + SalesAmount);
                }

                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
                fs.Close();
                fs.Dispose();
                fs = null;

                return true;
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[Business/WriteText] Error writing txt file [" + FullName + "].");
                ErrorTracking.Log(ex);

                return false;
            }
        }
    }
}
