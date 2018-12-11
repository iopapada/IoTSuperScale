using System;
using Windows.Storage;

namespace IoTSuperScale.IoTDB
{
    public sealed class AppSettings
    {
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        static StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        #region general properties
        private static int GetIntSetting(string key, int defaultValue)
        {
            if (localSettings.Values[key] == null)
            {
                localSettings.Values[key] = defaultValue;
            }
            return Convert.ToInt32(localSettings.Values[key]);
        }
        private static bool GetIntSetting(string key, bool defaultValue)
        {
            if (localSettings.Values[key] == null)
            {
                localSettings.Values[key] = defaultValue;
            }
            return Convert.ToBoolean(localSettings.Values[key]);
        }
        public static int ClockPinNumber
        {
            get
            {
                return GetIntSetting("clockPinNumber", 23);
            }
            set
            {
                localSettings.Values["clockPinNumber"] = value;
            }
        }
        public static int DataPinNumber
        {
            get
            {
                return GetIntSetting("DataPinNumber", 24);
            }
            set
            {
                localSettings.Values["DataPinNumber"] = value;
            }
        }
        public static int ScaleTimer
        {
            get
            {
                return GetIntSetting("ScaleTimer", 500);
            }
            set
            {
                localSettings.Values["ScaleTimer"] = value;
            }
        }
        public static double CalibrationHalfKilo
        {
            get
            {
                string key = "CalibrationHalfKilo";
                double defaultValue = 1;
                if (localSettings.Values[key] == null)
                {
                    localSettings.Values[key] = defaultValue;
                }
                return Convert.ToDouble(localSettings.Values[key]);
            }
            set
            {
                localSettings.Values["CalibrationHalfKilo"] = value;
            }
        }
        public static double CalibrationKilo
        {
            get
            {
                string key = "CalibrationKilo";
                double defaultValue = 1;
                if (localSettings.Values[key] == null)
                {
                    localSettings.Values[key] = defaultValue;
                }
                return Convert.ToDouble(localSettings.Values[key]);
            }
            set
            {
                localSettings.Values["CalibrationKilo"] = value;
            }
        }
        public static int OffsetZero
        {
            get
            {
                return GetIntSetting("OffsetZero", 0);
            }
            set
            {
                localSettings.Values["OffsetZero"] = value;
            }
        }
        public static int MinZero
        {
            get
            {
                return GetIntSetting("MinZero", 999999);
            }
            set
            {
                localSettings.Values["MinZero"] = value;
            }
        }
        public static int MaxZero
        {
            get
            {
                return GetIntSetting("MaxZero", 0);
            }
            set
            {
                localSettings.Values["MaxZero"] = value;
            }
        }
        public static string LeadingUnit
        {
            get
            {
                return (string)localSettings.Values["LeadingUnit"] ?? "";
            }
            set
            {
                localSettings.Values["LeadingUnit"] = value;
            }
        }
        public static string TrailingUnit
        {
            get
            {
                return (string)localSettings.Values["TrailingUnit"] ?? " kg";
            }
            set
            {
                localSettings.Values["TrailingUnit"] = value;
            }
        }
        public static int Precision
        {
            get
            {
                return GetIntSetting("Precision", 2);
            }
            set
            {
                localSettings.Values["Precision"] = value;
            }
        }
        public static string IpConfig
        {
            get
            {
                return (string)localSettings.Values["IpConfig"] ?? "192.168.1.255";
            }
            set
            {
                localSettings.Values["IpConfig"] = value;
            }
        }
        public static string PortConfig
        {
            get
            {
                return (string)localSettings.Values["PortConfig"] ?? "21200";
            }
            set
            {
                localSettings.Values["PortConfig"] = value;
            }
        }
        public static string IpERPServerConfig
        {
            get
            {
                return (string)localSettings.Values["IpERPServerConfig"] ?? "192.168.1.1";
            }
            set
            {
                localSettings.Values["IpERPServerConfig"] = value;
            }
        }
        public static string PortERPServerConfig
        {
            get
            {
                return (string)localSettings.Values["PortERPServerConfig"] ?? "1433";
            }
            set
            {
                localSettings.Values["PortERPServerConfig"] = value;
            }
        }
        public static string ERPDBInstance
        {
            get
            {
                return (string)localSettings.Values["ERPDBInstance"] ?? "";
            }
            set
            {
                localSettings.Values["ERPDBInstance"] = value;
            }
        }
        public static string MRPDBInstance
        {
            get
            {
                return (string)localSettings.Values["MRPDBInstance"] ?? "";
            }
            set
            {
                localSettings.Values["MRPDBInstance"] = value;
            }
        }
        public static string ERPDBname
        {
            get
            {
                return (string)localSettings.Values["ERPDBname"] ?? "";
            }
            set
            {
                localSettings.Values["ERPDBname"] = value;
            }
        }
        public static string MRPDBname
        {
            get
            {
                return (string)localSettings.Values["MRPDBname"] ?? "";
            }
            set
            {
                localSettings.Values["MRPDBname"] = value;
            }
        }
        public static string DBuser
        {
            get
            {
                return (string)localSettings.Values["DBuser"] ?? "sa";
            }
            set
            {
                localSettings.Values["DBuser"] = value;
            }
        }
        public static string DBpass
        {
            get
            {
                return (string)localSettings.Values["DBpass"] ?? "";
            }
            set
            {
                localSettings.Values["DBpass"] = value;
            }
        }
        public static string ERPDBConnectionString
        {
            get
            {
                return "Data source = " + IpERPServerConfig + "\\" + ERPDBInstance + "," + PortERPServerConfig + ";Initial Catalog=" + ERPDBname + ";User ID=" + DBuser + ";Password = " + DBpass + ";";
            }
            set
            {
                localSettings.Values["ERPDBConnectionString"] = value;
            }
        }
        public static string MRPDBConnectionString
        {
            get
            {
                return "Data source = " + IpERPServerConfig + "\\" + MRPDBInstance + "," + PortERPServerConfig + ";Initial Catalog=" + MRPDBname + ";User ID=" + DBuser + ";Password = " + DBpass + ";";
            }
            set
            {
                localSettings.Values["MRPDBConnectionString"] = value;
            }
        }
        public static bool BroadcastPcksConfig
        {
            get
            {
                return GetIntSetting("BroadcastPcksConfig", false);
            }
            set
            {
                localSettings.Values["BroadcastPcksConfig"] = value;
            }
        }
        public static string ScaleName
        {
            get
            {
                return (string)localSettings.Values["ScaleName"] ?? "Scale A";
            }
            set
            {
                localSettings.Values["ScaleName"] = value;
            }
        }
        public static int LCcapacity
        {
            get
            {
                return GetIntSetting("LCcapacity", 300);
            }
            set
            {
                localSettings.Values["LCcapacity"] = value;
            }
        }
        public static int ScreenSaverMins
        {
            get
            {
                return GetIntSetting("ScreenSaverMins", 0);
            }
            set
            {
                localSettings.Values["ScreenSaverMins"] = value;
            }
        }
        public static string LangConfig
        {
            get
            {
                return (string)localSettings.Values["LangConfig"] ?? "GR";
            }
            set
            {
                localSettings.Values["LangConfig"] = value;
            }
        }
        #endregion

        #region softaware properties
        public static string Copyrightattr
        {
            get
            {
                return (string)localSettings.Values["Copyrightattr"] ?? "";
            }
            set
            {
                localSettings.Values["Copyrightattr"] = value;
            }
        }
        public static string Companyattr
        {
            get
            {
                return (string)localSettings.Values["Companyattr"] ?? "";
            }
            set
            {
                localSettings.Values["Companyattr"] = value;
            }
        }
        public static string Trademarkattr
        {
            get
            {
                return (string)localSettings.Values["Trademarkattr"] ?? "";
            }
            set
            {
                localSettings.Values["Trademarkattr"] = value;
            }
        }
        public static string Version
        {
            get
            {
                return (string)localSettings.Values["Version"] ?? "";
            }
            set
            {
                localSettings.Values["Version"] = value;
            }
        }
        #endregion

        #region printer properties
        public static string IpPrinterConfig
        {
            get
            {
                return (string)localSettings.Values["IpPrinterConfig"] ?? "192.168.1.100";
            }
            set
            {
                localSettings.Values["IpPrinterConfig"] = value;
            }
        }
        public static string PortPrinterConfig
        {
            get
            {
                return (string)localSettings.Values["PortPrinterConfig"] ?? "9100";
            }
            set
            {
                localSettings.Values["PortPrinterConfig"] = value;
            }
        }
        public static int SumPrints
        {
            get
            {
                return GetIntSetting("SumPrints", 1);
            }
            set
            {
                localSettings.Values["SumPrints"] = value;
            }
        }
        public static int CopiesPrints
        {
            get
            {
                return GetIntSetting("CopiesPrints", 1);
            }
            set
            {
                localSettings.Values["CopiesPrints"] = value;
            }
        }
        public static int PalletsNum
        {
            get
            {
                return GetIntSetting("PalletsNum", 1);
            }
            set
            {
                localSettings.Values["PalletsNum"] = value;
            }
        }
        #endregion
    }
}
