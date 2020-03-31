using IoTSuperScale.DB;
using IoTSuperScale.IoTControls;
using IoTSuperScale.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace IoTSuperScale.IoTViews
{
    public sealed partial class PagePlan : Page
    {
        private ObservableCollection<PackagedMaterialItem> MaterialOptions;
        private PackagedMaterialItem _SelectedMaterial;
        public event PropertyChangedEventHandler PropertyChanged;

        public PagePlan()
        {
            this.InitializeComponent();
            //Load materials in ComboBox
            MaterialOptions = new ObservableCollection<PackagedMaterialItem>();
            DBOptionsManager.GetEnabledPackMaterialsList(MaterialOptions);
            _SelectedMaterial = MaterialOptions[0];
            SelectedMaterial = MaterialOptions[0];
            txtFooter.Text = App.GetAppTextFooter();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
        }
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            Frame.Navigate(typeof(PageLogin), null, new SuppressNavigationTransitionInfo());
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
        }
        private void BtnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //IoTDB.DBinit.GetDB();
                RowDefinition newR = new RowDefinition();
                int rowCount = grid.RowDefinitions.Count;
                newR.Height = new GridLength(0, GridUnitType.Auto);

                ColumnDefinition col1 = new ColumnDefinition();
                ColumnDefinition col2 = new ColumnDefinition();
                ColumnDefinition col3 = new ColumnDefinition();

                col1.Width = new GridLength(0, GridUnitType.Auto);
                col2.Width = new GridLength(0, GridUnitType.Auto);
                col3.Width = new GridLength(0, GridUnitType.Auto);

                grid.RowDefinitions.Add(newR);
                grid.ColumnDefinitions.Add(col1);
                grid.ColumnDefinitions.Add(col2);
                grid.ColumnDefinitions.Add(col3);

                ComboBox cboxMaterial = new ComboBox
                {
                    MinWidth = 290,
                    MaxWidth = 290,
                    Width = 290,
                    Height = 50,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Name = "CBoxMaterials",
                    DisplayMemberPath = "DisplayCodeDescr",
                    ItemsSource = MaterialOptions,
                    FontSize = 16
                };
                //cboxMaterial.SelectedItem = SelectedMaterial;
                //cboxMaterial.SelectedValuePath = SelectedMaterial.code;

                NumericSpinner numSpinner = new NumericSpinner
                {
                    Name = "spinner" + rowCount,
                    TextValueProperty = "24"
                };

                Button btnDelete = new Button
                {
                    Name = "btnDel" + rowCount,
                    Width = 50,
                    Height = 50
                };
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image
                {
                    Source = new BitmapImage(new Uri("ms-appx:///Assets/Delete.png"))
                };
                myBrush.ImageSource = image.Source;
                btnDelete.Background = myBrush;

                grid.Children.Add(cboxMaterial);
                grid.Children.Add(numSpinner);
                grid.Children.Add(btnDelete);

                Grid.SetColumn(cboxMaterial, 0);
                Grid.SetRow(cboxMaterial, rowCount);
                Grid.SetColumn(numSpinner, 1);
                Grid.SetRow(numSpinner, rowCount);
                Grid.SetColumn(btnDelete, 2);
                Grid.SetRow(btnDelete, rowCount);
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleGridError"));
            }
        }
        public PackagedMaterialItem SelectedMaterial
        {
            get
            {
                return _SelectedMaterial;
            }
            set
            {
                if (_SelectedMaterial != value)
                {
                    _SelectedMaterial = value;
                    RaisePropertyChanged("SelectedMaterial");
                }
            }
        }
        void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }
    }
}
