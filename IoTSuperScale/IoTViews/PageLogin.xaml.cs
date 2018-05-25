using IoTSuperScale.IoTDB;
using System;
using System.Linq;
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

            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().Reset();
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse().Reset();
            //Reload frame
            var _Frame = Window.Current.Content as Frame;
            _Frame.Navigate(_Frame.Content.GetType());
            _Frame.GoBack();
        }
    }
}
