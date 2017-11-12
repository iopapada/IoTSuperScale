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
        }

        private void btnScale_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageScale), null);
        }

        private void btnPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PagePlan), null);
        }
        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageReceipt), null);
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageCustomer), null);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageSettings), null);
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            Frame.Navigate(typeof(PageLogin), null);
        }

        private void btnPrinterUtils_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PagePrinterUtils), null);
        }
    }
}
