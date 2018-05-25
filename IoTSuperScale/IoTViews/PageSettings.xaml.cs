using IoTSuperScale.IoTCore;
using IoTSuperScale.IoTDB;
using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IoTSuperScale.IoTViews
{
    public sealed partial class PageSettings : Page
    {
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
            Frame.Navigate(typeof(PageLogin), null);
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
            txtBoxDBuser.Text = AppSettings.ERPDBuser;
            txtBoxDBpass.Password = AppSettings.ERPDBpass;

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
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgSettingsOnSaveLCcapacity"));
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
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgSettingsOnSavePrecision"));
            }
        }
        private void screenSaverSpinner_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                AppSettings.ScreenSaverMins = Int32.Parse(screenSaverSpinner.TextValueProperty.ToString());
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgSettingsOnSaveScreensaver"));
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
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgSettingsOnSaveTimer"));
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

        private void txtDBuser_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.ERPDBuser = txtBoxDBuser.Text;
        }

        private void txtDBpass_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.ERPDBpass = txtBoxDBpass.Password;
        }
    }
}
