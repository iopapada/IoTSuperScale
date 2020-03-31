using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace IoTSuperScale.Core
{
    class SerialCOM
    {
        static SerialDevice SerialPort;
        public async static void SerialConfig()
        {
            string aqs = SerialDevice.GetDeviceSelector("UART0");                   
            var dis = await DeviceInformation.FindAllAsync(aqs);                    
            SerialPort = await SerialDevice.FromIdAsync(dis[0].Id);    

            /* Configure serial settings */
            SerialPort.WriteTimeout = TimeSpan.FromMilliseconds(1000);
            SerialPort.ReadTimeout = TimeSpan.FromMilliseconds(1000);
            SerialPort.BaudRate = 9600;                                             
            SerialPort.Parity = SerialParity.None;                                  
            SerialPort.StopBits = SerialStopBitCount.One;                           
            SerialPort.DataBits = 8;
        }
        public async static void SerialWriteAsync(string txtbuffer)
        {
            DataWriter dataWriter = new DataWriter();
            dataWriter.WriteString(txtbuffer);
            uint bytesWritten = await SerialPort.OutputStream.WriteAsync(dataWriter.DetachBuffer());
        }
        public async static Task<string> SerialReadAsync()
        {
            const uint maxReadLength = 1024;
            DataReader dataReader = new DataReader(SerialPort.InputStream);
            uint bytesToRead = await dataReader.LoadAsync(maxReadLength);
            string rxBuffer = dataReader.ReadString(bytesToRead);
            return rxBuffer;
        }

    }
}
