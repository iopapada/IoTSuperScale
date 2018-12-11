using IoTSuperScale.IoTDB;
using System;
using System.Data.SqlClient;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using static IoTSuperScale.IoTDB.DBinit;

namespace IoTSuperScale.IoTViews
{
    public sealed partial class PageSettings : Page
    {
        //timer for seeing live real voltage
        public DispatcherTimer settingsTimer;
        public PageSettings()
        {
            this.InitializeComponent();
            settingsTimer = new DispatcherTimer();
            settingsTimer.Interval = TimeSpan.FromMilliseconds(AppSettings.ScaleTimer);
            settingsTimer.Tick += Timer_Tick;
            settingsTimer.Start();
            this.LoadSettings();
            txtFooter.Text = App.GetAppTextFooter();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
        }
        private void BtnZero_Click(object sender, RoutedEventArgs e)
        {
            App.s.Zero();
            txtOffset_Zero.Text = AppSettings.OffsetZero.ToString();
            txtMinOffset_Zero.Text = AppSettings.MinZero.ToString();
            txtMaxOffset_Zero.Text = AppSettings.MaxZero.ToString();
        }
        private void BtnCalibrate500gr_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.CalibrationHalfKilo = App.s.Calibrate(0.5);
            txtVal_500.Text = AppSettings.CalibrationHalfKilo.ToString();
        }
        private void BtnCalibrate1000gr_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.CalibrationKilo = App.s.Calibrate(1);
            txtVal_1000.Text = AppSettings.CalibrationKilo.ToString();
        }
        private void BtnERPDBconnectionTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
#pragma warning disable CS0642 // Possible mistaken empty statement
                if (SingletonERP.GetERPDbInstance().GetERPDBConnection()!=null) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgPingDB"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("titlePingDB"));
            }
            catch (SqlException ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleERPerrorDBConnection"));
            }
            finally {
                SingletonERP.GetERPDbInstance().CloseERPDBConnection();
            }
        }
        private void BtnMRPDBconnectionTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
#pragma warning disable CS0642 // Possible mistaken empty statement
                if (SingletonMRP.GetMRPDbInstance().GetMRPDBConnection() != null) ;
#pragma warning restore CS0642 // Possible mistaken empty statement
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgPingDB"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("titlePingDB"));
            }
            catch (SqlException ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleMRPerrorDBConnection"));
            }
            finally
            {
                SingletonMRP.GetMRPDbInstance().CloseMRPDBConnection();
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            settingsTimer.Stop();
            Frame rootFrame = Window.Current.Content as Frame;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
        }
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            settingsTimer.Stop();
            Frame.Navigate(typeof(PageLogin), null, new SuppressNavigationTransitionInfo());
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
        }

        public void LoadSettings()
        {
            txtOffset_Zero.Text = AppSettings.OffsetZero.ToString();
            txtMinOffset_Zero.Text = AppSettings.MinZero.ToString();
            txtMaxOffset_Zero.Text = AppSettings.MaxZero.ToString();
            txtVal_500.Text = AppSettings.CalibrationHalfKilo.ToString();
            txtVal_1000.Text = AppSettings.CalibrationKilo.ToString();
            txtboxScaleName.Text = AppSettings.ScaleName.ToString();

            txtboxIP.Text = AppSettings.IpConfig;
            txtBoxPort.Text = AppSettings.PortConfig;
            txtBoxIPServer.Text = AppSettings.IpERPServerConfig;
            txtBoxPortServer.Text = AppSettings.PortERPServerConfig;
            txtBoxDBname.Text = AppSettings.ERPDBname;
            txtBoxDBInstance.Text = AppSettings.ERPDBInstance;
            txtBoxMRPDBname.Text = AppSettings.MRPDBname;
            txtBoxMRPDBInstance.Text = AppSettings.MRPDBInstance;
            txtBoxDBuser.Text = AppSettings.DBuser;
            txtBoxDBpass.Password = AppSettings.DBpass;

            decimalPoints.TextValueProperty = AppSettings.Precision.ToString();
            screenSaverSpinner.TextValueProperty = AppSettings.ScreenSaverMins.ToString();
            txtLCcapacity.Text = AppSettings.LCcapacity.ToString();
            txtScaleTimer.Text = AppSettings.ScaleTimer.ToString();
        }
        private void Timer_Tick(object sender, object e)
        {
            App.s.GetReading();
            txtRVoltage.Text = App.s.lastOutput.ToString();
        }
        private void ChkBroadcast_Checked(object sender, RoutedEventArgs e)
        {
            AppSettings.BroadcastPcksConfig = true;
        }
        private void ChkBroadcast_Unchecked(object sender, RoutedEventArgs e)
        {
            AppSettings.BroadcastPcksConfig = false;
        }
        private void TxtboxScaleName_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.ScaleName = txtboxScaleName.Text;
        }
        private void TxtboxIP_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.IpConfig = txtboxIP.Text;
        }
        private void TxtBoxPort_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.PortConfig = txtBoxPort.Text;
        }
        private void TxtLCcapacity_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                AppSettings.LCcapacity = Int32.Parse(txtLCcapacity.Text.ToString());
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleSettingsOnSaveLCcapacity"));
            }
        }
        private void DecimalPoints_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                AppSettings.Precision = Int32.Parse(decimalPoints.TextValueProperty.ToString());
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleSettingsOnSavePrecision"));
            }
        }
        private void ScreenSaverSpinner_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int ignoreMe;
                bool successfullyParsed = int.TryParse(screenSaverSpinner.TextValueProperty.ToString(), out ignoreMe);
                if (successfullyParsed)
                {
                    AppSettings.ScreenSaverMins = Int32.Parse(screenSaverSpinner.TextValueProperty.ToString());
                    if (AppSettings.ScreenSaverMins == 0)
                    {
                        App.Current.idleTimer.Stop();
                        App.Current.IsIdle = true;
                    }
                    else
                    {
                        App.Current.idleTimer.Interval = TimeSpan.FromSeconds(60 * AppSettings.ScreenSaverMins);
                        App.Current.idleTimer.Start();
                        App.Current.IsIdle = false;
                    }
                }
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleSettingsOnSaveScreensaver"));
            }
        }
        private void TxtScaleTimer_LosingFocus(UIElement sender, Windows.UI.Xaml.Input.LosingFocusEventArgs args)
        {
            try
            {
                AppSettings.ScaleTimer = Int32.Parse(txtScaleTimer.Text.ToString());
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleSettingsOnSaveTimer"));
            }
        }
        private void ΤxtBoxPortServer_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.PortERPServerConfig = txtBoxPortServer.Text;
        }
        private void ΤxtboxIPServer_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.IpERPServerConfig = txtBoxIPServer.Text;
        }
        private void ΤxtDBInstance_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.ERPDBInstance = txtBoxDBInstance.Text;
        }
        private void ΤxtDBname_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.ERPDBname = txtBoxDBname.Text;
        }
        private void ΤxtBoxMRPDBInstance_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.MRPDBInstance = txtBoxMRPDBInstance.Text;
        }
        private void ΤxtBoxMRPDBname_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.MRPDBname = txtBoxMRPDBname.Text;
        }
        private void ΤxtDBuser_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.DBuser = txtBoxDBuser.Text;
        }
        private void ΤxtDBpass_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.DBpass = txtBoxDBpass.Password;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }
    }
}
