using IoTSuperScale.IoTDB;
using System;
using System.Linq;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            loadSettings();
        }

        private void loadSettings()
        {
            if (AppSettings.LangConfig.Equals("GR"))
                CBoxLang.SelectedItem = CBoxLang.Items[0] as ComboBoxItem;
            else
                CBoxLang.SelectedItem = CBoxLang.Items[1] as ComboBoxItem;
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
        }
    }
}
