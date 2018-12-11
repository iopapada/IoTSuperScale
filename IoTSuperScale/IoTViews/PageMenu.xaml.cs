using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

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
            Frame.Navigate(typeof(PageScale), null, new SuppressNavigationTransitionInfo());
        }

        private void BtnPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PagePlan), null, new SuppressNavigationTransitionInfo());
        }
        private void BtnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageReceipt), null, new SuppressNavigationTransitionInfo());
        }

        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageCustomer), null, new SuppressNavigationTransitionInfo());
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PageSettings), null, new SuppressNavigationTransitionInfo());
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            Frame.Navigate(typeof(PageLogin), null, new SuppressNavigationTransitionInfo());
        }

        private void BtnPrinterUtils_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PagePrinterUtils), null, new SuppressNavigationTransitionInfo());
        }
    }
}
