using IoTSuperScale.IoTDB;
using System;
using System.Data.SqlClient;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        private void btnZero_Click(object sender, RoutedEventArgs e)
        {
            App.s.Zero();
            txtOffset_Zero.Text = AppSettings.OffsetZero.ToString();
            txtMinOffset_Zero.Text = AppSettings.MinZero.ToString();
            txtMaxOffset_Zero.Text = AppSettings.MaxZero.ToString();
        }
        private void btnCalibrate500gr_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.CalibrationHalfKilo = App.s.Calibrate(0.5);
            txtVal_500.Text = AppSettings.CalibrationHalfKilo.ToString();
        }
        private void btnCalibrate1000gr_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.CalibrationKilo = App.s.Calibrate(1);
            txtVal_1000.Text = AppSettings.CalibrationKilo.ToString();
        }
        private void btnERPDBconnectionTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SingletonERP.getERPDbInstance().GetERPDBConnection();
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgPingDB"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("titlePingDB"));
            }
            catch (SqlException ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleERPerrorDBConnection"));
            }
            finally {
                SingletonERP.getERPDbInstance().CloseERPDBConnection();
            }
        }
        private void btnMRPDBconnectionTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SingletonMRP.getMRPDbInstance().GetMRPDBConnection();
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgPingDB"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("titlePingDB"));
            }
            catch (SqlException ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleMRPerrorDBConnection"));
            }
            finally
            {
                SingletonMRP.getMRPDbInstance().CloseMRPDBConnection();
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            settingsTimer.Stop();
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            settingsTimer.Stop();
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
        private void chkBroadcast_Checked(object sender, RoutedEventArgs e)
        {
            AppSettings.BroadcastPcksConfig = true;
        }
        private void chkBroadcast_Unchecked(object sender, RoutedEventArgs e)
        {
            AppSettings.BroadcastPcksConfig = false;
        }
        private void txtboxScaleName_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.ScaleName = txtboxScaleName.Text;
        }
        private void txtboxIP_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.IpConfig = txtboxIP.Text;
        }
        private void txtBoxPort_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.PortConfig = txtBoxPort.Text;
        }
        private void txtLCcapacity_LostFocus(object sender, RoutedEventArgs e)
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
        private void decimalPoints_LostFocus(object sender, RoutedEventArgs e)
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
        private void screenSaverSpinner_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                AppSettings.ScreenSaverMins = Int32.Parse(screenSaverSpinner.TextValueProperty.ToString());
                if (AppSettings.ScreenSaverMins == 0)
                {
                    App.Current.idleTimer.Stop();
                    App.Current.IsIdle = true;
                }
                else {
                    App.Current.idleTimer.Interval = TimeSpan.FromSeconds(60 * AppSettings.ScreenSaverMins);
                    App.Current.idleTimer.Start();
                    App.Current.IsIdle = false;
                }
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleSettingsOnSaveScreensaver"));
            }
        }
        private void txtScaleTimer_LosingFocus(UIElement sender, Windows.UI.Xaml.Input.LosingFocusEventArgs args)
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
        private void txtBoxPortServer_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.PortERPServerConfig = txtBoxPortServer.Text;
        }
        private void txtboxIPServer_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.IpERPServerConfig = txtBoxIPServer.Text;
        }
        private void txtDBInstance_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.ERPDBInstance = txtBoxDBInstance.Text;
        }
        private void txtDBname_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.ERPDBname = txtBoxDBname.Text;
        }
        private void txtBoxMRPDBInstance_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.MRPDBInstance = txtBoxMRPDBInstance.Text;
        }
        private void txtBoxMRPDBname_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.MRPDBname = txtBoxMRPDBname.Text;
        }
        private void txtDBuser_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.DBuser = txtBoxDBuser.Text;
        }
        private void txtDBpass_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.DBpass = txtBoxDBpass.Password;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
        }
    }
}
