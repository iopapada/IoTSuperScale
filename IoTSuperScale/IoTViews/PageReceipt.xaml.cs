using IoTSuperScale.IoTCore;
using IoTSuperScale.IoTDB;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Streams;
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
    public sealed partial class PageReceipt : Page, INotifyPropertyChanged
    {
        private ObservableCollection<SupplierItem> SupplierOptions;
        private SupplierItem _SelectedSupplier;
        private ObservableCollection<PackagedMaterialItem> PackagedMaterialOptions;
        private PackagedMaterialItem _SelectedPackagedMaterial;
        public event PropertyChangedEventHandler PropertyChanged;
        //Printer helper
        StorageFile protoWeightLabel;
        StorageFile dataWeightLabel;
        Encoding encoding;

        public PageReceipt()
        {
            this.InitializeComponent();
            txtFooter.Text = App.GetAppTextFooter();
            //Load suppliers in ComboBox
            SupplierOptions = new ObservableCollection<SupplierItem>();
            ComboBoxOptionsManager.GetAllSuppliersList(SupplierOptions);
            _SelectedSupplier = SupplierOptions[0];
            SelectedSupplier = SupplierOptions[0];
            //Load packaged materials in ComboBox
            PackagedMaterialOptions = new ObservableCollection<PackagedMaterialItem>();
            ComboBoxOptionsManager.GetEnabledPackMaterialsList(PackagedMaterialOptions);
            _SelectedPackagedMaterial = PackagedMaterialOptions[0];
            SelectedPackagedMaterial = PackagedMaterialOptions[0];
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                encoding = Encoding.GetEncoding("windows-1253");
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleEncodingError"));
            }
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
        }
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            Frame.Navigate(typeof(PageLogin), null, new SuppressNavigationTransitionInfo());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
        }
        public SupplierItem SelectedSupplier
        {
            get
            {
                return _SelectedSupplier;
            }
            set
            {
                if (_SelectedSupplier != value)
                {
                    _SelectedSupplier = value;
                    RaisePropertyChanged("SelectedSupplier");
                    txtBoxLot.Text = string.Empty;
                }
            }
        }
        public PackagedMaterialItem SelectedPackagedMaterial
        {
            get
            {
                return _SelectedPackagedMaterial;
            }
            set
            {
                if (_SelectedPackagedMaterial != value)
                {
                    _SelectedPackagedMaterial = value;
                    RaisePropertyChanged("PackagedMaterialItem");
                    txtBoxLot.Text = string.Empty;
                }
            }
        }
        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private async void BtnPrnt_Click(object sender, RoutedEventArgs e)
        {
            protoWeightLabel = await ApplicationData.Current.LocalFolder.GetFileAsync("Supplier.x");
            //edit label with real dat
            if (txtBoxLot.Text == string.Empty || txtBoxLot.Text == null)
            {
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Resources").GetString("msgLot"), ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleLabelError"));
                return;
            }
            if (protoWeightLabel != null)
            {
                //fill the data
                IBuffer bf = await FileIO.ReadBufferAsync(protoWeightLabel);
                DataReader reader = DataReader.FromBuffer(bf);
                byte[] fileContent = new byte[reader.UnconsumedBufferLength];
                reader.ReadBytes(fileContent);
                string protoVal = encoding.GetString(fileContent, 0, fileContent.Length);
                string newVal = protoVal.Replace("supplierdescr", SelectedSupplier.SupplierDescr);
                newVal = newVal.Replace("grsupplier", SelectedSupplier.GrSupplier);
                newVal = newVal.Replace("lot", txtBoxLot.Text);
                newVal = newVal.Replace("datereceipt", DateTime.Now.ToString("dd-MM-yyyy"));
                newVal = newVal.Replace("nums", printsSpinner.TextValueProperty.ToString());

                dataWeightLabel = await ApplicationData.Current.LocalFolder.CreateFileAsync("Data" + protoWeightLabel.Name, CreationCollisionOption.ReplaceExisting);
                File.WriteAllText(dataWeightLabel.Path, newVal, encoding);
            }
            PrinterUtil.SendToPrinterFile(dataWeightLabel);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }
    }
}
