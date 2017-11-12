using System;
using Windows.Storage;

namespace IoTSuperScale.IoTDB
{
    public sealed class AppSettings
    {
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        
        //should figure out how to refactor these public properties.
        //these prevent multiple scales from the same application...
        //should store the values where they're easier to read from source control
        #region general properties
        private static int getIntSetting(string key, int defaultValue)
        {
            if (localSettings.Values[key] == null)
            {
                localSettings.Values[key] = defaultValue;
            }
            return Convert.ToInt32(localSettings.Values[key]);
        }
        private static bool getIntSetting(string key, bool defaultValue)
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
                return getIntSetting("clockPinNumber", 23);
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
                return getIntSetting("DataPinNumber", 24);
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
                return getIntSetting("ScaleTimer", 500);
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
                return getIntSetting("OffsetZero", 0);
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
                return getIntSetting("MinZero", 999999);
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
                return getIntSetting("MaxZero", 0);
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
                return getIntSetting("Precision", 2);
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
        public static bool BroadcastPcksConfig
        {
            get
            {
                return getIntSetting("BroadcastPcksConfig", false);
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
                return getIntSetting("LCcapacity", 300);
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
                return getIntSetting("ScreenSaverMins", 15);
            }
            set
            {
                localSettings.Values["ScreenSaverMins"] = value;
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
                return getIntSetting("SumPrints", 1);
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
                return getIntSetting("CopiesPrints", 1);
            }
            set
            {
                localSettings.Values["CopiesPrints"] = value;
            }
        }
        #endregion

        #region material properties
        public static string NewLot
        {
            get
            {
                return (string)localSettings.Values["NewLot"] ?? "--";
            }
            set
            {
                localSettings.Values["NewLot"] = value;
            }
        }
        #endregion
    }
}
