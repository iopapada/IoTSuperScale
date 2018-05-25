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
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgReceiptEncoding"));
            }

        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            Frame.Navigate(typeof(PageLogin), null);
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
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

        private async void btnPrnt_Click(object sender, RoutedEventArgs e)
        {
            protoWeightLabel = await ApplicationData.Current.LocalFolder.GetFileAsync("Supplier.x");
            //edit label with real dat
            if (txtBoxLot.Text == string.Empty || txtBoxLot.Text == null)
            {
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgLot"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgLabelErrorTitle"));
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
                string newVal = protoVal.Replace("supplierdescr", SelectedSupplier.supplierDescr);
                newVal = newVal.Replace("grsupplier", SelectedSupplier.grSupplier);
                newVal = newVal.Replace("lot", txtBoxLot.Text);
                newVal = newVal.Replace("datereceipt", DateTime.Now.ToString("dd-MM-yyyy"));
                newVal = newVal.Replace("nums", printsSpinner.TextValueProperty.ToString());

                dataWeightLabel = await ApplicationData.Current.LocalFolder.CreateFileAsync("Data" + protoWeightLabel.Name, CreationCollisionOption.ReplaceExisting);
                File.WriteAllText(dataWeightLabel.Path, newVal, encoding);
            }
            PrinterUtil.sendToPrinterFile(dataWeightLabel);
        }
    }
}
