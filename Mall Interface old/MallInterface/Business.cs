using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using Transight.Interface.Common;
using Transight.Interface.Config;
using System.Configuration;
using WinSCP;
using System.Net;
using System.Data;
using System.Windows.Forms;

namespace TransightInterface
{
    
    public static class Business
    {
        

        public static string Export(DateTime StartDate, DateTime EndDate)
        {
            FormMain FrmMain = new FormMain(false);

            return ExportNew(StartDate, EndDate, FrmMain);
        }

        public static string ExportX(DateTime StartDate, DateTime EndDate)
        {
            FormMain FrmMain = new FormMain(false);

            return ExportAuto(StartDate, EndDate, FrmMain);
        }

        #region Export ori
        //public static string Export(DateTime StartDate, DateTime EndDate, FormMain FrmMain)
        //{
        //    try
        //    {
        //        string s = string.Empty;

        //        #region check export folder
        //        if (Program.ExportFolder == string.Empty || !Directory.Exists(Program.ExportFolder))
        //        {
        //            if (Program.ExportFolder == string.Empty)
        //                s = "Export folder not set.";
        //            else
        //                s = "Export folder not found.";

        //            if (!Program.IsAutoMode) 
        //                FrmMain.SetStatus(s);
        //            else
        //                Func.Log(s);

        //            return s;
        //        }
        //        #endregion check export folder

        //        //get export data
        //        int exportCount = 0;
        //        string storeNum;
        //        DateTime businessDate = StartDate;
        //        string fileName;
        //        List<StoreList> StoreList;

        //        Data.PrepareTempTable();


        //        //-- get export folder info
        //        string salefilepath = Program.ExportFolder;
        //        string subfolder = @"\";
        //        string outputpath = salefilepath + subfolder;
        //        string mfilepath = Program.SentFolder;
        //        string outputMpath = mfilepath + subfolder;
        //        string filename = string.Empty;

        //        string exportedFile = "";
        //        //--


        //        //get storenum
        //        SessionOptions sessionOptions;
        //        storeNum = Program.StoreNo;
        //        string path = string.Empty;
        //        string sentfolder = string.Empty;

        //        string sftpip = ConfigurationManager.AppSettings["06"];
        //        string sftpusername = ConfigurationManager.AppSettings["07"];
        //        string sftppwd = ConfigurationManager.AppSettings["08"];

        //        //process each business date
        //        while (businessDate <= EndDate)
        //        {
        //            int serialnumber = Data.GetSequenceNumber(businessDate);

        //            List<StoreList> FinalStoreList = new List<StoreList>();

        //            //get gch
        //            if (!Program.IsAutoMode) FrmMain.SetStatus("Getting check list for [" + businessDate.ToString(Const.DateFormat) + "]...");


        //            //New added
        //            StoreList = Data.GetStoreList(businessDate);
        //            serialnumber = serialnumber + 1;

        //            foreach (StoreList SL in StoreList)
        //            {
        //                DateTime mydate = SL.getbusDate;
        //                decimal mySalesAmount = SL.getSalesAmount;
        //                int myTotalTransaction = SL.getTotaltransaction;
        //                FinalStoreList.Add(new StoreList(mydate, mySalesAmount, myTotalTransaction, serialnumber));
        //            }



        //            if (FinalStoreList.Count == 0)
        //            {
        //                s = "No record(s) to export on [" + businessDate.ToString(Const.DateFormat) + "].";
        //                if (!Program.IsAutoMode) FrmMain.SetStatus(s);
        //                Func.Log(s);
        //            }
        //            else
        //            {
        //                #region write file
        //                //Get Sequence Number
        //                String SequenceAlpha = Data.GetSequenceSwitch(serialnumber);

        //                //set file name
        //                fileName = Program.StoreNo.ToString() + "_" + businessDate.ToString("yyyyMMdd") + "_" + SequenceAlpha + ".txt";
        //                //exportedFile = filename;
        //                //delete file
        //                try { File.Delete(Program.ExportFolder + @"\" + fileName); }
        //                catch { }

        //                //check if stil exist
        //                if (!File.Exists(Program.ExportFolder + @"\" + fileName))
        //                {
        //                    //writing file
        //                    if (!Program.IsAutoMode) FrmMain.SetStatus("Writing text file [" + fileName + "]...");

        //                    if (WriteText(Program.ExportFolder + @"\" + fileName, FinalStoreList))
        //                    {
        //                        s = "File [" + fileName + "] export successful.";

        //                        if (!Program.IsAutoMode) FrmMain.SetStatus(s);
        //                        Func.Log(s);
        //                        exportCount++;

        //                        Data.GetSequenceNumberUpdate(serialnumber, businessDate);

        //                    }
        //                    else
        //                    {
        //                        s = "Error writing [" + fileName + "].";

        //                        if (!Program.IsAutoMode) FrmMain.SetStatus(s);
        //                        Func.Log(s);
        //                    }
        //                }
        //                else
        //                {
        //                    //file exist. will not overwrite
        //                    s = "File [" + fileName + "] already exist.";
        //                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
        //                    Func.Log(s);
        //                }
        //                #endregion write file

        //                //#TCS 2019-10-01
        //                #region upload file to FTP


        //                Boolean SalesTXTFail = false;
        //                try
        //                {
        //                    string FTPOption = AppConfig.GetConfig("FTPOption").ToString();
        //                    string sshkey = AppConfig.GetConfig("SSHKEY").ToString();

        //                    if (FTPOption.ToUpper().Trim() == "TRUE" || FTPOption.ToUpper().Trim() == "Y")
        //                    {
        //                        FTP.SetDetails(@"ftp://" + Program.FTPIP, Program.FTPUserName, Program.FTPPassword);

        //                        path = @"" + outputpath + fileName;

        //                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://" + sftpip + @"/ " + fileName);
        //                        //request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
        //                        request.Method = WebRequestMethods.Ftp.UploadFile;
        //                        request.UseBinary = true;
        //                        request.KeepAlive = false;
        //                        request.UsePassive = true;   // adapt to FTP server who only handle PASSIVE mode

        //                        request.Credentials = new NetworkCredential(sftpusername, sftppwd);
        //                        // Copy the contents of the file to the request stream.  
        //                        StreamReader sourceStream = new StreamReader(path);
        //                        byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
        //                        sourceStream.Close();
        //                        sourceStream.Dispose();

        //                        request.ContentLength = fileContents.Length;
        //                        Stream requestStream = request.GetRequestStream();
        //                        requestStream.Write(fileContents, 0, fileContents.Length);
        //                        requestStream.Close();
        //                        requestStream.Dispose();

        //                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        //                        //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
        //                        Func.Log(path + "Upload succeeded. - " + response.StatusDescription);

        //                        response.Close();

        //                        if (!SalesTXTFail)
        //                        {
        //                            File.Copy(path, outputMpath + fileName, true);
        //                            Func.Log("Copied file to " + Program.SentFolder);
        //                            File.Delete(path);
        //                            Func.Log("Deleting file " + path);
        //                        }

        //                    }
        //                }
        //                catch (WebException e)
        //                {
        //                    Console.WriteLine(e.Message.ToString());
        //                    String status = ((FtpWebResponse)e.Response).StatusDescription;
        //                    //Console.WriteLine(status);
        //                    Func.Log(path + " failed to upload to FTP. - " + status);
        //                    SalesTXTFail = true;
        //                    ErrorTracking.Log("[Business/Export] WebException Error during export to FTP.");
        //                    ErrorTracking.Log(status);


        //                }
        //                catch (Exception ex)
        //                {
        //                    //Console.WriteLine(ex.Message.ToString());
        //                    Func.Log(path + " failed to upload to FTP. - " + ex.Message.ToString());
        //                    SalesTXTFail = true;
        //                    ErrorTracking.Log("[Business/Export] Error during export to FTP.");
        //                    ErrorTracking.Log(ex.Message.ToString());
        //                }

        //                #endregion

        //            }

        //            //set next day
        //            businessDate = businessDate.AddDays(1);
        //        }

        //        #region set message
        //        if (exportCount == 0)
        //        {
        //            s = "No record(s) to export.";
        //            if (!Program.IsAutoMode) FrmMain.SetStatus(s);
        //            Func.Log(s);
        //        }
        //        else
        //        {
        //            s = "Export completed.";
        //            if (!Program.IsAutoMode) FrmMain.SetStatus(s);
        //            Func.Log(s);
        //            s = string.Empty;
        //        }

        //        return s;
        //        #endregion set message
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorTracking.Log("[Business/Export] Error during export.");
        //        ErrorTracking.Log(ex);
        //        Func.Log("Error during export.");
        //        return "ERR";
        //    }
        //}

        #endregion ori


        #region Export tukar ori
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
                List<posmasterSH> posmasterSH;

                Data.PrepareTempTable();


                //-- get export folder info
                string salefilepath = Program.ExportFolder;
                string subfolder = @"\";
                string outputpath = salefilepath + subfolder;
                string mfilepath = Program.SentFolder;
                string outputMpath = mfilepath + subfolder;
                string filename = string.Empty;

                //string exportedFile = "";
                //--


                //get storenum
                //SessionOptions sessionOptions;
                storeNum = Program.StoreNo;
                string path = string.Empty;
                string sentfolder = string.Empty;

                string sftpip = ConfigurationManager.AppSettings["06"];
                string sftpusername = ConfigurationManager.AppSettings["07"];
                string sftppwd = ConfigurationManager.AppSettings["08"];
                string fileno = "000";
                //process each business date
                while (businessDate <= EndDate)
                {
                    int serialnumber = Data.GetSequenceNumber(businessDate);


                    List<posmasterSH> FinalStoreList = new List<posmasterSH>();

                    //get gch
                    if (!Program.IsAutoMode) FrmMain.SetStatus("Getting check list for [" + businessDate.ToString(Const.DateFormat) + "]...");


                    //New added
                    posmasterSH = Data.GetPosmasterSHList(businessDate);
                    serialnumber = serialnumber + 1;

                    foreach (posmasterSH SL in posmasterSH)
                    {
                        DateTime mydate = SL.getbusDate;
                        decimal mySalesAmount = SL.getSalesAmount;
                        int myTotalTransaction = SL.getTotaltransaction;
                        int fileNo = SL.getfileNo;
                        //fileno = SL.fileNo.ToString();
                        fileno = fileNo.ToString().PadLeft(3, '0');
                        FinalStoreList.Add(new posmasterSH(mydate, mySalesAmount, myTotalTransaction, serialnumber, fileNo));
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
                        //fileno = fileNo.ToString().PadLeft(3, '0');


                        //set file name
                        
                        fileName = Program.StoreNo.ToString() + "." + fileno + ".csv";
                        //exportedFile = filename;
                        //delete file
                        try { File.Delete(Program.ExportFolder + @"\" + fileName); }
                        catch { }

                        //check if stil exist
                        //if (!File.Exists(Program.ExportFolder + @"\" + fileName))
                        //{
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
                        //}
                        //else
                        //{
                        //    //file exist. will not overwrite
                        //    s = "File [" + fileName + "] already exist.";
                        //    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                        //    Func.Log(s);
                        //}
                        #endregion write file

                        //#TCS 2019-10-01
                        #region upload file to FTP


                        Boolean SalesTXTFail = false;
                        try
                        {
                            string FTPOption = AppConfig.GetConfig("FTPOption").ToString();
                            string sshkey = AppConfig.GetConfig("SSHKEY").ToString();

                            if (FTPOption.ToUpper().Trim() == "TRUE" || FTPOption.ToUpper().Trim() == "Y")
                            {
                                FTP.SetDetails(@"ftp://" + Program.FTPIP, Program.FTPUserName, Program.FTPPassword);

                                path = @"" + outputpath + fileName;

                                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://" + sftpip + @"/ " + fileName);
                                //request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                                request.Method = WebRequestMethods.Ftp.UploadFile;
                                request.UseBinary = true;
                                request.KeepAlive = false;
                                request.UsePassive = true;   // adapt to FTP server who only handle PASSIVE mode

                                request.Credentials = new NetworkCredential(sftpusername, sftppwd);
                                // Copy the contents of the file to the request stream.  
                                StreamReader sourceStream = new StreamReader(path);
                                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                                sourceStream.Close();
                                sourceStream.Dispose();

                                request.ContentLength = fileContents.Length;
                                Stream requestStream = request.GetRequestStream();
                                requestStream.Write(fileContents, 0, fileContents.Length);
                                requestStream.Close();
                                requestStream.Dispose();

                                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                                //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                                Func.Log(path + "Upload succeeded. - " + response.StatusDescription);

                                response.Close();

                                if (!SalesTXTFail)
                                {
                                    File.Copy(path, outputMpath + fileName, true);
                                    Func.Log("Copied file to " + Program.SentFolder);
                                    File.Delete(path);
                                    Func.Log("Deleting file " + path);
                                }

                            }
                        }
                        catch (WebException e)
                        {
                            Console.WriteLine(e.Message.ToString());
                            String status = ((FtpWebResponse)e.Response).StatusDescription;
                            //Console.WriteLine(status);
                            Func.Log(path + " failed to upload to FTP. - " + status);
                            SalesTXTFail = true;
                            ErrorTracking.Log("[Business/Export] WebException Error during export to FTP.");
                            ErrorTracking.Log(status);


                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine(ex.Message.ToString());
                            Func.Log(path + " failed to upload to FTP. - " + ex.Message.ToString());
                            SalesTXTFail = true;
                            ErrorTracking.Log("[Business/Export] Error during export to FTP.");
                            ErrorTracking.Log(ex.Message.ToString());
                        }

                        #endregion

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

        #endregion ori



        #region export unsend

        public static string ExportUnsend()
        {
            try
            {
                if (!System.IO.Directory.Exists(AppConfig.Sent_Logs))
                {
                    System.IO.Directory.CreateDirectory(AppConfig.Sent_Logs);
                }




                string s = string.Empty;
                string us = string.Empty; //unsent tag
                Boolean SalesTXTFail = false;
                #region check export folder
                if (Program.ExportFolder == string.Empty || !Directory.Exists(Program.ExportFolder))
                {
                    if (Program.ExportFolder == string.Empty)
                        s = "Export folder not set.";
                    else
                        s = "Export folder not found.";

                   

                    return s;
                }
                #endregion check export folder

                //-- get export folder info
                string salefilepath = Program.ExportFolder;
                string subfolder = @"\";
                string outputpath = salefilepath + subfolder;
                string mfilepath = Program.SentFolder;
                string outputMpath = mfilepath + subfolder;
                string filename = string.Empty;


                string path = string.Empty;
                string sentfolder = string.Empty;

                string sftpip = ConfigurationManager.AppSettings["06"];
                string sftpusername = ConfigurationManager.AppSettings["07"];
                string sftppwd = ConfigurationManager.AppSettings["08"];

                //checking for unsent files
                #region check unsent files
                DirectoryInfo d = new DirectoryInfo(Program.ExportFolder);
                foreach (FileInfo file in d.GetFiles())
                {
                    SalesTXTFail = false;
                    try
                    {
                        //string fileName = file.Name.ToString();
                        string FTPOption = AppConfig.GetConfig("FTPOption").ToString();
                        string sshkey = AppConfig.GetConfig("SSHKEY").ToString();

                        if (FTPOption.ToUpper().Trim() == "TRUE" || FTPOption.ToUpper().Trim() == "Y")
                        {
                            FTP.SetDetails(@"ftp://" + Program.FTPIP, Program.FTPUserName, Program.FTPPassword);

                            path = @"" + outputpath + file.Name.ToString();

                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://" + sftpip + @"/" + file.Name.ToString());
                            //request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                            request.Method = WebRequestMethods.Ftp.UploadFile;
                            request.UseBinary = true;
                            request.KeepAlive = false;
                            request.UsePassive = true;   // adapt to FTP server who only handle PASSIVE mode

                            request.Credentials = new NetworkCredential(sftpusername, sftppwd);
                            // Copy the contents of the file to the request stream.  
                            StreamReader sourceStream = new StreamReader(path);
                            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                            sourceStream.Close();
                            sourceStream.Dispose();

                            request.ContentLength = fileContents.Length;
                            Stream requestStream = request.GetRequestStream();
                            requestStream.Write(fileContents, 0, fileContents.Length);
                            requestStream.Close();
                            requestStream.Dispose();

                            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                            //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                            //System.Windows.Forms.MessageBox.Show("Sales file successfully sent to RLC server.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            //Sales file successfully sent to RLC server”
                            Func.Log(path + "Upload succeeded. - " + response.StatusDescription);
                            string line = "";
                            string busidate = "";
                            DateTime unsentBusidate;
                            using (StreamReader sr = new StreamReader(path))
                            {
                                while (!sr.EndOfStream)
                                {
                                    line = sr.ReadLine();
                                    if (line.Contains("1800000"))
                                    {
                                        //business date inside file
                                        busidate = line.Substring(line.Length - 10);
                                    }

                                }
                                sr.Dispose();
                            }
                            unsentBusidate = DateTime.ParseExact(busidate, "MM/dd/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);


                            Data.InsertBatchLogs(file.Name.ToString(), unsentBusidate);
                            response.Close();
                            us = "Send successfully.";
                            if (!SalesTXTFail)
                            {
                                File.Copy(path, outputMpath + file.Name.ToString(), true);
                                Func.Log("Copied file to " + Program.SentFolder);
                                File.Delete(path);
                                Func.Log("Deleting file " + path);
                            }

                        }
                        else 
                        {
                            us = "FTP not activated.";
                            s = "FTP not activated.";
                        }
                    }
                    catch (WebException e)
                    {
                        Console.WriteLine(e.Message.ToString());
                        String status = ((FtpWebResponse)e.Response).StatusDescription;
                        //Console.WriteLine(status);
                        //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface WebException", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        //Sales file is not sent to RLC server. Please contact your POS vendor
                        Func.Log(path + " failed to upload to FTP again. - " + status);
                        SalesTXTFail = true;
                        ErrorTracking.Log("[Business/Export] WebException Error during export to FTP.");
                        ErrorTracking.Log(status);


                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message.ToString());
                        Func.Log(path + " failed to upload to FTP. - " + ex.Message.ToString());
                        //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        SalesTXTFail = true;
                        ErrorTracking.Log("[Business/Export] Error during export to FTP.");
                        ErrorTracking.Log(ex.Message.ToString());
                    }
                }
                #endregion check unsent files



                #region set message
                if (SalesTXTFail == true)
                {
                    s = "FTP server Error";
                    //if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);
                }
                else if (us == string.Empty)
                {
                    s = "No unsend file";

                    Func.Log(s);
                }
                else if (s == "FTP not activated.")
                {
                    s = "FTP not activated";

                    Func.Log(s);
                }
                else
                {
                    s = "Sending completed.";
                    //if (!Program.IsAutoMode) FrmMain.SetStatus(s);
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


        #endregion export unsend

        public static string DeleteLogs(DateTime busidate, string filename)
        {
            string s = string.Empty;

            try
            {
                DataFunctions.Execute("Delete from mallinterface_batchlogs where  businessdate = '" + busidate + "' and filename = '" + filename + "'");
                s = "Export completed.";
                Func.Log(s);
                s = string.Empty;
            }
            catch (Exception ex)
            {
                s = ex.Message.ToString();
                ErrorTracking.Log("[Business/DeleteLogs] Error during deleting logs.");
                ErrorTracking.Log(ex.Message.ToString());
            }


            

            return s;
        }




        private static string terminalno;

        #region Export  
        public static string ExportNew(DateTime StartDate, DateTime EndDate, FormMain FrmMain)
        {
            try
            {
                if (!System.IO.Directory.Exists(AppConfig.Sent_Logs))
                {
                    System.IO.Directory.CreateDirectory(AppConfig.Sent_Logs);
                }

                


                string s = string.Empty;
                string us = string.Empty; //unsent tag
                Boolean SalesTXTFail = false;
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

                //-- get export folder info
                string salefilepath = Program.ExportFolder;
                string subfolder = @"\";
                string outputpath = salefilepath + subfolder;
                string mfilepath = Program.SentFolder;
                string outputMpath = mfilepath + subfolder;
                string filename = string.Empty;


                string path = string.Empty;
                string sentfolder = string.Empty;

                string sftpip = ConfigurationManager.AppSettings["06"];
                string sftpusername = ConfigurationManager.AppSettings["07"];
                string sftppwd = ConfigurationManager.AppSettings["08"];
                string sftpkey = ConfigurationManager.AppSettings["09"];
                string sftpport = ConfigurationManager.AppSettings["10"];

                //checking for unsent files
                #region check unsent files
                DirectoryInfo d = new DirectoryInfo(Program.ExportFolder);
                foreach (FileInfo file in d.GetFiles())
                {
                    SalesTXTFail = false;
                    try
                    {
                        //string fileName = file.Name.ToString();
                        string FTPOption = AppConfig.GetConfig("FTPOption").ToString();
                        string SFTPOption = AppConfig.GetConfig("SFTPOption").ToString();
                        string sshkey = AppConfig.GetConfig("SSHKEY").ToString();
                        string SFTPDestination = AppConfig.GetConfig("SFTPDestination").ToString();

                        if (FTPOption.ToUpper().Trim() == "TRUE" || FTPOption.ToUpper().Trim() == "Y")
                        {
                            FTP.SetDetails(@"ftp://" + Program.FTPIP, Program.FTPUserName, Program.FTPPassword);

                            path = @"" + outputpath + file.Name.ToString();

                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://" + sftpip + @"/" + file.Name.ToString());
                            //request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                            request.Method = WebRequestMethods.Ftp.UploadFile;
                            request.UseBinary = true;
                            request.KeepAlive = false;
                            request.UsePassive = true;   // adapt to FTP server who only handle PASSIVE mode

                            request.Credentials = new NetworkCredential(sftpusername, sftppwd);
                            // Copy the contents of the file to the request stream.  
                            StreamReader sourceStream = new StreamReader(path);
                            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                            sourceStream.Close();
                            sourceStream.Dispose();

                            request.ContentLength = fileContents.Length;
                            Stream requestStream = request.GetRequestStream();
                            requestStream.Write(fileContents, 0, fileContents.Length);
                            requestStream.Close();
                            requestStream.Dispose();

                            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                            //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                            //System.Windows.Forms.MessageBox.Show("Sales file successfully sent to RLC server.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            //Sales file successfully sent to RLC server”
                            Func.Log(path + "Upload succeeded. - " + response.StatusDescription);
                            string line = "";
                            string busidate = "";
                            DateTime unsentBusidate;
                            using (StreamReader sr = new StreamReader(path))
                            {
                                while (!sr.EndOfStream)
                                {
                                    line = sr.ReadLine();
                                    if (line.Contains("1800000"))
                                    {
                                        //business date inside file
                                        busidate = line.Substring(line.Length - 10);
                                    }

                                }
                                sr.Dispose();
                            }
                            unsentBusidate = DateTime.ParseExact(busidate, "MM/dd/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);


                            Data.InsertBatchLogs(file.Name.ToString(), unsentBusidate);
                            response.Close();
                            us = "Send successfully.";
                            if (!SalesTXTFail)
                            {
                                File.Copy(path, outputMpath + file.Name.ToString(), true);
                                Func.Log("Copied file to " + Program.SentFolder);
                                File.Delete(path);
                                Func.Log("Deleting file " + path);
                            }

                        }
                        else if ((SFTPOption.ToUpper().Trim() == "TRUE" || SFTPOption.ToUpper().Trim() == "Y") && sftpkey.Trim() != "")
                        {

                            path = @"" + outputpath + file.Name.ToString();

                            SessionOptions sessionOptions = new SessionOptions();
                            sessionOptions.Protocol = Protocol.Sftp;
                            sessionOptions.HostName = sftpip;
                            sessionOptions.UserName = sftpusername;
                            sessionOptions.Password = sftppwd;
                            sessionOptions.PortNumber = Convert.ToInt32(sftpport);
                            sessionOptions.SshHostKeyFingerprint = sftpkey;
                            //sessionOptions.TimeoutInMilliseconds = 7000;
                            Session session = new Session();
                            session.Open(sessionOptions);
                            TransferOptions transferOptions = new TransferOptions();
                            transferOptions.TransferMode = TransferMode.Binary;
                            TransferOperationResult transferResult;
                            //This is for Getting/Downloading files from SFTP  
                            //transferResult = session.GetFiles(filepath, destinatonpath, false, transferOptions);

                            //This is for Putting/Uploading file on SFTP  
                            transferResult = session.PutFiles(path, SFTPDestination, false, transferOptions);
                            transferResult.Check();

                            string line = "";
                            string busidate = "";
                            DateTime unsentBusidate;
                            using (StreamReader sr = new StreamReader(path))
                            {
                                while (!sr.EndOfStream)
                                {
                                    line = sr.ReadLine();
                                    if (line.Contains("1800000"))
                                    {
                                        //business date inside file
                                        busidate = line.Substring(line.Length - 10);
                                    }

                                }
                                sr.Dispose();
                            }
                            unsentBusidate = DateTime.ParseExact(busidate, "MM/dd/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);


                            Data.InsertBatchLogs(file.Name.ToString(), unsentBusidate);
                           
                            us = "Send successfully.";
                            if (!SalesTXTFail)
                            {
                                File.Copy(path, outputMpath + file.Name.ToString(), true);
                                Func.Log("Copied file to " + Program.SentFolder);
                                File.Delete(path);
                                Func.Log("Deleting file " + path);
                            }


                        }
                    }
                    catch (WebException e)
                    {
                        Console.WriteLine(e.Message.ToString());
                        String status = ((FtpWebResponse)e.Response).StatusDescription;
                        //Console.WriteLine(status);
                        //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface WebException", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        //Sales file is not sent to RLC server. Please contact your POS vendor
                        Func.Log(path + " failed to upload to FTP again. - " + status);
                        SalesTXTFail = true;
                        ErrorTracking.Log("[Business/Export] WebException Error during export to FTP.");
                        ErrorTracking.Log(status);


                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message.ToString());
                        Func.Log(path + " failed to upload to FTP. - " + ex.Message.ToString());
                        //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        SalesTXTFail = true;
                        ErrorTracking.Log("[Business/Export] Error during export to FTP.");
                        ErrorTracking.Log(ex.Message.ToString());
                    }
                }
                #endregion check unsent files


                //get export data
                int exportCount = 0;
                string storeNum;
                //string machineId;
                DateTime businessDate = StartDate;
                string fileName;
                List<posmasterSHNew> posmasterSHNew;

                Data.PrepareTempTable();




                //string exportedFile = "";
                //--

                
                //get storenum
                //SessionOptions sessionOptions;
                storeNum = Program.StoreNo;
                //machineId = Program.StoreNo;
                
                
                //process each business date
                while (businessDate <= EndDate)
                {
                    int serialnumber = Data.GetSequenceNumber(businessDate);

                    List<posmasterSHNew> FinalStoreListNew = new List<posmasterSHNew>();

                    //get gch
                    if (!Program.IsAutoMode) FrmMain.SetStatus("Getting check list for [" + businessDate.ToString(Const.DateFormat) + "]...");


                    //New added
                    posmasterSHNew = Data.GetPosmasterSHNewList(businessDate);
                    serialnumber = serialnumber + 1;

                    foreach (posmasterSHNew SL in posmasterSHNew)
                    {
                        string TenantID = Program.StoreNo.PadLeft(16, '0');
                        string TerminalNo = SL.getTerminalNo.PadLeft(16, '0');
                        terminalno = SL.getTerminalNo.PadLeft(2, '0');
                        string GrossSales = SL.getGrossSales.PadLeft(16, '0');
                        string TTax = SL.getTTax.PadLeft(16, '0');
                        string TVoid = SL.getTVoid.PadLeft(16, '0');
                        string CVoid = SL.getCVoid.PadLeft(16, '0');
                        string TDisc = SL.getTDisc.PadLeft(16, '0');
                        string CDisc = SL.getCDisc.PadLeft(16, '0');
                        string TRefund = SL.getTRefund.PadLeft(16, '0');
                        string CRefund = SL.getCRefund.PadLeft(16, '0');
                        string TNegAdj = SL.getTNegAdj.PadLeft(16, '0');
                        string CNegAdj = SL.getCNegAdj.PadLeft(16, '0');
                        string TServChg = SL.getTServChg.PadLeft(16, '0');
                        string PrevZCnt = SL.getPrevZCnt.PadLeft(16, '0');
                        string PrevGT = SL.getPrevGT.PadLeft(16, '0');
                        string NewZCnt = SL.getNewZCnt.PadLeft(16, '0');
                        string NewGT = SL.getNewGT.PadLeft(16, '0');
                        string BizDate = SL.getBizDate.PadLeft(16, '0');
                        string Novelty = SL.getNovelty.PadLeft(16, '0');
                        string Misc = SL.getMisc.PadLeft(16, '0');
                        string LocalTax = SL.getLocalTax.PadLeft(16, '0');
                        string TCreditSales = SL.getTCreditSales.PadLeft(16, '0');
                        string TCreditTax = SL.getTCreditTax.PadLeft(16, '0');
                        string TNVatSales = SL.getTNVatSales.PadLeft(16, '0');
                        string Pharma = SL.getPharma.PadLeft(16, '0');
                        string NPharma = SL.getNPharma.PadLeft(16, '0');
                        string TPWDDisc = SL.getTPWDDisc.PadLeft(16, '0');
                        string GrossNotSub = SL.getGrossNotSub.PadLeft(16, '0');
                        string TReprint = SL.getTReprint.PadLeft(16, '0');
                        string CReprint = SL.getCReprint.PadLeft(16, '0');


                        FinalStoreListNew.Add(new posmasterSHNew(TenantID,
                        TerminalNo,
                        GrossSales,
                        TTax,
                        TVoid,
                        CVoid,
                        TDisc,
                        CDisc,
                        TRefund,
                        CRefund,
                        TNegAdj,
                        CNegAdj,
                        TServChg,
                        PrevZCnt,
                        PrevGT,
                        NewZCnt,
                        NewGT,
                        BizDate,
                        Novelty,
                        Misc,
                        LocalTax,
                        TCreditSales,
                        TCreditTax,
                        TNVatSales,
                        Pharma,
                        NPharma,
                        TPWDDisc,
                        GrossNotSub,
                        TReprint,
                        CReprint));
                        }


                    //if (FinalStoreListNew.Count == 0)
                    //{
                    //    FinalStoreListNew.Add(new posmasterSHNew(businessDate.ToString("MM/dd/yyyy HH:mm"), businessDate.ToString("yyyyMMdd"),  0, 0, 0, 0, 0, 0, 0, 0));

                    //    s = "No record(s) to export on [" + businessDate.ToString(Const.DateFormat) + "].";
                    //    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    //    Func.Log(s);
                    //}
                    Data.InsertBatchLogs(fileName, businessDate);
                    if (FinalStoreListNew.Count > 0)
                    {
                        #region write file
                        //Get Sequence Number
                        String SequenceAlpha = Data.GetSequenceSwitch(serialnumber);
                        int BatchNumber = Data.GetBatchNumber(businessDate) + 1;
                         

                        //set file name
                        //fileName = "H" + Program.StoreNo.ToString() + "_" + businessDate.ToString("yyyyMMdd") + "_" + SequenceAlpha + ".txt";
                        //fileName = "H" + Program.StoreNo.ToString() + "_" + businessDate.ToString("yyyyMMdd") + ".txt";
                        fileName = Program.StoreNo.Substring(Program.StoreNo.Length-4) + businessDate.ToString("MMdd") + "." + terminalno + BatchNumber.ToString();
                        //exportedFile = filename;
                        //delete file
                        try { File.Delete(Program.ExportFolder + @"\" + fileName); }
                        catch { }

                        //check if stil exist
                        //if (!File.Exists(Program.ExportFolder + @"\" + fileName))
                        //{
                            //writing file
                            if (!Program.IsAutoMode) FrmMain.SetStatus("Writing text file [" + fileName + "]...");

                            if (WriteTextNew(Program.ExportFolder + @"\" + fileName, FinalStoreListNew))
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
                        //}
                        //else
                        //{
                        //    //file exist. will not overwrite
                        //    s = "File [" + fileName + "] already exist.";
                        //    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                        //    Func.Log(s);
                        //}
                        #endregion write file
                        //Data.InsertBatchLogs(fileName, businessDate);
                        //#TCS 2019-10-01
                        #region upload file to FTP


                        SalesTXTFail = false;
                        try
                        {
                            string FTPOption = AppConfig.GetConfig("FTPOption").ToString();
                            string sshkey = AppConfig.GetConfig("SSHKEY").ToString();
                            string SFTPOption = AppConfig.GetConfig("SFTPOption").ToString();
                            string SFTPDestination = AppConfig.GetConfig("SFTPDestination").ToString();

                            if (FTPOption.ToUpper().Trim() == "TRUE" || FTPOption.ToUpper().Trim() == "Y")
                            {
                                FTP.SetDetails(@"ftp://" + Program.FTPIP, Program.FTPUserName, Program.FTPPassword);

                                path = @"" + outputpath + fileName;

                                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://" + sftpip + @"/" + fileName);
                                //request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                                request.Method = WebRequestMethods.Ftp.UploadFile;
                                request.UseBinary = true;
                                request.KeepAlive = false;
                                request.UsePassive = true;   // adapt to FTP server who only handle PASSIVE mode

                                request.Credentials = new NetworkCredential(sftpusername, sftppwd);
                                // Copy the contents of the file to the request stream.  
                                StreamReader sourceStream = new StreamReader(path);
                                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                                sourceStream.Close();
                                sourceStream.Dispose();

                                request.ContentLength = fileContents.Length;
                                Stream requestStream = request.GetRequestStream();
                                requestStream.Write(fileContents, 0, fileContents.Length);
                                requestStream.Close();
                                requestStream.Dispose();

                                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                                //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                                //System.Windows.Forms.MessageBox.Show("Sales file successfully sent to RLC server.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                //Sales file successfully sent to RLC server”
                                Func.Log(path + "Upload succeeded. - " + response.StatusDescription);
                                //Data.InsertBatchLogs(fileName, businessDate);
                                response.Close();
                                s = "Send successfully.";
                                if (!SalesTXTFail)
                                {
                                    File.Copy(path, outputMpath + fileName, true);
                                    Func.Log("Copied file to " + Program.SentFolder);
                                    File.Delete(path);
                                    Func.Log("Deleting file " + path);
                                }

                            }
                            else if ((SFTPOption.ToUpper().Trim() == "TRUE" || SFTPOption.ToUpper().Trim() == "Y") && sftpkey.Trim() != "")
                            {

                                path = @"" + outputpath + fileName;

                                SessionOptions sessionOptions = new SessionOptions();
                                sessionOptions.Protocol = Protocol.Sftp;
                                sessionOptions.HostName = sftpip;
                                sessionOptions.UserName = sftpusername;
                                sessionOptions.Password = sftppwd;
                                sessionOptions.PortNumber = Convert.ToInt32(sftpport);
                                sessionOptions.SshHostKeyFingerprint = sftpkey;
                                //sessionOptions.TimeoutInMilliseconds = 7000;
                                Session session = new Session();
                                session.Open(sessionOptions);
                                TransferOptions transferOptions = new TransferOptions();
                                transferOptions.TransferMode = TransferMode.Binary;
                                TransferOperationResult transferResult;
                                //This is for Getting/Downloading files from SFTP  
                                //transferResult = session.GetFiles(filepath, destinatonpath, false, transferOptions);

                                //This is for Putting/Uploading file on SFTP  
                                transferResult = session.PutFiles(path, SFTPDestination, false, transferOptions);
                                transferResult.Check();



                                Func.Log(path + "Upload succeeded. - ");
                                //Data.InsertBatchLogs(fileName, businessDate);

                                us = "Send successfully.";
                                if (!SalesTXTFail)
                                {
                                    File.Copy(path, outputMpath + fileName, true);
                                    Func.Log("Copied file to " + Program.SentFolder);
                                    File.Delete(path);
                                    Func.Log("Deleting file " + path);
                                }


                            }
                        }
                        catch (WebException e)
                        {
                            Console.WriteLine(e.Message.ToString());
                            String status = ((FtpWebResponse)e.Response).StatusDescription;
                            //Console.WriteLine(status);
                            //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface WebException", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                            //Sales file is not sent to RLC server. Please contact your POS vendor
                            Func.Log(path + " failed to upload to SFTP. - " + status);
                            SalesTXTFail = true;
                            ErrorTracking.Log("[Business/Export] WebException Error during export to FTP.");
                            ErrorTracking.Log(status);


                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine(ex.Message.ToString());
                            Func.Log(path + " failed to upload to SFTP. - " + ex.Message.ToString());
                            //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                            SalesTXTFail = true;
                            ErrorTracking.Log("[Business/Export] Error during export to FTP.");
                            ErrorTracking.Log(ex.Message.ToString());
                        }

                        #endregion

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
                else if (SalesTXTFail == true)
                {
                    s = "FTP server Error";
                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);
                }
                else if (s.Contains("export successful."))
                {
                    s = "Export completed.";
                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);
                    
                }
                else if (us == "Send successfully.")
                {
                    s = "Pending sent successfully.";
                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);
                }
                else
                {
                    s = "Sending completed.";
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

        #endregion kfc


        #region export KLCC



        public static string ExportAuto(DateTime StartDate, DateTime EndDate, FormMain FrmMain)
        {
            try
            {
                if (!System.IO.Directory.Exists(AppConfig.Sent_Logs))
                {
                    System.IO.Directory.CreateDirectory(AppConfig.Sent_Logs);
                }




                string s = string.Empty;
                string us = string.Empty; //unsent tag
                Boolean SalesTXTFail = false;
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

                //-- get export folder info
                string salefilepath = Program.ExportFolder;
                string subfolder = @"\";
                string outputpath = salefilepath + subfolder;
                string mfilepath = Program.SentFolder;
                string outputMpath = mfilepath + subfolder;
                string filename = string.Empty;


                string path = string.Empty;
                string sentfolder = string.Empty;

                string sftpip = ConfigurationManager.AppSettings["06"];
                string sftpusername = ConfigurationManager.AppSettings["07"];
                string sftppwd = ConfigurationManager.AppSettings["08"];
                string sftpkey = ConfigurationManager.AppSettings["09"];
                string sftpport = ConfigurationManager.AppSettings["10"];

               

                //checking for unsent files
                #region check unsent files
                DirectoryInfo d = new DirectoryInfo(Program.ExportFolder);
                foreach (FileInfo file in d.GetFiles())
                {
                    SalesTXTFail = false;
                    try
                    {
                        //string fileName = file.Name.ToString();
                        string FTPOption = AppConfig.GetConfig("FTPOption").ToString();
                        string sshkey = AppConfig.GetConfig("SSHKEY").ToString();

                        if (FTPOption.ToUpper().Trim() == "TRUE" || FTPOption.ToUpper().Trim() == "Y")
                        {
                            FTP.SetDetails(@"ftp://" + Program.FTPIP, Program.FTPUserName, Program.FTPPassword);

                            path = @"" + outputpath + file.Name.ToString();

                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://" + sftpip + @"/" + file.Name.ToString());
                            //request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                            request.Method = WebRequestMethods.Ftp.UploadFile;
                            request.UseBinary = true;
                            request.KeepAlive = false;
                            request.UsePassive = true;   // adapt to FTP server who only handle PASSIVE mode

                            request.Credentials = new NetworkCredential(sftpusername, sftppwd);
                            // Copy the contents of the file to the request stream.  
                            StreamReader sourceStream = new StreamReader(path);
                            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                            sourceStream.Close();
                            sourceStream.Dispose();

                            request.ContentLength = fileContents.Length;
                            Stream requestStream = request.GetRequestStream();
                            requestStream.Write(fileContents, 0, fileContents.Length);
                            requestStream.Close();
                            requestStream.Dispose();

                            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                            //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                            //System.Windows.Forms.MessageBox.Show("Sales file successfully sent to RLC server.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            //Sales file successfully sent to RLC server”
                            Func.Log(path + "Upload succeeded. - " + response.StatusDescription);
                            string line = "";
                            string busidate = "";
                            DateTime unsentBusidate;
                            using (StreamReader sr = new StreamReader(path))
                            {
                                while (!sr.EndOfStream)
                                {
                                    line = sr.ReadLine();
                                    if (line.Contains("1800000"))
                                    {
                                        //business date inside file
                                        busidate = line.Substring(line.Length - 10);
                                    }

                                }
                                sr.Dispose();
                            }
                            unsentBusidate = DateTime.ParseExact(busidate, "MM/dd/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);


                            Data.InsertBatchLogs(file.Name.ToString(), unsentBusidate);
                            response.Close();
                            us = "Send successfully.";
                            if (!SalesTXTFail)
                            {
                                File.Copy(path, outputMpath + file.Name.ToString(), true);
                                Func.Log("Copied file to " + Program.SentFolder);
                                File.Delete(path);
                                Func.Log("Deleting file " + path);
                            }

                        }


                    }
                    catch (WebException e)
                    {
                        Console.WriteLine(e.Message.ToString());
                        String status = ((FtpWebResponse)e.Response).StatusDescription;
                        //Console.WriteLine(status);
                        //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface WebException", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        //Sales file is not sent to RLC server. Please contact your POS vendor
                        Func.Log(path + " failed to upload to FTP again. - " + status);
                        SalesTXTFail = true;
                        ErrorTracking.Log("[Business/Export] WebException Error during export to FTP.");
                        ErrorTracking.Log(status);


                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message.ToString());
                        Func.Log(path + " failed to upload to FTP. - " + ex.Message.ToString());
                        //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        SalesTXTFail = true;
                        ErrorTracking.Log("[Business/Export] Error during export to FTP.");
                        ErrorTracking.Log(ex.Message.ToString());
                    }
                }
                #endregion check unsent files


                //get export data
                int exportCount = 0;
                string storeNum;
                //string machineId;
                DateTime businessDate = StartDate;
                string fileName;
                List<posmasterSHNew> posmasterSHNew;

                Data.PrepareTempTable();




                //string exportedFile = "";
                //--


                //get storenum
                //SessionOptions sessionOptions;
                storeNum = Program.StoreNo;
                //machineId = Program.StoreNo;


                //process each business date
                while (businessDate <= EndDate)
                {
                    int serialnumber = Data.GetSequenceNumber(businessDate);

                    List<posmasterSHNew> FinalStoreListNew = new List<posmasterSHNew>();

                    //get gch
                    if (!Program.IsAutoMode) FrmMain.SetStatus("Getting check list for [" + businessDate.ToString(Const.DateFormat) + "]...");


                    //New added
                    posmasterSHNew = Data.GetPosmasterSHNewList(businessDate);
                    serialnumber = serialnumber + 1;

                    foreach (posmasterSHNew SL in posmasterSHNew)
                    {
                        string TenantID = Program.StoreNo.PadLeft(16, '0');
                        string TerminalNo = SL.getTerminalNo.PadLeft(16, '0');
                        terminalno = SL.getTerminalNo.PadLeft(2, '0');
                        string GrossSales = SL.getGrossSales.PadLeft(16, '0');
                        string TTax = SL.getTTax.PadLeft(16, '0');
                        string TVoid = SL.getTVoid.PadLeft(16, '0');
                        string CVoid = SL.getCVoid.PadLeft(16, '0');
                        string TDisc = SL.getTDisc.PadLeft(16, '0');
                        string CDisc = SL.getCDisc.PadLeft(16, '0');
                        string TRefund = SL.getTRefund.PadLeft(16, '0');
                        string CRefund = SL.getCRefund.PadLeft(16, '0');
                        string TNegAdj = SL.getTNegAdj.PadLeft(16, '0');
                        string CNegAdj = SL.getCNegAdj.PadLeft(16, '0');
                        string TServChg = SL.getTServChg.PadLeft(16, '0');
                        string PrevZCnt = SL.getPrevZCnt.PadLeft(16, '0');
                        string PrevGT = SL.getPrevGT.PadLeft(16, '0');
                        string NewZCnt = SL.getNewZCnt.PadLeft(16, '0');
                        string NewGT = SL.getNewGT.PadLeft(16, '0');
                        string BizDate = SL.getBizDate.PadLeft(16, '0');
                        string Novelty = SL.getNovelty.PadLeft(16, '0');
                        string Misc = SL.getMisc.PadLeft(16, '0');
                        string LocalTax = SL.getLocalTax.PadLeft(16, '0');
                        string TCreditSales = SL.getTCreditSales.PadLeft(16, '0');
                        string TCreditTax = SL.getTCreditTax.PadLeft(16, '0');
                        string TNVatSales = SL.getTNVatSales.PadLeft(16, '0');
                        string Pharma = SL.getPharma.PadLeft(16, '0');
                        string NPharma = SL.getNPharma.PadLeft(16, '0');
                        string TPWDDisc = SL.getTPWDDisc.PadLeft(16, '0');
                        string GrossNotSub = SL.getGrossNotSub.PadLeft(16, '0');
                        string TReprint = SL.getTReprint.PadLeft(16, '0');
                        string CReprint = SL.getCReprint.PadLeft(16, '0');


                        FinalStoreListNew.Add(new posmasterSHNew(TenantID,
        TerminalNo,
        GrossSales,
        TTax,
        TVoid,
        CVoid,
        TDisc,
        CDisc,
        TRefund,
        CRefund,
        TNegAdj,
        CNegAdj,
        TServChg,
        PrevZCnt,
        PrevGT,
        NewZCnt,
        NewGT,
        BizDate,
        Novelty,
        Misc,
        LocalTax,
        TCreditSales,
        TCreditTax,
        TNVatSales,
        Pharma,
        NPharma,
        TPWDDisc,
        GrossNotSub,
        TReprint,
        CReprint));
                    }


                    //if (FinalStoreListNew.Count == 0)
                    //{
                    //    FinalStoreListNew.Add(new posmasterSHNew(businessDate.ToString("MM/dd/yyyy HH:mm"), businessDate.ToString("yyyyMMdd"),  0, 0, 0, 0, 0, 0, 0, 0));

                    //    s = "No record(s) to export on [" + businessDate.ToString(Const.DateFormat) + "].";
                    //    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    //    Func.Log(s);
                    //}

                    if (FinalStoreListNew.Count > 0)
                    {
                        #region write file
                        //Get Sequence Number
                        String SequenceAlpha = Data.GetSequenceSwitch(serialnumber);
                        int BatchNumber = Data.GetBatchNumber(businessDate) + 1;

                            //if (BatchNumber > 1)
                            //{
                            //    break;
                            //}

                        //set file name
                        //fileName = "H" + Program.StoreNo.ToString() + "_" + businessDate.ToString("yyyyMMdd") + "_" + SequenceAlpha + ".txt";
                        //fileName = "H" + Program.StoreNo.ToString() + "_" + businessDate.ToString("yyyyMMdd") + ".txt";
                        fileName = Program.StoreNo.Substring(Program.StoreNo.Length - 4) + businessDate.ToString("MMdd") + "." + terminalno + BatchNumber.ToString();
                        //exportedFile = filename;
                        //delete file
                        try { File.Delete(Program.ExportFolder + @"\" + fileName); }
                        catch { }

                        //check if stil exist
                        // if (!File.Exists(Program.ExportFolder + @"\" + fileName))
                        Data.InsertBatchLogs(fileName, businessDate);
                        exportCount++;
                        //writing file
                        if (!Program.IsAutoMode) FrmMain.SetStatus("Writing text file [" + fileName + "]...");

                            if (WriteTextNew(Program.ExportFolder + @"\" + fileName, FinalStoreListNew))
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
                        
                        //else
                        //{
                        //    //file exist. will not overwrite
                        //    s = "File [" + fileName + "] already exist.";
                        //    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                        //    Func.Log(s);
                       // }
                        #endregion write file
                       // Data.InsertBatchLogs(fileName, businessDate);
                        //#TCS 2019-10-01
                        #region upload file to FTP


                        SalesTXTFail = false;
                        try
                        {
                            string FTPOption = AppConfig.GetConfig("FTPOption").ToString();
                            string sshkey = AppConfig.GetConfig("SSHKEY").ToString();
                            string SFTPOption = AppConfig.GetConfig("SFTPOption").ToString();
                            string SFTPDestination = AppConfig.GetConfig("SFTPDestination").ToString();

                            if (FTPOption.ToUpper().Trim() == "TRUE" || FTPOption.ToUpper().Trim() == "Y")
                            {
                                FTP.SetDetails(@"ftp://" + Program.FTPIP, Program.FTPUserName, Program.FTPPassword);

                                path = @"" + outputpath + fileName;

                                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://" + sftpip + @"/" + fileName);
                                //request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                                request.Method = WebRequestMethods.Ftp.UploadFile;
                                request.UseBinary = true;
                                request.KeepAlive = false;
                                request.UsePassive = true;   // adapt to FTP server who only handle PASSIVE mode

                                request.Credentials = new NetworkCredential(sftpusername, sftppwd);
                                // Copy the contents of the file to the request stream.  
                                StreamReader sourceStream = new StreamReader(path);
                                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                                sourceStream.Close();
                                sourceStream.Dispose();

                                request.ContentLength = fileContents.Length;
                                Stream requestStream = request.GetRequestStream();
                                requestStream.Write(fileContents, 0, fileContents.Length);
                                requestStream.Close();
                                requestStream.Dispose();

                                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                                //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                                //System.Windows.Forms.MessageBox.Show("Sales file successfully sent to RLC server.", "Transight Interface", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                //Sales file successfully sent to RLC server”
                                Func.Log(path + "Upload succeeded. - " + response.StatusDescription);
                                //Data.InsertBatchLogs(fileName, businessDate);
                                response.Close();
                                s = "Send successfully.";
                                if (!SalesTXTFail)
                                {
                                    File.Copy(path, outputMpath + fileName, true);
                                    Func.Log("Copied file to " + Program.SentFolder);
                                    File.Delete(path);
                                    Func.Log("Deleting file " + path);
                                }

                            }
                            else if ((SFTPOption.ToUpper().Trim() == "TRUE" || SFTPOption.ToUpper().Trim() == "Y") && sftpkey.Trim() != "")
                            {

                                path = @"" + outputpath + fileName;

                                SessionOptions sessionOptions = new SessionOptions();
                                sessionOptions.Protocol = Protocol.Sftp;
                                sessionOptions.HostName = sftpip;
                                sessionOptions.UserName = sftpusername;
                                sessionOptions.Password = sftppwd;
                                sessionOptions.PortNumber = Convert.ToInt32(sftpport);
                                sessionOptions.SshHostKeyFingerprint = sftpkey;
                                //sessionOptions.TimeoutInMilliseconds = 7000;
                                Session session = new Session();
                                session.Open(sessionOptions);
                                TransferOptions transferOptions = new TransferOptions();
                                transferOptions.TransferMode = TransferMode.Binary;
                                TransferOperationResult transferResult;
                                //This is for Getting/Downloading files from SFTP  
                                //transferResult = session.GetFiles(filepath, destinatonpath, false, transferOptions);

                                //This is for Putting/Uploading file on SFTP  
                                transferResult = session.PutFiles(path, SFTPDestination, false, transferOptions);
                                transferResult.Check();



                                Func.Log(path + "Upload succeeded. - ");
                                //Data.InsertBatchLogs(fileName, businessDate);

                                us = "Send successfully.";
                                if (!SalesTXTFail)
                                {
                                    File.Copy(path, outputMpath + fileName, true);
                                    Func.Log("Copied file to " + Program.SentFolder);
                                    File.Delete(path);
                                    Func.Log("Deleting file " + path);
                                }


                            }
                        }
                        catch (WebException e)
                        {
                            Console.WriteLine(e.Message.ToString());
                            String status = ((FtpWebResponse)e.Response).StatusDescription;
                            //Console.WriteLine(status);
                            //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface WebException", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                            //Sales file is not sent to RLC server. Please contact your POS vendor
                            Func.Log(path + " failed to upload to FTP. - " + status);
                            SalesTXTFail = true;
                            ErrorTracking.Log("[Business/Export] WebException Error during export to FTP.");
                            ErrorTracking.Log(status);


                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine(ex.Message.ToString());
                            Func.Log(path + " failed to upload to FTP. - " + ex.Message.ToString());
                            //System.Windows.Forms.MessageBox.Show("Sales file is not sent to RLC server. Please contact your POS vendor.", "Transight Interface System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                            SalesTXTFail = true;
                            ErrorTracking.Log("[Business/Export] Error during export to FTP.");
                            ErrorTracking.Log(ex.Message.ToString());
                        }

                        #endregion

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
                    //s = string.Empty;
                }
                else if (SalesTXTFail == true)
                {
                    s = "FTP server Error";
                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);
                }
                else if (s.Contains("export successful."))
                {
                    s = "Export completed.";
                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);

                }
                else if (us == "Send successfully.")
                {
                    s = "Pending sent successfully.";
                    if (!Program.IsAutoMode) FrmMain.SetStatus(s);
                    Func.Log(s);
                }
                else
                {
                    s = "Sending completed.";
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
        //public static string Export(DateTime StartDate, DateTime EndDate, FormMain FrmMain)
        //{

        //    #region Creating FilePath to save your Own Selected Drive

        //    string salefilepath = Program.ExportFolder;
        //    string subfolder = @"\";
        //    string outputpath = salefilepath + subfolder;
        //    string mfilepath = Program.SentFolder;
        //    string outputMpath = mfilepath + subfolder;
        //    string filename = string.Empty;
        //    DateTime _StartDate = StartDate;
        //    DateTime _EndDate = EndDate;
        //    #endregion

        //    string tenantcode = ConfigurationManager.AppSettings["02"];
        //    //string loccode = ConfigurationManager.AppSettings["03"];
        //    //string categorycode = ConfigurationManager.AppSettings["04"];
        //    //string taxstatus = ConfigurationManager.AppSettings["07"];    
        //    string path = string.Empty;
        //    string sentfolder = string.Empty;


        //    string sftpip = ConfigurationManager.AppSettings["06"];
        //    string sftpusername = ConfigurationManager.AppSettings["07"];
        //    string sftppwd = ConfigurationManager.AppSettings["08"];
        //    //string sshkey = ConfigurationManager.AppSettings["SSHKEY"];
        //    //string _SFTPOption = ConfigurationManager.AppSettings["SFTPOption"];
        //    SessionOptions sessionOptions; //1.1.0.2
        //    try
        //    {
        //        string s = string.Empty;
        //        //get export data
        //        bool isNoDateFound = false;
        //        int exportCount = 0;
        //        DataTable tbl;
        //        DataRow dr;

        //        List<posmasterSH> posmasterSH;


        //        //FTP.SetDetails(@"ftp://" + Program.FTPIP, Program.FTPUserName, Program.FTPPassword);           
        //        try
        //        {
        //            while (_StartDate <= _EndDate)
        //            {

        //                #region write data(Mall FB)

        //                Func.Log("Date =" + _StartDate.ToString() + " - START");

        //                //Data dD = new Data();

        //                // Send the StartDate from the txtbox to SP [ Data.GetPosmasterSHList ],the get the data and store in posmasterSHList
        //                posmasterSH = Data.GetPosmasterSHList(_StartDate);

        //                // Dynamically Creating Columns in a Sales Header Table [ with below ColumnNames ]
        //                // LS - OTWC header file 
        //                DataTable table = new DataTable();
        //                table.Columns.Add("MachineID", typeof(string));
        //                table.Columns.Add("Batchid", typeof(string));
        //                table.Columns.Add("Date", typeof(string));
        //                table.Columns.Add("Hour", typeof(string));
        //                table.Columns.Add("ReceiptCount", typeof(string));
        //                table.Columns.Add("GTOSales", typeof(decimal));
        //                table.Columns.Add("GST", typeof(decimal)); //SST
        //                table.Columns.Add("Discount", typeof(decimal));
        //                table.Columns.Add("SrvChrg", typeof(decimal));
        //                table.Columns.Add("Pax", typeof(string));
        //                table.Columns.Add("Cash", typeof(decimal));
        //                table.Columns.Add("TNG", typeof(decimal));
        //                table.Columns.Add("Visa", typeof(decimal));
        //                table.Columns.Add("MasterCard", typeof(decimal));
        //                table.Columns.Add("Amex", typeof(decimal));
        //                table.Columns.Add("Voucher", typeof(decimal));
        //                table.Columns.Add("Others", typeof(decimal));
        //                table.Columns.Add("GSTreg", typeof(string));

        //                // Inserting a Records in a Row wise from the PosmasterSHList
        //                for (int i = 0; i < TransightInterface.posmasterSH.Count; i++)
        //                {
        //                    posmasterSH pMaster = posmasterSH[i];
        //                    // adding new row to the table and then Inserting records [ ex: dr=table.NewRow(); ]
        //                    dr = table.NewRow();
        //                    dr["MachineID"] = tenantcode;
        //                    dr["BatchID"] = pMaster.batchid.ToString();
        //                    string dat = pMaster.busidate.ToString("ddMMyyyy");
        //                    dr["Date"] = dat;
        //                    dr["Hour"] = pMaster.hour.ToString();
        //                    dr["ReceiptCount"] = pMaster.receiptcount.ToString();
        //                    dr["GTOSales"] = pMaster.GTOsales.ToString("N2");
        //                    dr["GST"] = pMaster.GST.ToString("N2");
        //                    dr["Discount"] = pMaster.Discount.ToString("N2");
        //                    dr["SrvChrg"] = pMaster.svcchrg.ToString("N2");
        //                    dr["Pax"] = pMaster.pax.ToString();
        //                    dr["Cash"] = pMaster.cash.ToString("N2");
        //                    dr["TNG"] = pMaster.nets.ToString("N2");
        //                    dr["Visa"] = pMaster.visa.ToString("N2");
        //                    dr["MasterCard"] = pMaster.mastercard.ToString("N2");
        //                    dr["Amex"] = pMaster.amex.ToString("N2");
        //                    dr["Voucher"] = pMaster.voucher.ToString("N2");
        //                    dr["Others"] = pMaster.others.ToString("N2");
        //                    dr["GSTreg"] = pMaster.GSTreg.ToString();
        //                    table.Rows.Add(dr);
        //                }

        //                tbl = table;

        //                StringBuilder commaDelimitedText = new StringBuilder();

        //                // Generating  salesGTO in a Drive based on your Path
        //                foreach (DataRow row in tbl.Rows)
        //                {
        //                    if (row != null)
        //                    {
        //                        string value = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}",
        //                        row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15], row[16], row[17]); // how you format is up to you (spaces, tabs, delimiter, etc)
        //                        commaDelimitedText.AppendLine(value);
        //                    }

        //                    filename = "H" + tenantcode + "_" + String.Format("{0:yyyyMMdd}", DateTime.Parse(_StartDate.ToString())) + ".txt";
        //                    path = @"" + outputpath + filename;
        //                    //sentfolder = @"" + Program.SentFolder + shyas + "" + loccode + "_SD_" + String.Format("{0:MMdd}", DateTime.Parse(StartDate.ToString())) + ".CSV";
        //                    File.WriteAllText(path, commaDelimitedText.ToString(), Encoding.UTF8);
        //                }


        //                Func.Log("Sales path:" + path);


        //                //Boolean SalesPDFFail = false;
        //                string FTPOption = AppConfig.GetConfig("FTPOption").ToString();
        //                string sshkey = AppConfig.GetConfig("SSHKEY").ToString();
        //                Boolean SalesTXTFail = false;
        //                if (FTPOption == "True" || FTPOption == "true" || FTPOption == "TRUE" || FTPOption == "Y")
        //                {

        //                    try
        //                    {
        //                        //1.1.0.2
        //                        sessionOptions = new SessionOptions
        //                        {
        //                            Protocol = Protocol.Sftp,
        //                            HostName = sftpip,
        //                            UserName = sftpusername,
        //                            Password = sftppwd,
        //                            SshHostKeyFingerprint = sshkey
        //                        };

        //                        using (Session session = new Session())
        //                        {
        //                            // Connect
        //                            session.Open(sessionOptions);

        //                            // Upload files
        //                            TransferOptions transferOptions = new TransferOptions();
        //                            transferOptions.TransferMode = TransferMode.Binary;

        //                            TransferOperationResult transferResult;
        //                            transferResult = session.PutFiles(path, "/", false, transferOptions);

        //                            // Throw on any error
        //                            transferResult.Check();

        //                            // Print results
        //                            foreach (TransferEventArgs transfer in transferResult.Transfers)
        //                            {
        //                                Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
        //                                Func.Log(path + "Upload succeeded.");
        //                            }

        //                        }
        //                    }
        //                    catch
        //                    {
        //                        System.Windows.Forms.MessageBox.Show(path + " failed to upload to SFTP.", "Transight Interface", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
        //                        Func.Log(path + " failed to upload to SFTP.");
        //                        SalesTXTFail = true;

        //                    }
        //                    if (!SalesTXTFail)
        //                    {
        //                        Func.Log(path + " File to uploaded to SFTP.");

        //                        if (File.Exists(path))
        //                        {
        //                            File.Copy(path, Program.SentFolder + @"\" + filename, true);
        //                            Func.Log("Copied file to " + Program.SentFolder);
        //                            File.Delete(path);
        //                            Func.Log("Deleting file " + Program.SentFolder);
        //                        }
        //                        else
        //                        {
        //                            Func.Log("Output file not found");
        //                        }


        //                    }
        //                }

        //                Func.Log("Date =" + _StartDate.ToString() + " - END");
        //                _StartDate = _StartDate.AddDays(1);

        //                if (tbl != null)
        //                {
        //                    if (tbl.Rows.Count > 0)
        //                    {
        //                        exportCount = 0 + 1;
        //                    }
        //                }

        //                #endregion write data(Mall FB)
        //            }
        //        }

        //        catch (ApplicationException aex) //previous date not found (same week & day)
        //        {
        //            isNoDateFound = true;
        //            s = aex.Message;
        //            if (!Program.IsAutoMode) FrmMain.SetStatus(s);
        //            Func.Log(s);
        //        }

        //        #region set message [ if Transation complet or not complet ]

        //        if (_StartDate == _EndDate && isNoDateFound)
        //            return s;

        //        if (exportCount == 0)
        //        {
        //            s = "No record(s) to export.";
        //            if (!Program.IsAutoMode) FrmMain.SetStatus(s);
        //            Func.Log(s);
        //        }
        //        else
        //        {
        //            s = "Export completed.";
        //            if (!Program.IsAutoMode) FrmMain.SetStatus(s);
        //            Func.Log(s);
        //            s = string.Empty;
        //        }


        //        return s;

        //        #endregion set message
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorTracking.Log("[Business/Export] Error during export.");
        //        ErrorTracking.Log(ex);
        //        Func.Log("Error during export.");
        //        return "ERR";
        //    }
        //    finally
        //    {
        //        System.Diagnostics.Process.Start("rasdial.exe", Program.Dialer + " /d");
        //    }
        //}
        #endregion


        private static bool WriteText(string FullName, List<posmasterSH> StoreList)
        {
            try
            {
                FileStream fs = new FileStream(FullName, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);

                foreach (posmasterSH SL in StoreList)
                {
                    //string SalesAmount = SL.getSalesAmount.ToString("0.00").Replace(".","").PadLeft(12,'^');
                    ///string TotalTransaction = SL.getTotaltransaction.ToString().PadLeft(10,'^');
                    string SalesAmount = SL.getSalesAmount.ToString("0.00").PadLeft(11, '0');
                    //string FileNo = SL.getfileNo.ToString().PadLeft(3, '0');

                    //sw.WriteLine("D"+Program.StoreNo.ToString() + "," + SL.getbusDate.ToString("dd/MM/yyyy")+ "," + SalesAmount);
                    sw.WriteLine("D" + Program.StoreNo.ToString() + SL.getbusDate.ToString("yyyyMMdd") + SalesAmount);
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

        private static bool WriteTextNew(string FullName, List<posmasterSHNew> StoreList)
        {
            try
            {
                FileStream fs = new FileStream(FullName, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                
               
                foreach (posmasterSHNew SL in StoreList)
                {
                    
                    string TenantID = Program.StoreNo.PadLeft(16,'0');
                    string TerminalNo = SL.getTerminalNo.PadLeft(16,'0');
                    string GrossSales= SL.getGrossSales.PadLeft(16,'0');
                    string TTax = SL.getTTax.PadLeft(16,'0');
                    string TVoid = SL.getTVoid.PadLeft(16,'0');
                    string CVoid = SL.getCVoid.PadLeft(16,'0');
                    string TDisc = SL.getTDisc.PadLeft(16,'0');
                    string CDisc = SL.getCDisc.PadLeft(16,'0');
                    string TRefund = SL.getTRefund.PadLeft(16,'0');
                    string CRefund = SL.getCRefund.PadLeft(16,'0');
                    string TNegAdj = SL.getTNegAdj.PadLeft(16,'0');
                    string CNegAdj = SL.getCNegAdj.PadLeft(16,'0');
                    string TServChg = SL.getTServChg.PadLeft(16,'0');
                    string PrevZCnt = SL.getPrevZCnt.PadLeft(16,'0');
                    string PrevGT = SL.getPrevGT.PadLeft(16,'0');
                    string NewZCnt = SL.getNewZCnt.PadLeft(16,'0');
                    string NewGT = SL.getNewGT.PadLeft(16,'0');
                    string BizDate = SL.getBizDate.PadLeft(16,'0');
                    string Novelty = SL.getNovelty.PadLeft(16,'0');
                    string Misc = SL.getMisc.PadLeft(16,'0');
                    string LocalTax = SL.getLocalTax.PadLeft(16,'0');
                    string TCreditSales = SL.getTCreditSales.PadLeft(16,'0');
                    string TCreditTax = SL.getTCreditTax.PadLeft(16,'0');
                    string TNVatSales = SL.getTNVatSales.PadLeft(16,'0');
                    string Pharma = SL.getPharma.PadLeft(16,'0');
                    string NPharma = SL.getNPharma.PadLeft(16,'0');
                    string TPWDDisc = SL.getTPWDDisc.PadLeft(16,'0');
                    string GrossNotSub = SL.getGrossNotSub.PadLeft(16,'0');
                    string TReprint = SL.getTReprint.PadLeft(16,'0');
                    string CReprint = SL.getCReprint.PadLeft(16,'0');

                    //string myMachineId = SL.getMachineId;


                    sw.WriteLine("01" + TenantID + System.Environment.NewLine + "02" +
        TerminalNo + System.Environment.NewLine + "03" +
        GrossSales + System.Environment.NewLine + "04" +
        TTax + System.Environment.NewLine + "05" +
        TVoid + System.Environment.NewLine + "06" +
        CVoid + System.Environment.NewLine + "07" +
        TDisc + System.Environment.NewLine + "08" +
        CDisc + System.Environment.NewLine + "09" +
        TRefund + System.Environment.NewLine + "10" +
        CRefund + System.Environment.NewLine + "11" +
        TNegAdj + System.Environment.NewLine + "12" +
        CNegAdj + System.Environment.NewLine + "13" +
        TServChg + System.Environment.NewLine + "14" +
        PrevZCnt + System.Environment.NewLine + "15" +
        PrevGT + System.Environment.NewLine + "16" +
        NewZCnt + System.Environment.NewLine + "17" +
        NewGT + System.Environment.NewLine + "18" +
        BizDate + System.Environment.NewLine + "19" +
        Novelty + System.Environment.NewLine + "20" +
        Misc + System.Environment.NewLine + "21" +
        LocalTax + System.Environment.NewLine + "22" +
        TCreditSales + System.Environment.NewLine + "23" +
        TCreditTax + System.Environment.NewLine + "24" +
        TNVatSales + System.Environment.NewLine + "25" +
        Pharma + System.Environment.NewLine + "26" +
        NPharma + System.Environment.NewLine + "27" +
        TPWDDisc + System.Environment.NewLine + "28" +
        GrossNotSub + System.Environment.NewLine + "29" +
        TReprint + System.Environment.NewLine + "30" +
        CReprint + System.Environment.NewLine);



        




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
