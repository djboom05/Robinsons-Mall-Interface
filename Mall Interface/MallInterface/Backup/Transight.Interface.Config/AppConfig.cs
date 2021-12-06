using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using Transight.Interface.Common;
using System.Text.RegularExpressions;

namespace Transight.Interface.Config
{
    public enum TransightDBOption { Registry, ConnString }

    public static class AppConfig
    {
        private static string appPath = Path.GetFullPath(Assembly.GetExecutingAssembly().Location.Replace(Assembly.GetExecutingAssembly().GetName().Name + ".dll", string.Empty));

        //default settings
        private static TransightDBOption dbOption;
        private static string dbServer;
        private static string dbName;
        private static string logPath;
        private static int logCleanUp; //in days
        private static int businessDateOffset; //in days        
        private static int configCount;
        private static int configTitleCount; //for screen modification

        //other configs

        public static TransightDBOption DBOption
        {
            get
            {
                return dbOption;
            }
        }

        public static string DBServer
        {
            get
            {
                return dbServer;
            }
        }

        public static string DBName
        {
            get
            {
                return dbName;
            }
        }

        public static string LogPath
        {
            get
            {
                return logPath;
            }
        }

        public static int LogCleanUp
        {
            get
            {
                return logCleanUp;
            }
        }

        public static int BusinessDateOffset
        {
            get
            {
                return businessDateOffset;
            }
        }

        public static int ConfigCount
        {
            get
            {
                return configCount;
            }
        }

        public static int ConfigTitleCount
        {
            get
            {
                return configTitleCount;
            }
        }

        public static List<XMLConfig> ConfigList = new List<XMLConfig> { };

        public static void Load(bool ShowMessage)
        {
            try
            {
                string sConfig = ConfigurationManager.AppSettings["DBOption"];
                if (sConfig == null || !(sConfig.Trim() == "R" || sConfig.Trim() == "S"))
                    throw new ApplicationException("Error getting Transight DB option");

                if (sConfig.Trim() == "R")
                    dbOption = TransightDBOption.Registry;
                else
                    dbOption = TransightDBOption.ConnString;

                sConfig = ConfigurationManager.AppSettings["DBServer"];
                if (sConfig == null) throw new ApplicationException("Error getting Transight DB server");
                dbServer = sConfig.Trim();

                sConfig = ConfigurationManager.AppSettings["DBName"];
                if (sConfig == null) throw new ApplicationException("Error getting Transight DB name");
                dbName = sConfig.Trim();

                sConfig = ConfigurationManager.AppSettings["LogPath"];
                if (sConfig == null) throw new ApplicationException("Error getting log path");

                if (sConfig.Trim() == string.Empty)
                {
                    if (ShowMessage) MessageBox.Show("Log path not set. Interface will use application path.", "App Config", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    logPath = appPath + @"Logs";
                    if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
                }
                else if (!Directory.Exists(sConfig.Trim()))
                {
                    if (ShowMessage) MessageBox.Show("Log path not found. Interface will use application path.", "App Config", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    logPath = appPath + @"Logs";
                    if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
                }
                else
                    logPath = sConfig;

                sConfig = ConfigurationManager.AppSettings["LogCleanUp"];
                if (sConfig == null || !Func.IsInt32(sConfig.Trim())) throw new ApplicationException("Error getting log clean up");
                logCleanUp = Int32.Parse(sConfig.Trim());

                sConfig = ConfigurationManager.AppSettings["BusinessDateOffset"];
                if (sConfig == null || !Func.IsInt32(sConfig.Trim())) throw new ApplicationException("Error getting business date offset");
                businessDateOffset = Int32.Parse(sConfig.Trim());

                sConfig = ConfigurationManager.AppSettings["ConfigCount"];
                if (sConfig == null || !Func.IsInt32(sConfig.Trim())) throw new ApplicationException("Error getting config count");
                configCount = Int32.Parse(sConfig.Trim());

                if (configCount < 0 || configCount >= 100)
                    throw new ApplicationException("Invalid range for ConfigCount");

                string[] settings;

                for (int i = 1; i <= configCount; i++)
                {
                    //get setting string
                    sConfig = ConfigurationManager.AppSettings["C" + i.ToString().PadLeft(2, '0')];
                    if (sConfig == null)
                        throw new ApplicationException("Error getting setting string for [C" + i.ToString().PadLeft(2, '0') + "]");

                    if (sConfig.Split('#').Length != 11)
                        throw new ApplicationException("Invalid setting string for [C" + i.ToString().PadLeft(2, '0') + "]");

                    settings = sConfig.Split('#');

                    sConfig = ConfigurationManager.AppSettings[i.ToString().PadLeft(2, '0')];
                    if (sConfig == null)
                        throw new ApplicationException("Error getting setting value for [" + i.ToString().PadLeft(2, '0') + "]");

                    try
                    {
                        //add to config list
                        ConfigList.Add(new XMLConfig(i, settings, sConfig));
                    }
                    catch (Exception ex)
                    {
                        ErrorTracking.Log("[Error loading config [C" + i.ToString().PadLeft(2, '0') + "]");
                        throw ex;
                    }
                }

                //for display purpose
                configTitleCount = 0;

                foreach (XMLConfig xmlConfig in ConfigList)
                {
                    if (xmlConfig.CtrlSetting.Type == ControlType.None)
                        configTitleCount += 1;
                }
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                ErrorTracking.Log(ex);
                throw ex;
            }
        }

        public static bool SaveAll(bool IsRegistry, string NewDBServer, string NewDBName, string NewLogPath, int NewLogCleanUp, int NewBusinessDateOffset, List<XMLConfig> NewXMLConfigList)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (IsRegistry)
                    config.AppSettings.Settings["DBOption"].Value = "R";
                else
                    config.AppSettings.Settings["DBOption"].Value = "S";

                config.AppSettings.Settings["DBServer"].Value = NewDBServer;
                config.AppSettings.Settings["DBName"].Value = NewDBName;
                config.AppSettings.Settings["LogPath"].Value = NewLogPath;
                config.AppSettings.Settings["LogCleanUp"].Value = NewLogCleanUp.ToString();
                config.AppSettings.Settings["BusinessDateOffset"].Value = NewBusinessDateOffset.ToString();

                //set new value
                if (IsRegistry)
                    dbOption = TransightDBOption.Registry;
                else
                    dbOption = TransightDBOption.ConnString;

                dbServer = NewDBServer;
                dbName = NewDBName;
                logPath = NewLogPath;
                logCleanUp = NewLogCleanUp;
                businessDateOffset = NewBusinessDateOffset;

                //set other config
                foreach (XMLConfig newConfig in NewXMLConfigList)
                {
                    if (newConfig.Changed)
                    {
                        switch (newConfig.CtrlSetting.Type)
                        {
                            case ControlType.TxtBox:
                            case ControlType.Path:
                            case ControlType.File:
                            case ControlType.DropDown:
                            case ControlType.RdButton:
                                config.AppSettings.Settings[newConfig.Index.ToString().PadLeft(2, '0')].Value = newConfig.StringValue;
                                break;

                            case ControlType.DtTmPicker:
                                config.AppSettings.Settings[newConfig.Index.ToString().PadLeft(2, '0')].Value = newConfig.DateTimeValue.ToString(newConfig.CtrlSetting.Desc);
                                break;

                            case ControlType.NmrcUpDown:
                                config.AppSettings.Settings[newConfig.Index.ToString().PadLeft(2, '0')].Value = newConfig.IntValue.ToString();
                                break;

                            case ControlType.Decimal:
                                config.AppSettings.Settings[newConfig.Index.ToString().PadLeft(2, '0')].Value = newConfig.DecimalValue.ToString("0.".PadRight(newConfig.CtrlSetting.Size + 2, '0'));
                                break;

                            case ControlType.ChkBox:
                                config.AppSettings.Settings[newConfig.Index.ToString().PadLeft(2, '0')].Value = Func.IIf(newConfig.BoolValue, "Y", "N");
                                break;
                        }

                        newConfig.Changed = false;
                    }
                }

                //set new value for other config
                ConfigList = null;
                ConfigList = NewXMLConfigList;

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                return true;
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[AppConfig/SaveAll] Error saving configs.");
                ErrorTracking.Log(ex);
                return false;
            }
        }

        public static bool Save(string Name, object value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                //set & save config
                foreach (XMLConfig xmlConfig in ConfigList)
                {
                    if (xmlConfig.Name == Name)
                    {
                        switch (xmlConfig.CtrlSetting.Type)
                        {
                            case ControlType.TxtBox:
                            case ControlType.Path:
                            case ControlType.File:
                            case ControlType.DropDown:
                            case ControlType.RdButton:
                                xmlConfig.SetValue(Convert.ToString(value));
                                config.AppSettings.Settings[xmlConfig.Index.ToString().PadLeft(2, '0')].Value = xmlConfig.StringValue;
                                break;

                            case ControlType.DtTmPicker:
                                xmlConfig.SetValue(Convert.ToDateTime(value));
                                config.AppSettings.Settings[xmlConfig.Index.ToString().PadLeft(2, '0')].Value = xmlConfig.DateTimeValue.ToString(xmlConfig.CtrlSetting.Desc);
                                break;

                            case ControlType.NmrcUpDown:
                                xmlConfig.SetValue(Convert.ToInt32(value));
                                config.AppSettings.Settings[xmlConfig.Index.ToString().PadLeft(2, '0')].Value = xmlConfig.IntValue.ToString();
                                break;

                            case ControlType.Decimal:
                                xmlConfig.SetValue(Convert.ToDecimal(value));
                                config.AppSettings.Settings[xmlConfig.Index.ToString().PadLeft(2, '0')].Value = xmlConfig.DecimalValue.ToString("0.".PadRight(xmlConfig.CtrlSetting.Size + 2, '0'));
                                break;

                            case ControlType.ChkBox:
                                xmlConfig.SetValue(Convert.ToBoolean(value));
                                config.AppSettings.Settings[xmlConfig.Index.ToString().PadLeft(2, '0')].Value = Func.IIf(xmlConfig.BoolValue, "Y", "N");
                                break;
                        }
                    }
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                return true;
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[AppConfig/Save] Error saving config [" + Name + "].");
                ErrorTracking.Log(ex);
                return false;
            }
        }

        public static object GetConfig(int Index)
        {
            object obj = new object();

            foreach (XMLConfig config in ConfigList)
            {
                if (config.Index == Index)
                {
                    switch (config.CtrlSetting.Type)
                    {
                        case ControlType.TxtBox:
                        case ControlType.Path:
                        case ControlType.File:
                        case ControlType.DropDown:
                        case ControlType.RdButton:
                            obj = config.StringValue;
                            break;

                        case ControlType.DtTmPicker:
                            obj = config.DateTimeValue;
                            break;

                        case ControlType.NmrcUpDown:
                            obj = config.IntValue;
                            break;

                        case ControlType.Decimal:
                            obj = config.DecimalValue;
                            break;

                        case ControlType.ChkBox:
                            obj = config.BoolValue;
                            break;
                    }
                }
            }

            return obj;
        }

        public static object GetConfig(string Name)
        {
            object obj = new object();

            foreach (XMLConfig config in ConfigList)
            {
                if (config.Name == Name)
                {
                    switch (config.CtrlSetting.Type)
                    {
                        case ControlType.TxtBox:
                        case ControlType.Path:
                        case ControlType.File:
                        case ControlType.DropDown:
                        case ControlType.RdButton:
                            obj = config.StringValue;
                            break;

                        case ControlType.DtTmPicker:
                            obj = config.DateTimeValue;
                            break;

                        case ControlType.NmrcUpDown:
                            obj = config.IntValue;
                            break;

                        case ControlType.Decimal:
                            obj = config.DecimalValue;
                            break;

                        case ControlType.ChkBox:
                            obj = config.BoolValue;
                            break;
                    }
                }
            }

            return obj;
        }
    }

    public class XMLConfig
    {
        private int index;
        private string name;
        private string stringValue;
        private int intValue;
        private decimal decimalValue;
        private DateTime dateTimeValue;
        private bool boolValue;

        public ControlSetting CtrlSetting;
        public bool Changed;

        public int Index
        {
            get
            {
                return index;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string StringValue
        {
            get
            {
                return stringValue;
            }
        }

        public int IntValue
        {
            get
            {
                return intValue;
            }
        }

        public decimal DecimalValue
        {
            get
            {
                return decimalValue;
            }
        }

        public DateTime DateTimeValue
        {
            get
            {
                return dateTimeValue;
            }
        }

        public bool BoolValue
        {
            get
            {
                return boolValue;
            }
        }

        public XMLConfig(int Index, string[] Settings, string Value)
        {
            const string NOW = "Now";
            const string MIN = "Min";
            const string MAX = "Max";

            index = Index;
            name = Settings[1];

            CtrlSetting = new ControlSetting(Settings);

            switch (CtrlSetting.Type)
            {
                case ControlType.TxtBox:
                case ControlType.Path:
                case ControlType.File:
                case ControlType.DropDown:
                case ControlType.RdButton:
                    stringValue = Value;
                    break;

                case ControlType.DtTmPicker:
                    #region DateTimePicker
                    if (Value != string.Empty)
                    {
                        if (Func.IsDateTime(Value, CtrlSetting.Desc))
                            dateTimeValue = Func.ConvertStringToDate(Value, CtrlSetting.Desc);

                        if (dateTimeValue < CtrlSetting.MinDate)
                            dateTimeValue = CtrlSetting.MinDate;
                        else if (dateTimeValue > CtrlSetting.MaxDate)
                            dateTimeValue = CtrlSetting.MaxDate;
                    }
                    else
                    {
                        if (Settings[4] == MIN)
                            dateTimeValue = CtrlSetting.MinDate;
                        else if (Settings[4] == MAX)
                            dateTimeValue = CtrlSetting.MaxDate;
                        else if (Settings[4] == NOW)
                        {
                            if (DateTime.Today < CtrlSetting.MinDate)
                                dateTimeValue = CtrlSetting.MinDate;
                            else if (DateTime.Today > CtrlSetting.MaxDate)
                                dateTimeValue = CtrlSetting.MaxDate;
                            else
                                dateTimeValue = DateTime.Today;
                        }
                    }
                    #endregion
                    break;

                case ControlType.NmrcUpDown:
                    if (Func.IsInt32(Value))
                    {
                        intValue = Convert.ToInt32(Value);

                        if (intValue < CtrlSetting.MinValue)
                            intValue = Convert.ToInt32(CtrlSetting.MinValue);
                        else if (intValue > CtrlSetting.MaxValue)
                            intValue = Convert.ToInt32(CtrlSetting.MaxValue);
                    }
                    else
                        intValue = Convert.ToInt32(CtrlSetting.MinValue);
                    break;

                case ControlType.Decimal:
                    if (Func.IsDecimal(Value))
                    {
                        decimalValue = Convert.ToDecimal(Value);

                        if (decimalValue < CtrlSetting.MinValue)
                            decimalValue = CtrlSetting.MinValue;
                        else if (decimalValue > CtrlSetting.MaxValue)
                            decimalValue = CtrlSetting.MaxValue;
                    }
                    else
                        decimalValue = CtrlSetting.MinValue;
                    break;

                case ControlType.ChkBox:
                    if (Value == "Y")
                        boolValue = true;
                    else
                        boolValue = false;
                    break;
            }
        }

        public void SetValue(string s)
        {
            switch (CtrlSetting.Type)
            {
                case ControlType.TxtBox:
                case ControlType.Path:
                case ControlType.File:
                case ControlType.DropDown:
                case ControlType.RdButton:
                    stringValue = s;
                    break;
            }
        }

        public void SetValue(DateTime dt)
        {
            switch (CtrlSetting.Type)
            {
                case ControlType.DtTmPicker:
                    dateTimeValue = dt;
                    break;
            }
        }

        public void SetValue(int i)
        {
            switch (CtrlSetting.Type)
            {
                case ControlType.NmrcUpDown:
                    intValue = i;
                    break;
            }
        }

        public void SetValue(decimal d)
        {
            switch (CtrlSetting.Type)
            {
                case ControlType.Decimal:
                    decimalValue = d;
                    break;
            }
        }

        public void SetValue(bool b)
        {
            switch (CtrlSetting.Type)
            {
                case ControlType.ChkBox:
                    boolValue = b;
                    break;
            }
        }
    }

    public enum ControlType { TxtBox, DtTmPicker, NmrcUpDown, Decimal, ChkBox, RdButton, None, File, Path, DropDown }

    public class ControlSetting
    {
        private ControlType type;
        private string caption;
        private List<string> values;
        private string defaultValue;
        private int size;
        private string regEx;
        private string desc;
        private DateTime minDate;
        private DateTime maxDate;
        private decimal minValue;
        private decimal maxValue;
        private decimal interval;

        public ControlType Type
        {
            get
            {
                return type;
            }
        }

        public string Caption
        {
            get
            {
                return caption;
            }
        }

        public List<string> Values
        {
            get
            {
                return values;
            }
        }

        public string Default
        {
            get
            {
                return defaultValue;
            }
        }

        public int Size
        {
            get
            {
                return size;
            }
        }

        public string RegEx
        {
            get
            {
                return regEx;
            }
        }

        public string Desc
        {
            get
            {
                return desc;
            }
        }

        public DateTime MinDate
        {
            get
            {
                return minDate;
            }
        }

        public DateTime MaxDate
        {
            get
            {
                return maxDate;
            }
        }

        public decimal MinValue
        {
            get
            {
                return minValue;
            }
        }

        public decimal MaxValue
        {
            get
            {
                return maxValue;
            }
        }

        public decimal Interval
        {
            get
            {
                return interval;
            }
        }

        public ControlSetting(string[] Settings)
        {
            const string NOW = "Now";

            switch (Settings[0])
            {
                case "L":
                    type = ControlType.None;
                    break;

                case "T":
                    type = ControlType.TxtBox;
                    defaultValue = Settings[4];
                    size = Convert.ToInt32(Settings[8]);

                    if (Settings[9] != string.Empty)
                        regEx = Settings[9];

                    if (Settings[10] != string.Empty)
                        desc = Settings[10];
                    break;

                case "P":
                    type = ControlType.Path;

                    if (Settings[10] != string.Empty)
                        desc = Settings[10];
                    break;

                case "F":
                    type = ControlType.File;
                    defaultValue = Settings[4];

                    if (Settings[10] != string.Empty)
                        desc = Settings[10];
                    break;

                case "D":
                    #region DateTimePicker
                    type = ControlType.DtTmPicker;

                    if (Settings[5] == NOW) //min
                        minDate = DateTime.Today;
                    else
                    {
                        if (Func.IsDateTime(Settings[5], Settings[10]))
                            minDate = Func.ConvertStringToDate(Settings[5], Settings[10]);
                        else
                        {
                            if (Regex.IsMatch(Settings[5], @"[+-][\d][DMY]"))
                            {
                                int i = Convert.ToInt32(Settings[5].Substring(1, Settings[5].Length - 2));

                                if (Settings[5].Substring(0, 1) == "-")
                                    i *= -i;

                                //last char
                                switch (Settings[5].Substring(Settings[5].Length - 1, 1))
                                {
                                    case "D":
                                        minDate = DateTime.Today.AddDays(i);
                                        break;

                                    case "M":
                                        minDate = DateTime.Today.AddMonths(i);
                                        break;

                                    case "Y":
                                        minDate = DateTime.Today.AddYears(i);
                                        break;
                                }
                            }
                        }
                    }

                    if (Settings[6] == NOW) //max
                        maxDate = DateTime.Today;
                    else
                    {
                        if (Func.IsDateTime(Settings[6], Settings[10]))
                            maxDate = Func.ConvertStringToDate(Settings[6], Settings[10]);
                        else
                        {
                            if (Regex.IsMatch(Settings[6], @"[+-][\d][DMY]"))
                            {
                                int i = Convert.ToInt32(Settings[6].Substring(1, Settings[6].Length - 2));

                                if (Settings[6].Substring(0, 1) == "-")
                                    i *= -i;

                                //last char
                                switch (Settings[6].Substring(Settings[6].Length - 1, 1))
                                {
                                    case "D":
                                        maxDate = DateTime.Today.AddDays(i);
                                        break;

                                    case "M":
                                        maxDate = DateTime.Today.AddMonths(i);
                                        break;

                                    case "Y":
                                        maxDate = DateTime.Today.AddYears(i);
                                        break;
                                }
                            }
                        }
                    }

                    //date format
                    desc = Settings[10];
                    #endregion
                    break;

                case "I":
                    type = ControlType.NmrcUpDown;

                    if (Func.IsInt32(Settings[5]))
                        minValue = Convert.ToInt32(Settings[5]);

                    if (Func.IsInt32(Settings[6]))
                        maxValue = Convert.ToInt32(Settings[6]);

                    if (Func.IsInt32(Settings[7]))
                        interval = Convert.ToInt32(Settings[7]);

                    if (Settings[10] != string.Empty)
                        desc = Settings[10];
                    break;

                case "M":
                    type = ControlType.Decimal;

                    if (Func.IsDecimal(Settings[5]))
                        minValue = Convert.ToDecimal(Settings[5]);

                    if (Func.IsDecimal(Settings[6]))
                        maxValue = Convert.ToDecimal(Settings[6]);

                    if (Func.IsInt32(Settings[8]))
                        size = Convert.ToInt32(Settings[8]);

                    if (Settings[10] != string.Empty)
                        desc = Settings[10];
                    break;

                case "C":
                    type = ControlType.ChkBox;

                    if (Settings[10] != string.Empty)
                        desc = Settings[10];
                    break;

                case "W":
                    type = ControlType.DropDown;

                    values = new List<string> { };

                    string[] v = Settings[3].Split('|');

                    for (int i = 0; i < v.Length; i++)
                    {
                        values.Add(v[i]);
                    }
                    break;

                case "R":
                    type = ControlType.RdButton;

                    values = new List<string> { };
                    values.Add(Settings[3].Split('|')[0]);
                    values.Add(Settings[3].Split('|')[1]);

                    break;
            }

            caption = Settings[2];
        }
    }
}