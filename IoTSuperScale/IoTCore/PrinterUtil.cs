using IoTSuperScale.IoTDB;
using System;
using System.IO;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage;
using Windows.Storage.Streams;

namespace IoTSuperScale.IoTCore
{
    public class PrinterUtil
    {
        //Printer helper
        static StreamSocket socket;
        public static async void sendTestToPrinter(string test, string printsSpinner)
        {
            StorageFile testLabel = await ApplicationData.Current.LocalFolder.GetFileAsync("Customer.x");
            if (testLabel != null)
            {
                //fill the data
                IBuffer bf = await FileIO.ReadBufferAsync(testLabel);
                DataReader reader = DataReader.FromBuffer(bf);
                byte[] fileContent = new byte[reader.UnconsumedBufferLength];
                reader.ReadBytes(fileContent);
                string protoVal = App.encoding.GetString(fileContent, 0, fileContent.Length);
                string newVal = protoVal.Replace("customerdescr", test);
                newVal = newVal.Replace("nums", printsSpinner);

                StorageFile dataTestLabel = await ApplicationData.Current.LocalFolder.CreateFileAsync("Data" + testLabel.Name, CreationCollisionOption.ReplaceExisting);
                File.WriteAllText(dataTestLabel.Path, newVal, App.encoding);
                sendToPrinterFile(dataTestLabel);
            }
        }

        public static async void sendToPrinterFile(StorageFile dataWeightLabel)
        {
            socket = new StreamSocket();
            HostName serverHost = new HostName(AppSettings.IpPrinterConfig);
            try
            {
                await socket.ConnectAsync(serverHost, AppSettings.PortPrinterConfig);
                //send binary readfile to printer
                byte[] buffer = new byte[1024];
                int readcount = 0;
                using (BinaryReader fileReader = new BinaryReader(dataWeightLabel.OpenStreamForReadAsync().GetAwaiter().GetResult()))
                {
                    int read = fileReader.Read(buffer, 0, buffer.Length);
                    while (read > 0)
                    {
                        readcount += read;
                        Stream streamWrite = socket.OutputStream.AsStreamForWrite();
                        streamWrite.Write(buffer, 0, read);
                        await streamWrite.FlushAsync();
                        read = fileReader.Read(buffer, 0, buffer.Length);
                    }
                    //streamWrite.Dispose();
                }
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, "Print error");
            }
            finally
            {
                socket.Dispose();
            }
        }
    }
}
