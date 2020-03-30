# IoTSuperScale

IoT business weight application for OS 10.0.16299 and above

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Hardware Prerequisites

What things you need to install the software and how to install them:

1. [Raspberry pi 3B model](https://www.google.com/search?q=raspberry+pi+3+b&tbm=isch&ved=2ahUKEwjyrNqNoMDoAhWLLOwKHYKOCwwQ2-cCegQIABAA&oq=raspberry+pi+3+b&gs_lcp=CgNpbWcQAzIECCMQJzIECCMQJzICCAAyBAgAEB4yBAgAEB4yBAgAEB4yBAgAEB4yBAgAEB4yBAgAEB4yBAgAEB5QsrQDWLK0A2CetgNoAHAAeACAAcABiAHAAZIBAzAuMZgBAKABAaoBC2d3cy13aXotaW1n&sclient=img&ei=geCAXrLoOIvZsAeCna5g&bih=937&biw=1920#imgrc=n8jzdr6hUV6CQM)
2. Raspberry 7" Official touch screen (Capacitive touch)
3. A microSD card at least 8GB
4. [Load Cell Amplifier HX711](https://www.google.com/search?q=amplifier+hx711&tbm=isch&hl=el&chips=q:amplifier+hx711,online_chips:hx711+load+cells&hl=el&ved=2ahUKEwjAj_esnMDoAhUFeRoKHcRYC60Q4lYoAHoECAEQFQ&biw=1903&bih=937#imgrc=fvOwk2pwjuEr8M)
5. An Indusrtial Load Cell bar
6. A Label printer i.e. [Zebra GC420t](https://www.google.com/search?q=zebra+gc420t+google+icons&tbm=isch&ved=2ahUKEwj2kvPmmcDoAhW2wQIHHZYkDRsQ2-cCegQIABAA&oq=zebra+gc420t+google+icons&gs_lcp=CgNpbWcQA1Di9SZYiIInYJaDJ2gAcAB4AIABsAGIAZYGkgEDMC41mAEAoAEBqgELZ3dzLXdpei1pbWc&sclient=img&ei=5dmAXraMObaDi-gPlsm02AE&bih=937&biw=1903&hl=el#imgrc=llM8peIN_1O2qM&imgdii=V2jDth8p1nNchM)
7. [ZebraNet 10/100 Print Server](https://www.google.com/imgres?imgurl=https%3A%2F%2Fcdn11.bigcommerce.com%2Fs-40d25%2Fimages%2Fstencil%2F1280x1280%2Fproducts%2F420%2F1587%2Fzebra-p1031031-zebranet-10-100-external-print-server-supports-the-following-printers-2824-2844-2824z-3842-2844z-105sl-110pax4-110xiiiip_1__41471.1487287824.jpg%3Fc%3D2%26imbypass%3Don&imgrefurl=https%3A%2F%2Fwww.barcodes.com.au%2Fzebra-print-server-external-10-100%2F&tbnid=s8LZkPO-yk5SoM&vet=12ahUKEwi9qrnPmMDoAhWQlRQKHS_XDM4QMygkegQIARBW..i&docid=VH3UMg9CvTczOM&w=1280&h=960&q=server%20printer%20zebra%20gc%20420t&ved=2ahUKEwi9qrnPmMDoAhWQlRQKHS_XDM4QMygkegQIARBW)
### Installing

Fisrt of all you have to Go to the Windows 10 developer center and Get the Windows 10 IoT Core Dashboard. Select

![alt Raspberry GPIO pins](https://www.google.com/search?q=raspberry+pi+3+b%2B&sxsrf=ALeKk01OBO8st5aVfrS-pbdA2m6mgbCd_g:1585504080906&source=lnms&tbm=isch&sa=X&ved=2ahUKEwiN4qD8nsDoAhXzweYKHaUyAIQQ_AUoAXoECAwQAw&biw=1920&bih=937#imgrc=FgNob9M3AV6tBM&imgdii=KzpD3WChgPaJsM)

<img src="https://cloud.githubusercontent.com/assets/4307137/10105283/251b6868-63ae-11e5-9918-b789d9d682ec.png" width="30%"></img> <img src="https://cloud.githubusercontent.com/assets/4307137/10105290/2a183f3a-63ae-11e5-9380-50d9f6d8afd6.png" width="30%"></img> <img src="https://cloud.githubusercontent.com/assets/4307137/10105284/26aa7ad4-63ae-11e5-88b7-bc523a095c9f.png" width="30%"></img> <img src="https://cloud.githubusercontent.com/assets/4307137/10105288/28698fae-63ae-11e5-8ba7-a62360a8e8a7.png" width="30%"></img> <img src="https://cloud.githubusercontent.com/assets/4307137/10105283/251b6868-63ae-11e5-9918-b789d9d682ec.png" width="30%"></img> <img src="https://cloud.githubusercontent.com/assets/4307137/10105290/2a183f3a-63ae-11e5-9380-50d9f6d8afd6.png" width="30%"></img> 

## On Coding side

You will find features such as:
* **Connection with MS sql server**

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

[On Creating and read files .x and .json](https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files "Google's Homepage")

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## What you have to consider before implementing UWP application on Windows IoT 10

* [Performance](https://docs.microsoft.com/en-us/windows/uwp/debug-test-perf/performance-and-xaml-ui "Performance")
