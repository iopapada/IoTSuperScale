using IoTSuperScale.IoTDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace IoTSuperScale.IoTCore
{
    public sealed class Scale
    {
        //helper variables
        private HX711 device; 
        public string zeroPointString;
        public double zeroPoint;
        public double finalDigitVal;
        public string finalStringVal;
        public int lastOutput;
        private double lastFV = -1;
        private double bLastFV = -1;
        private int dbgTheVal;

        public Scale()
        {
            Zero();
            zeroPointString = CreateZeroPoint();
            zeroPoint = Double.Parse(zeroPointString);
            zeroPointString = zeroPointString + AppSettings.TrailingUnit;
        }

        public string CreateZeroPoint()
        {
            string zeroP = "0";
            if (AppSettings.Precision > 0)
                zeroP += ".";
            for (int i = 0; i < AppSettings.Precision; i++) {
                zeroP += "0";
            }
            return zeroP;
        }
        public string GetReading()
        {
            try
            {
                finalStringVal = zeroPointString;
                //Capture voltage only one time...
                lastOutput = _GetOutputData();
                double dataOffsetDiff = lastOutput - AppSettings.OffsetZero;
                if (AppSettings.CalibrationKilo != 1)
                    finalDigitVal = dataOffsetDiff / AppSettings.CalibrationKilo;
                else if (AppSettings.CalibrationHalfKilo != 1)
                    finalDigitVal = dataOffsetDiff / AppSettings.CalibrationHalfKilo;
                
                finalStringVal = AppSettings.LeadingUnit + Math.Round(finalDigitVal, AppSettings.Precision).ToString() + AppSettings.TrailingUnit;
                //Auto correct offset...
                if (finalDigitVal < 0.05 && finalDigitVal > -0.05)
                {
                    AppSettings.OffsetZero = lastOutput;
                    finalStringVal = zeroPointString;
                }
                //Broadcast scale value only when we have real weigth and only three attempt for zero values
                double dbgTemp = Convert.ToDouble(String.Format("{0:" + zeroPoint.ToString() + "}", finalDigitVal));
                if (!(dbgTemp == 0 && bLastFV == 0 && lastFV == 0) && AppSettings.BroadcastPcksConfig)
                {
                    BroadcastScaleVal(finalStringVal);
                }
                bLastFV = lastFV;
                lastFV = dbgTemp;
                return finalStringVal;
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, "Error on reading scale values");
                return finalStringVal;
            }
        }
        private void BroadcastScaleVal(string message)
        {
            try
            {
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                sender.EnableBroadcast = true;
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(AppSettings.IpConfig), Int32.Parse(AppSettings.PortConfig));
                byte[] data = Encoding.ASCII.GetBytes(message);
                sender.SendTo(data, iep);
                sender.Dispose();
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, "Exception in Broadcast function 2");
            }
        }
        public void Zero()
        {
            Prework();
            AppSettings.OffsetZero = _GetOutputData();
            AppSettings.MaxZero = AppSettings.OffsetZero;
            AppSettings.MinZero = AppSettings.OffsetZero;
        }
        public string ZeroWithoutPrework()
        {
            AppSettings.OffsetZero = _GetOutputData();
            AppSettings.MaxZero = AppSettings.OffsetZero;
            AppSettings.MinZero = AppSettings.OffsetZero;
            return zeroPointString;
        }
        public double Calibrate(double weight)
        {
            int temp = 0;
            if (weight == 0)
                weight = 1;
                temp = _GetOutputData() - AppSettings.OffsetZero;
            return temp / weight;
        }
        private void Prework()
        {
            List<int> suggestedVals = new List<int>(10);
            for (int i = 0; i < 10; i++)
            {
                var t = Task.Run(async delegate
                {
                    await Task.Delay(20);
                    return 42;
                });
                t.Wait();
                int temp = _GetOutputData();
                string tempString = temp.ToString();
                suggestedVals.Add(Int32.Parse(tempString.Substring(0, 2)));
            }
            dbgTheVal = suggestedVals.GroupBy(s => s).OrderByDescending(s => s.Count()).First().Key;
        }
        private void InitializeDevice()
        {
            GpioPin dataPin;
            GpioPin clockPin;
            if (device == null)
            {
                GpioController controller = GpioController.GetDefault();
                GpioOpenStatus status;
                if (controller != null
                    && controller.TryOpenPin(AppSettings.ClockPinNumber, GpioSharingMode.Exclusive, out clockPin, out status)
                    && controller.TryOpenPin(AppSettings.DataPinNumber, GpioSharingMode.Exclusive, out dataPin, out status))
                {
                    device = new HX711(clockPin, dataPin);
                }
                else device = null;
            }
            if (device != null)
                device.PowerOn();
        }
        private int _GetOutputData()
        {
            InitializeDevice();
            int result = 0;
            if (device != null)
            {

                result = device.Read();
            }
            device.PowerDown();
            return result;
        }
    }
}

