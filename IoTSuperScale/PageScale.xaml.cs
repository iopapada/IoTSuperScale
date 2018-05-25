using IoTSuperScale.IoTCore;
using IoTSuperScale.IoTDB;
using IoTSuperScale.IoTViews;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Collections.ObjectModel;
using System.IO;
using System;
using Windows.UI;
using System.Threading;
using System.Resources;
using System.Reflection;
using Windows.ApplicationModel.Resources;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTSuperScale
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageScale : Page, INotifyPropertyChanged
    {
        //Helper UI values
        object previousTouch1 = null;
        object previousTouch2 = null;
        object currentTouch = null;
        public DispatcherTimer scaleTimer;
        //Helper values
        public static string lastWeightValue;
        public static double tareweight;
        private ObservableCollection<PackagedMaterialItem> MaterialOptions;
        private ObservableCollection<SupplierItem> SupplierOptions;
        public ObservableCollection<LotItem> LotOptions;

        //private ObservableCollection<LotItem> _LotOptions;
        private PackagedMaterialItem _SelectedMaterial;
        private SupplierItem _SelectedSupplier;
        private LotItem _SelectedLot;

        public event PropertyChangedEventHandler PropertyChanged;
        StorageFile currentLabel;
        StorageFile protoWeightLabel;
        StorageFile protoMaterialLabel;
        StorageFile dataWeightLabel;
        //Printer values
        int step;
        double sum;

        public PageScale()
        {
            try
            {
                this.InitializeComponent();
                txtScaleName.Text = AppSettings.ScaleName+" ("+AppSettings.LCcapacity+")";
                txtFooter.Text = App.GetAppTextFooter();
               
                //Start scale timer tick
                scaleTimer = new DispatcherTimer();
                scaleTimer.Interval = TimeSpan.FromMilliseconds(AppSettings.ScaleTimer);
                scaleTimer.Tick += Timer_Tick;
                scaleTimer.Start();
                //By authenticated access
                if (App.isAuthenticated)
                {
                    //Check printer settings
                    if (AppSettings.SumPrints > 1)
                    {
                        step = 0;
                        sum = 0;
                        txtSum.Text = step.ToString() + " - " + ResourceLoader.GetForCurrentView().GetString("lblTotal") + sum.ToString();
                    }
                    DisplayUtilities();
                    //Load materials in ComboBox
                    MaterialOptions = new ObservableCollection<PackagedMaterialItem>();
                    ComboBoxOptionsManager.GetEnabledPackMaterialsList(MaterialOptions);
                    _SelectedMaterial = MaterialOptions[0];
                    SelectedMaterial = MaterialOptions[0];
                    //Load empty lot
                    LotOptions = new ObservableCollection<LotItem>();
                    InsertEmptyLot();
                    RaisePropertyChanged("LotOptions");
                    _SelectedLot = LotOptions[0];
                    SelectedLot = LotOptions[0];
                    //Load suppliers in ComboBox
                    SupplierOptions = new ObservableCollection<SupplierItem>();
                    ComboBoxOptionsManager.GetAllSuppliersList(SupplierOptions);
                    _SelectedSupplier = SupplierOptions[0];
                    SelectedSupplier = SupplierOptions[0];
                    //Load some labels
                    LoadLabelsFiles();
                    //In case we want to save the state of pagescale
                    //NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
                }
                else
                    HideUtilities();
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgErrorOnLoadScale"));
            }
        }
        private async void LoadLabelsFiles()
        {
            try
            {
                protoMaterialLabel = await ApplicationData.Current.LocalFolder.GetFileAsync("Material.x");
                protoWeightLabel = await ApplicationData.Current.LocalFolder.GetFileAsync("WeightMaterial.x");
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgNonExistLabelsTitle"));
            }
        }
        #region UI functions & bar buttons
        private void DisplayUtilities()
        {
            btnBack.Visibility = Visibility.Visible;
            btnLogOut.Visibility = Visibility.Visible;
            border1.Visibility = Visibility.Visible;
            border1_Copy.Visibility = Visibility.Visible;
            border1_Copy1.Visibility = Visibility.Visible;

            lblSup.Visibility = Visibility.Visible;
            CBoxSuppliers.Visibility = Visibility.Visible;
            lblLOT.Visibility = Visibility.Visible;
            CBoxLotNums.Visibility = Visibility.Visible;
            lblPRINTS.Visibility = Visibility.Visible;
            printsSpinner.Visibility = Visibility.Visible;

            lblMaterialDescription.Visibility = Visibility.Visible;
            txtDescr.Visibility = Visibility.Visible;
            CBoxMaterials.Visibility = Visibility.Visible;
            txtNetWeightBorder.Visibility = Visibility.Visible;
            lblNetW.Visibility = Visibility.Visible;
            txtNetW.Visibility = Visibility.Visible;
            qtySpinner.Visibility = Visibility.Visible;

            btnTare.Visibility = Visibility.Visible;
            btnPrnt.Visibility = Visibility.Visible;
        }
        private void HideUtilities()
        {
            btnBack.Visibility = Visibility.Collapsed;
            btnLogOut.Visibility = Visibility.Collapsed;
            border1.Visibility = Visibility.Collapsed;
            border1_Copy.Visibility = Visibility.Collapsed;
            border1_Copy1.Visibility = Visibility.Collapsed;

            lblSup.Visibility = Visibility.Collapsed;
            CBoxSuppliers.Visibility = Visibility.Collapsed;
            lblLOT.Visibility = Visibility.Collapsed;
            CBoxLotNums.Visibility = Visibility.Collapsed;
            lblPRINTS.Visibility = Visibility.Collapsed;
            printsSpinner.Visibility = Visibility.Collapsed;

            lblMaterialDescription.Visibility = Visibility.Collapsed;
            txtDescr.Visibility = Visibility.Collapsed;
            CBoxMaterials.Visibility = Visibility.Collapsed;
            txtNetWeightBorder.Visibility = Visibility.Collapsed;
            lblNetW.Visibility = Visibility.Collapsed;
            txtNetW.Visibility = Visibility.Collapsed;
            qtySpinner.Visibility = Visibility.Collapsed;

            btnTare.Visibility = Visibility.Collapsed;
            //btnZero.Visibility = Visibility.Collapsed;
            btnPrnt.Visibility = Visibility.Collapsed;
        }
        private void Page_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            currentTouch = e.OriginalSource;
            if ((currentTouch is Image) && (previousTouch1 is TextBlock) && (previousTouch2 is Image))
                btnLogOut_Click(null, null);

            previousTouch2 = previousTouch1;
            previousTouch1 = currentTouch;
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            Frame.Navigate(typeof(PageLogin), null);
            scaleTimer.Stop();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
            scaleTimer.Stop();
        }
        #endregion
        private void Timer_Tick(object sender, object e)
        {
            try
            {
                txtWeight.Text = App.s.GetReading();
                txtRv.Text = App.s.lastOutput.ToString();
                //calculation net weight
                if (App.isAuthenticated && !SelectedMaterial.code.Equals("000") && !txtWeight.Text.Equals(App.s.zeroPointString))
                {
                    txtNetW.Text = calculateNetW(App.s.finalDigitVal, SelectedMaterial.tarePack, SelectedMaterial.tarePrecentage, Int32.Parse(qtySpinner.TextValueProperty));
                    currentLabel = protoWeightLabel;
                }
                else
                {
                    txtNetW.Text = App.s.zeroPointString;
                    currentLabel = protoMaterialLabel;
                }

                onChangeWeightDigit();
                lastWeightValue = txtWeight.Text.ToString();
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgErrorScaleValues"));
            }
        }
        private string calculateNetW(double weight, double tareweight, double precentage, int qty)
        {
            if (weight <= 0)
                return App.s.zeroPointString;
            double w = (weight - (tareweight * qty)) * (1 - precentage);
            w = Math.Round(w, 1);
            return w.ToString() + AppSettings.TrailingUnit;
        }
        private void equalityOnFirstDec()
        {
            txtWeightBorder.Background = new SolidColorBrush(Color.FromArgb(191, 0, 245, 56));
        }
        private void onChangeWeightDigit()
        {
            if (txtWeight.Text.ToString().Equals(lastWeightValue))
                txtWeightBorder.Style = Resources["NeutralTextBoxStyle"] as Style;
            //txtWeightBorder.Background = new SolidColorBrush(Color.FromArgb(191, 0, 245, 56));
            else
                txtWeightBorder.Style = Resources["WarnTextBoxStyle"] as Style;
            //txtWeightBorder.Background = new SolidColorBrush(Color.FromArgb(191, 245, 43, 43));
        }
        private void btnTare_Click(object sender, RoutedEventArgs e)
        {
            scaleTimer.Stop();
            Frame.Navigate(typeof(PageScreenSaver), null);
            //tareweight = App.s.finalDigitVal;
        }
        private void btnZero_Click(object sender, RoutedEventArgs e)
        {
            txtWeight.Text = App.s.ZeroWithoutPrework();
        }
        private async void btnPrnt_Click(object sender, RoutedEventArgs e)
        {
            //edit label with real data
            if (SelectedSupplier.code == "000" && (SelectedMaterial.type == PackagedMaterialItem.materialType.BIO || SelectedMaterial.type == PackagedMaterialItem.materialType.SEMIBIO))
            {
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgGrSupplier"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgLabelErrorTitle"));
                return;
            }
            if (SelectedLot.Code == "000")
            {
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgLot"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgLabelErrorTitle"));
                return;
            }
            //Check printer settings
            if (AppSettings.SumPrints > 1 && step <= AppSettings.SumPrints)
            {
                step++;
                sum += Double.Parse(txtNetW.Text.Remove(txtNetW.Text.Length - 3, 3));
                txtSum.Text = step.ToString() + " - " + ResourceLoader.GetForCurrentView().GetString("lblTotal") + sum.ToString() + AppSettings.TrailingUnit;
            }

            if (currentLabel != null)
            {
                //copy label file to public folder for editing rights
                //await protoWeightLabel.CopyAsync(ApplicationData.Current.LocalFolder, "WeightData.x",NameCollisionOption.ReplaceExisting);

                //fill the data
                IBuffer bf = await FileIO.ReadBufferAsync(currentLabel);
                DataReader reader = DataReader.FromBuffer(bf);
                byte[] fileContent = new byte[reader.UnconsumedBufferLength];
                reader.ReadBytes(fileContent);
                string protoVal = App.encoding.GetString(fileContent, 0, fileContent.Length);
                string newVal = protoVal.Replace("materialdescr", SelectedMaterial.materialReadableDescr);
                newVal = newVal.Replace("country", SelectedMaterial.country);
                newVal = newVal.Replace("region", SelectedMaterial.region);
                newVal = newVal.Replace("grsupplier", SelectedSupplier.grSupplier);
                newVal = newVal.Replace("category", SelectedMaterial.category);
                newVal = newVal.Replace("variety", SelectedMaterial.variety);
                //if(SelectedMaterial.type == PackagedMaterialItem.materialType.BIO || SelectedMaterial.type == PackagedMaterialItem.materialType.SEMIBIO)
                newVal = newVal.Replace("datereceipt", DateTime.Now.ToString("dd-MM-yyyy"));
                newVal = newVal.Replace("weightval", txtNetW.Text);
                newVal = newVal.Replace("lot", SelectedLot.Code);
                newVal = newVal.Replace("nums", printsSpinner.TextValueProperty.ToString());

                dataWeightLabel = await ApplicationData.Current.LocalFolder.CreateFileAsync("Data" + currentLabel.Name, CreationCollisionOption.ReplaceExisting);
                File.WriteAllText(dataWeightLabel.Path, newVal, App.encoding);
                //StorageFolder publicFolder = ApplicationData.Current.LocalFolder;
                //dataLabelFile = await publicFolder.GetFileAsync("WeightData.x");
            }
            PrinterUtil.sendToPrinterFile(dataWeightLabel);
            if (step == AppSettings.SumPrints)
            {
                Thread.Sleep(500);
                PrinterUtil.sendTestToPrinter(sum.ToString() + AppSettings.TrailingUnit, AppSettings.CopiesPrints.ToString());
                AppSettings.CopiesPrints = 1;
                AppSettings.SumPrints = 1;
                txtSum.Text = String.Empty;
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
                    //Load lots of selected material
                    LotOptions = new ObservableCollection<LotItem>();
                    LotOptions = DBinit.GetLotsOfProduct(AppSettings.ConnectionString, SelectedMaterial.code);
                    InsertEmptyLot();
                    RaisePropertyChanged("LotOptions");
                    SelectedLot = LotOptions[0];
                    //Load suppliers 
                    SelectedSupplier = SupplierOptions[0];
                    ChangeSelectedLabel();
                }
            }
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
                }
            }
        }
        public LotItem SelectedLot
        {
            get
            {
                return _SelectedLot;
            }
            set
            {
                if (_SelectedLot != value)
                {
                    _SelectedLot = value;
                    RaisePropertyChanged("SelectedLot");
                }
            }
        }

        private void InsertEmptyLot() {
            LotItem initLot = new LotItem();
            initLot.Code = "Καμία επιλογή";
            initLot.Qty1 = 0;
            initLot.Qty2 = 0;
            LotOptions.Insert(0, initLot);
        }
        private async void ChangeSelectedLabel()
        {
            try
            {
                if (SelectedMaterial.type == PackagedMaterialItem.materialType.BIO)
                {
                    if(SelectedMaterial.isEEcountry==true)
                        currentLabel = await ApplicationData.Current.LocalFolder.GetFileAsync("Bio.x");
                    else
                        currentLabel = await ApplicationData.Current.LocalFolder.GetFileAsync("BioOutEE.x");
                }
                else if (SelectedMaterial.type == PackagedMaterialItem.materialType.SEMIBIO)
                    currentLabel = await ApplicationData.Current.LocalFolder.GetFileAsync("SemiBio.x");
                else if (SelectedMaterial.type == PackagedMaterialItem.materialType.CONVENTIONAL)
                    currentLabel = await ApplicationData.Current.LocalFolder.GetFileAsync("Material.x");
            }
            catch (Exception)
            {
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgNonExistLabels"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgLabelErrorTitle"));
            }
        }
        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private void CBoxMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBoxMaterials.SelectedIndex > 0)
            {
                btnPrnt.IsEnabled = true;
                printsSpinner.IsEnabled = true;
                CBoxLotNums.IsEnabled = true;
                CBoxSuppliers.IsEnabled = true;
                qtySpinner.TextValueProperty = "1";
                printsSpinner.TextValueProperty = "1";
            }
            else
            {
                btnPrnt.IsEnabled = false;
                printsSpinner.IsEnabled = false;
                CBoxLotNums.IsEnabled = false;
                CBoxSuppliers.IsEnabled = false;
                qtySpinner.TextValueProperty = "1";
                printsSpinner.TextValueProperty = "1";
            }
        }
    }
}
