﻿using IoTSuperScale.Configuration;
using IoTSuperScale.Core;
using IoTSuperScale.Models;
using IoTSuperScale.IoTViews;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

namespace IoTSuperScale
{

    sealed partial class App : Application
    {
        public static bool isAuthenticated;
        public static Encoding encoding;
        //Scale object is the first default view on start of application
        public static Scale s = new Scale();
        //Idle time vars
        public static new App Current => (App)Application.Current;
        public event EventHandler IsIdleChanged;
        public DispatcherTimer idleTimer;
        private bool isIdle;
        public bool IsIdle
        {
            get
            {
                return isIdle;
            }

            set
            {
                if (isIdle != value)
                {
                    isIdle = value;
                    IsIdleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                encoding = Encoding.GetEncoding("windows-1253");
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleEncodingError"));
            }
        }
        
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            idleTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(60 * AppSettings.ScreenSaverMins)
            };
            idleTimer.Tick += ΟnIdleTimerTick;
            if (AppSettings.ScreenSaverMins != 0)
                idleTimer.Start();
            Window.Current.CoreWindow.PointerMoved += ΟnCoreWindowPointerMoved;
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }
                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    await Startup.ConfigureAsync();
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(PageScale), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }
        
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void ΟnIdleTimerTick(object sender, object e)
        {
            idleTimer.Stop();
            IsIdle = true;
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(PageScreenSaver), null);
        }

        private void ΟnCoreWindowPointerMoved(CoreWindow sender, PointerEventArgs args)
        {
            idleTimer.Stop();
            idleTimer.Interval = TimeSpan.FromSeconds(60 * AppSettings.ScreenSaverMins);
            if (AppSettings.ScreenSaverMins != 0)
                idleTimer.Start();
            IsIdle = false;
        }
        
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        public static async void PrintOkMessage(string content, string title)
        {
            var dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand("Ok"));//,new UICommandInvokedHandler(this.CommandInvokedHandler)));
            await dialog.ShowAsync();
        }
        public static string GetAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("Version: {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

        }
        public static string GetAppTextFooter()
        {
            var currentAssembly = typeof(App).GetTypeInfo().Assembly;
            var customAttributes = currentAssembly.CustomAttributes;
            if (customAttributes != null)
            {
                var list = customAttributes.ToList();
                var result1 = list.FirstOrDefault(x => x.AttributeType.Name == "AssemblyCopyrightAttribute");
                AppSettings.Copyrightattr = result1.ConstructorArguments[0].Value.ToString();
                var result2 = list.FirstOrDefault(x => x.AttributeType.Name == "AssemblyCompanyAttribute");
                AppSettings.Companyattr = result2.ConstructorArguments[0].Value.ToString();
                var result3 = list.FirstOrDefault(x => x.AttributeType.Name == "AssemblyTrademarkAttribute");
                AppSettings.Trademarkattr = result3.ConstructorArguments[0].Value.ToString();
            }
            //same with GetAppVersion
            var version = typeof(App).GetTypeInfo().Assembly.GetName().Version;
            return AppSettings.Copyrightattr + " " + AppSettings.Companyattr + " " + AppSettings.Trademarkattr + " " + GetAppVersion();
        }
    }
}
