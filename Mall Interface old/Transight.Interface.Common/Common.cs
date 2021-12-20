using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Reflection;
using System.Globalization;
using System.IO.Compression;

namespace Transight.Interface.Common
{
    public static class Const
    {
        public static string DateTimeFormat
        {
            get { return "yyyy-MM-dd HH:mm:ss"; }
        }

        public static string DateFormat
        {
            get { return "yyyy-MM-dd"; }
        }

        public static string TimeFormat
        {
            get { return "HH:mm:ss"; }
        }
    }

    public static class Func
    {
        private static string logPath;

        public static string GetPOSRegistryConnectionString()
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Datascan");

                if (regKey.GetValue("POSServer") == null)
                    throw new Exception("Error reading registry.");

                return regKey.GetValue("POSServer").ToString().Replace("Provider=SQLOLEDB.1;", string.Empty) + ";user ID=datascan;password=DTSbsd7188228";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetHQRegistryConnectionString()
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Datascan");

                if (regKey.GetValue("HeadQ") == null)
                    throw new Exception("Error reading registry.");

                return regKey.GetValue("HeadQ").ToString().Replace("Provider=SQLOLEDB.1;", string.Empty) + ";user ID=datascan;password=dtsbsd7188228";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetECLMRegistryConnectionString()
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Datascan");

                if (regKey.GetValue("CLM") == null)
                    throw new Exception("Error reading registry.");

                return regKey.GetValue("CLM").ToString().Replace("Provider=SQLOLEDB.1;", string.Empty) + ";user ID=datascan;password=dtsbsd7188228";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool PingHost(string HostName)
        {
            Ping ping = new Ping();

            try
            {
                PingReply pingReply = ping.Send(HostName);

                if (pingReply.Status == IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch //(Exception ex)
            {
                return false;
            }
            finally
            {
                ping.Dispose();
                ping = null;
            }
        }

        public static string LogPath
        {
            set { logPath = value; }
        }

        public static void Log(string s)
        {
            try
            {
                if (logPath == null || logPath == string.Empty)
                    throw new ApplicationException("Log path not set.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);

                FileStream fs = new FileStream(logPath + @"\" + DateTime.Today.ToString(Const.DateFormat) + ".log", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToString(Const.DateTimeFormat) + " " + s);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                sw = null;
                fs.Close();
                fs.Dispose();
                fs = null;
            }
            catch //(Exception ex)
            {
                //throw ex;
            }
        }

        public static void ClearLogs(int XDays)
        {
            try
            {
                if (logPath == null || logPath == string.Empty)
                    throw new ApplicationException("Log path not set.");

                if (!Directory.Exists(logPath)) return;

                DirectoryInfo di = new DirectoryInfo(logPath);
                FileInfo[] fiList = di.GetFiles("*.log");

                List<string> toDeleteList = new List<string> { };
                string regexFileNamePattern = @"(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$";
                string fName;

                foreach (FileInfo fileInfo in fiList)
                {
                    //if file name match yyyy-MM-dd
                    fName = fileInfo.Name.Replace(".log", string.Empty);

                    if (System.Text.RegularExpressions.Regex.IsMatch(fName, regexFileNamePattern))
                    {
                        if (Convert.ToDateTime(fName) < DateTime.Today.AddDays(-XDays))
                        {
                            try
                            {
                                fileInfo.Delete();
                            }
                            catch
                            { }
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                //throw ex;
            }
        }

        public static bool IsInt32(string s)
        {
            try
            {
                int i = Int32.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDecimal(string s)
        {
            try
            {
                decimal d = Decimal.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateTime(string s, string Format)
        {
            try
            {
                CultureInfo CI = CultureInfo.CreateSpecificCulture("en-US");
                CI.DateTimeFormat.ShortDatePattern = Format;

                DateTime dt = Convert.ToDateTime(s, CI.DateTimeFormat);

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static DateTime ConvertStringToDate(string s, string Format)
        {
            CultureInfo CI = CultureInfo.CreateSpecificCulture("en-US");
            CI.DateTimeFormat.ShortDatePattern = Format;

            return Convert.ToDateTime(s, CI.DateTimeFormat);
        }

        public static string IIf(bool Expression, string TruePart, string FalsePart)
        {
            if (Expression)
                return TruePart;
            else
                return FalsePart;
        }

        public static int IIf(bool Expression, int TruePart, int FalsePart)
        {
            if (Expression)
                return TruePart;
            else
                return FalsePart;
        }

        public static decimal IIf(bool Expression, decimal TruePart, decimal FalsePart)
        {
            if (Expression)
                return TruePart;
            else
                return FalsePart;
        }
    }

    public static class ErrorTracking
    {
        private enum Tracking { ON, OFF }
        private static Tracking Track = IIf(Func.IIf(ConfigurationManager.AppSettings["ErrorTracking"] == null, string.Empty, ConfigurationManager.AppSettings["ErrorTracking"]) == "ON", Tracking.ON, Tracking.OFF);
        private static string ErrorTrackingLogPath = Path.GetFullPath(Assembly.GetExecutingAssembly().Location.Replace(Assembly.GetExecutingAssembly().GetName().Name + ".dll", string.Empty)) + @"\Logs";

        public static void Log(string s)
        {
            try
            {
                if (Track == Tracking.ON)
                {
                    if (!Directory.Exists(ErrorTrackingLogPath)) Directory.CreateDirectory(ErrorTrackingLogPath);

                    FileStream fs = new FileStream(ErrorTrackingLogPath + @"\" + DateTime.Today.ToString(Const.DateFormat) + "Err.log", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(DateTime.Now.ToString(Const.DateTimeFormat) + " " + s);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                    sw = null;
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
            }
            catch { }
        }

        public static void Log(Exception ex)
        {
            Log(ex.ToString());
        }

        private static Tracking IIf(bool Expression, Tracking TruePart, Tracking FalsePart)
        {
            if (Expression)
                return TruePart;
            else
                return FalsePart;
        }
    }
}
