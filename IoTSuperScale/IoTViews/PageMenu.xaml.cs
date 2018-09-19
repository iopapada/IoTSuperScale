using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IoTSuperScale.IoTViews
{
    public sealed partial class PageMenu : Page
    {
        public PageMenu()
        {
            this.InitializeComponent();
            txtFooter.Text = App.GetAppTextFooter();
            //NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
        }

        private void BtnScale_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageScale), null);
        }

        private void BtnPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PagePlan), null);
        }
        private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageReceipt), null);
        }

        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageCustomer), null);
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageSettings), null);
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            Frame.Navigate(typeof(PageLogin), null);
        }

        private void BtnPrinterUtils_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PagePrinterUtils), null);
        }
    }
}
