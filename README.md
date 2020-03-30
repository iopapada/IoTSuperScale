# IoTSuperScale

IoT business weight application for OS 10.0.16299 and above

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Hardware Prerequisites

What things you will need to install the software and how to install them:

1. [Raspberry pi 3B model](https://www.google.com/search?q=raspberry+pi+3+b&tbm=isch&ved=2ahUKEwjyrNqNoMDoAhWLLOwKHYKOCwwQ2-cCegQIABAA&oq=raspberry+pi+3+b&gs_lcp=CgNpbWcQAzIECCMQJzIECCMQJzICCAAyBAgAEB4yBAgAEB4yBAgAEB4yBAgAEB4yBAgAEB4yBAgAEB4yBAgAEB5QsrQDWLK0A2CetgNoAHAAeACAAcABiAHAAZIBAzAuMZgBAKABAaoBC2d3cy13aXotaW1n&sclient=img&ei=geCAXrLoOIvZsAeCna5g&bih=937&biw=1920#imgrc=n8jzdr6hUV6CQM)
2. [Raspberry 7" Official touch screen (Capacitive touch)](https://www.adslgr.com/forum/attachment.php?attachmentid=160698&d=1441707390&thumb=1)
3. A microSD card at least 8GB
4. [Load Cell Amplifier HX711](https://www.google.com/search?q=amplifier+hx711&tbm=isch&hl=el&chips=q:amplifier+hx711,online_chips:hx711+load+cells&hl=el&ved=2ahUKEwjAj_esnMDoAhUFeRoKHcRYC60Q4lYoAHoECAEQFQ&biw=1903&bih=937#imgrc=fvOwk2pwjuEr8M)
5. An Indusrtial Load Cell bar
6. A Label printer i.e. [Zebra GC420t](https://www.google.com/search?q=zebra+gc420t+google+icons&tbm=isch&ved=2ahUKEwj2kvPmmcDoAhW2wQIHHZYkDRsQ2-cCegQIABAA&oq=zebra+gc420t+google+icons&gs_lcp=CgNpbWcQA1Di9SZYiIInYJaDJ2gAcAB4AIABsAGIAZYGkgEDMC41mAEAoAEBqgELZ3dzLXdpei1pbWc&sclient=img&ei=5dmAXraMObaDi-gPlsm02AE&bih=937&biw=1903&hl=el#imgrc=llM8peIN_1O2qM&imgdii=V2jDth8p1nNchM)
7. [ZebraNet 10/100 Print Server](https://www.google.com/imgres?imgurl=https%3A%2F%2Fcdn11.bigcommerce.com%2Fs-40d25%2Fimages%2Fstencil%2F1280x1280%2Fproducts%2F420%2F1587%2Fzebra-p1031031-zebranet-10-100-external-print-server-supports-the-following-printers-2824-2844-2824z-3842-2844z-105sl-110pax4-110xiiiip_1__41471.1487287824.jpg%3Fc%3D2%26imbypass%3Don&imgrefurl=https%3A%2F%2Fwww.barcodes.com.au%2Fzebra-print-server-external-10-100%2F&tbnid=s8LZkPO-yk5SoM&vet=12ahUKEwi9qrnPmMDoAhWQlRQKHS_XDM4QMygkegQIARBW..i&docid=VH3UMg9CvTczOM&w=1280&h=960&q=server%20printer%20zebra%20gc%20420t&ved=2ahUKEwi9qrnPmMDoAhWQlRQKHS_XDM4QMygkegQIARBW)
### Installing App

Fisrt of all you have to Go to the Windows 10 developer center and Get the Windows 10 IoT Core Dashboard. Select set up device in order to create the OS image of raspberry. [Link to set up device](https://www.windowscentral.com/how-install-windows-10-iot-raspberry-pi-3)

<img src="https://www.programoergosum.com/images/cursos/238-control-de-gpio-con-python-en-raspberry-pi/pines-gpio-rpi-2.png" width="45%"></img>
<img src="https://www.programoergosum.com/images/cursos/238-control-de-gpio-con-python-en-raspberry-pi/pines-gpio-rpi-2.png" width="45%"></img>

Clone the project in Visual studio and create the App package by right clicking the .csproj -> Store -> Create App Packages.
In your Browser type the ip_device:8080 -> credentials device -> Apps -> Apps manager -> Local storage -> Choose file from AppPackages.

<img src="https://cloud.githubusercontent.com/assets/4307137/10105283/251b6868-63ae-11e5-9918-b789d9d682ec.png" width="30%"></img> 
<img src="https://cloud.githubusercontent.com/assets/4307137/10105290/2a183f3a-63ae-11e5-9380-50d9f6d8afd6.png" width="30%"></img> 
<img src="https://cloud.githubusercontent.com/assets/4307137/10105284/26aa7ad4-63ae-11e5-88b7-bc523a095c9f.png" width="30%"></img> 

## On Coding side

You will find features such as:
* **Multilingual resources**
* **Connection with the Load cell sensor**

### Receiving data from sensor via amplifier HX711
```C#
while (!IsReady()){
}
string binaryData = "";
for(int pulses = 0; pulses < 25 + (int)InputAndGainSelection ; pulses++)
{
    PowerDownAndSerialClockInput.Write(GpioPinValue.High);
    PowerDownAndSerialClockInput.Write(GpioPinValue.Low);
    if (pulses < 25)
        binaryData += (int)SerialDataOutput.Read();
}
return Convert.ToInt32(binaryData, 2);
}
```
### Transformation of Voltage to Weight value
```C#
voltOutput = _GetOutputData();
//Transform the processed voltage value
double dataOffsetDiff = voltOutput - AppSettings.OffsetZero;
if (AppSettings.CalibrationKilo != 1)
    finalDigitVal = dataOffsetDiff / AppSettings.CalibrationKilo;
else if (AppSettings.CalibrationHalfKilo != 1)
    finalDigitVal = dataOffsetDiff / AppSettings.CalibrationHalfKilo;
finalDigitVal = Math.Round(finalDigitVal, AppSettings.Precision);
finalStringVal = AppSettings.LeadingUnit + Math.Round(finalDigitVal, AppSettings.Precision).ToString() + AppSettings.TrailingUnit;
//Auto correct offset...
if (finalDigitVal < 0.05 && finalDigitVal > -0.05){
    AppSettings.OffsetZero = voltOutput;
    finalStringVal = zeroPointString;
}

return finalStringVal;
```
* **Connection with MS SQL server**
* On having Connection with MS sql server, you have to modify the sql procedures
* **Without SQL Server connection wil retrieve dummy data from JSON files**
* **Connection with Label printer via WiFi Network**

### Sending label file to the printer
```C#
socket = new StreamSocket();
HostName serverHost = new HostName(AppSettings.IpPrinterConfig);
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
}
```

### Great hints on coding the app

[HX711 amplifier](https://github.com/Pabreetzio/IotScale)
[On Creating and read files .x and .json](https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files "How to create and read files in UWP")

## Configuration of system

### Configuration 
### Configuration Printer settings
### Configuration Network settings
Add additional notes about how to deploy this on a live system
<img src="https://cloud.githubusercontent.com/assets/4307137/10105288/28698fae-63ae-11e5-8ba7-a62360a8e8a7.png" width="30%"></img> 
<img src="https://cloud.githubusercontent.com/assets/4307137/10105283/251b6868-63ae-11e5-9918-b789d9d682ec.png" width="30%"></img> 
<img src="https://cloud.githubusercontent.com/assets/4307137/10105290/2a183f3a-63ae-11e5-9380-50d9f6d8afd6.png" width="30%"></img> 

## Demo in real World!!

Add additional notes about how to deploy this on a live system
<img src="https://cloud.githubusercontent.com/assets/4307137/10105288/28698fae-63ae-11e5-8ba7-a62360a8e8a7.png" width="50%"></img> 
<img src="https://cloud.githubusercontent.com/assets/4307137/10105283/251b6868-63ae-11e5-9918-b789d9d682ec.png" width="50%"></img> 

## New features

## What you have to consider before implementing UWP application on Windows IoT 10

* [Performance](https://docs.microsoft.com/en-us/windows/uwp/debug-test-perf/performance-and-xaml-ui "Performance")
