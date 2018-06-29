using IoTSuperScale.IoTCore;
using IoTSuperScale.IoTDB;
using IoTSuperScale.IoTViews;
using System;
using System.Diagnostics;
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

namespace IoTSuperScale
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
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
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleEncodingError"));
            }
        }
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            idleTimer = new DispatcherTimer();
            idleTimer.Interval = TimeSpan.FromSeconds(60*AppSettings.ScreenSaverMins);
            idleTimer.Tick += onIdleTimerTick;
            if (AppSettings.ScreenSaverMins != 0)
                idleTimer.Start();
            Window.Current.CoreWindow.PointerMoved += onCoreWindowPointerMoved;
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
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(PageScale), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }
        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        private void onIdleTimerTick(object sender, object e)
        {
            idleTimer.Stop();
            IsIdle = true;
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(PageScreenSaver), null);
        }

        private void onCoreWindowPointerMoved(CoreWindow sender, PointerEventArgs args)
        {
            idleTimer.Stop();
            idleTimer.Interval = TimeSpan.FromSeconds(60 * AppSettings.ScreenSaverMins);
            if (AppSettings.ScreenSaverMins != 0)
                idleTimer.Start();
            IsIdle = false;
        }
        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
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
            const string proteasbirth = "04-05-2017";
            var startDate = DateTime.Parse(proteasbirth);
            var currentDate = DateTime.Now;
            var elapsedTimeSpan = currentDate.Subtract(startDate);

            return string.Format("Version: {0}", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion);
            //For Assembly version
            //return string.Format("Version: {0}",Assembly.GetExecutingAssembly().GetName().Version.ToString()).Replace("DB",elapsedTimeSpan.TotalDays.ToString());

        }
        public static string GetAppTextFooter()
        {
            var currentAssembly = typeof(App).GetTypeInfo().Assembly;
            var customAttributes = currentAssembly.CustomAttributes;
            var list = customAttributes.ToList();
            var res = list[0];
            var result1 = list.FirstOrDefault(x => x.AttributeType.Name == "AssemblyCopyrightAttribute");
            string copyrightattr = result1.ConstructorArguments[0].Value.ToString();
            var result2 = list.FirstOrDefault(x => x.AttributeType.Name == "AssemblyCompanyAttribute");
            string companyattr = result2.ConstructorArguments[0].Value.ToString();
            var result3 = list.FirstOrDefault(x => x.AttributeType.Name == "AssemblyTrademarkAttribute");
            string trademarkattr = result3.ConstructorArguments[0].Value.ToString();
            return copyrightattr + " " + companyattr + "," + trademarkattr + "," + GetAppVersion();
        }
    }
}
