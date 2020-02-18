using IoTSuperScale.IoTDB;
using Windows.ApplicationModel.Core;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace IoTSuperScale.IoTViews
{
    public sealed partial class PageLogin : Page
    {
        public PageLogin()
        {
            this.InitializeComponent();
            txtFooter.Text = App.GetAppTextFooter();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            LoadSettings();
        }
        private void LoadSettings()
        {
            if (AppSettings.LangConfig.Equals("GR"))
                CBoxLang.SelectedItem = CBoxLang.Items[0] as ComboBoxItem;
            else
                CBoxLang.SelectedItem = CBoxLang.Items[1] as ComboBoxItem;
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame mainFrame = Window.Current.Content as Frame;
            App.isAuthenticated = false;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            mainFrame.Navigate(typeof(PageScale), null, new SuppressNavigationTransitionInfo());
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = true;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            Frame.Navigate(typeof(PageMenu), null, new SuppressNavigationTransitionInfo());
        }
        private void CBoxLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem temp1 = CBoxLang.SelectedItem as ComboBoxItem;
            var culture = new System.Globalization.CultureInfo("en-US");
            switch (temp1.Name)
            {
                case "GR":
                    culture = new System.Globalization.CultureInfo("el-GR");
                    break;
                default:
                    culture = new System.Globalization.CultureInfo("en-US");
                    break;
                    
            }
            AppSettings.LangConfig = temp1.Name;

            ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().Reset();
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse().Reset();
            //Reload frame
            var _Frame = Window.Current.Content as Frame;
            _Frame.Navigate(_Frame.Content.GetType());
            _Frame.GoBack();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Exit();
            CoreApplication.Exit();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }
    }
}
