using IoTSuperScale.Core;
using IoTSuperScale.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IoTSuperScale.IoTViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PagePrinterUtils : Page
    {
        public PagePrinterUtils()
        {
            this.InitializeComponent();
            txtFooter.Text = App.GetAppTextFooter();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            LoadSettings();
        }
        private void LoadSettings()
        {
            txtboxIPPrinter.Text = AppSettings.IpPrinterConfig.ToString();
            txtBoxPortPrinter.Text = AppSettings.PortPrinterConfig.ToString();
            sumSpinner.TextValueProperty = AppSettings.SumPrints.ToString();
            copiesSpinner.TextValueProperty = AppSettings.CopiesPrints.ToString();
            palletsSpinner.TextValueProperty = AppSettings.PalletsNum.ToString();
        }
        private void TxtboxIPPrinter_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.IpPrinterConfig = txtboxIPPrinter.Text;
        }
        private void TxtBoxPortPrinter_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.PortPrinterConfig = txtBoxPortPrinter.Text;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
        }
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            Frame.Navigate(typeof(PageLogin), null, new SuppressNavigationTransitionInfo());
        }
        private void BtnPrnt_Click(object sender, RoutedEventArgs e)
        {
            PrinterUtil.SendTestToPrinter(txtBoxTestText.Text, copiesSpinner.TextValueProperty.ToString());
        }
        private void CopiesSpinner_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.CopiesPrints = Int32.Parse(copiesSpinner.TextValueProperty);
        }
        private void SumSpinner_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.SumPrints = Int32.Parse(sumSpinner.TextValueProperty);
        }
        private void PalletsSpinner_LostFocus(object sender, RoutedEventArgs e)
        {
            AppSettings.PalletsNum = Int32.Parse(palletsSpinner.TextValueProperty);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }
    }
}
