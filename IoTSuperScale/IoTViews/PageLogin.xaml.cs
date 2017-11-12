using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IoTSuperScale.IoTViews
{
    public sealed partial class PageLogin : Page
    {
        public PageLogin()
        {
            this.InitializeComponent();
            txtFooter.Text = App.GetAppTextFooter();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame mainFrame = Window.Current.Content as Frame;
            App.isAuthenticated = false;
            mainFrame.Navigate(typeof(PageScale), null);
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = true;
            Frame.Navigate(typeof(PageMenu), null);
        }
    }
}
