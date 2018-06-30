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
using Windows.ApplicationModel.Resources;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using static IoTSuperScale.IoTDB.DBinit;
using System.Data.SqlClient;

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
        public static DispatcherTimer scaleTimer;
        //Helper values
        public static string lastWeightValue;
        public static double tareweight;
        private ObservableCollection<PackagedMaterialItem> MaterialOptions;
        private ObservableCollection<SupplierItem> SupplierOptions;
        public ObservableCollection<LotItem> LotOptions;
        private PackagedMaterialItem _SelectedMaterial;
        private SupplierItem _SelectedSupplier;
        private LotItem _SelectedLot;
        public event PropertyChangedEventHandler PropertyChanged;
        StorageFile currentLabel;
        StorageFile protoWeightLabel;
        StorageFile protoMaterialLabel;
        StorageFile dataWeightLabel;
        public static SqlConnection sin;
        //Printer values
        int step;
        int pallet;
        double sum;

        public PageScale()
        {
            try
            {
                this.InitializeComponent();
                txtScaleName.Text = AppSettings.ScaleName+" ("+AppSettings.LCcapacity+")";
                txtFooter.Text = App.GetAppTextFooter();

                string temp = App.s.createZeroPoint();
                double zeroPoint = Double.Parse(temp);
                App.s.zeroPointString = temp + AppSettings.TrailingUnit;

                //Start scale timer tick
                scaleTimer = new DispatcherTimer();
                scaleTimer.Interval = TimeSpan.FromMilliseconds(AppSettings.ScaleTimer);
                scaleTimer.Tick += Timer_Tick;
                scaleTimer.Start();
                //just prepare the singleton object avoiding latency
                try
                {
                    sin = SingletonERP.getERPDbInstance().GetERPDBConnection();
                    SingletonERP.getERPDbInstance().CloseERPDBConnection();
                    //sin2 = SingletonMRP.getMRPDbInstance().GetMRPDBConnection();
                    //SingletonMRP.getMRPDbInstance().CloseMRPDBConnection();
                }
                catch (Exception ex)
                {
                    App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleERPerrorDBConnection"));
                }
                //By authenticated access
                if (App.isAuthenticated)
                {
                    //Check printer settings
                    if (AppSettings.SumPrints > 1)
                    {
                        step = 0;
                        sum = 0;
                        pallet = 1;
                        txtSum.Text = ResourceLoader.GetForCurrentView().GetString("lblPallet") + pallet.ToString() + " - " + ResourceLoader.GetForCurrentView().GetString("Step") + step.ToString()+"/"+AppSettings.SumPrints+ " - " + ResourceLoader.GetForCurrentView().GetString("lblTotal") + sum.ToString() + AppSettings.TrailingUnit;
                    }
                    DisplayUtilities();
                    //Load materials in ComboBox
                    MaterialOptions = new ObservableCollection<PackagedMaterialItem>();
                    ComboBoxOptionsManager.GetEnabledPackMaterialsList(MaterialOptions);
                    _SelectedMaterial = MaterialOptions[0];
                    SelectedMaterial = MaterialOptions[0];
                    //Load empty lot
                    LotOptions = new ObservableCollection<LotItem>();
                    RaisePropertyChanged("LotOptions");
                    //Load suppliers in ComboBox
                    SupplierOptions = new ObservableCollection<SupplierItem>();
                    ComboBoxOptionsManager.GetAllSuppliersList(SupplierOptions);
                    _SelectedSupplier = SupplierOptions[0];
                    SelectedSupplier = SupplierOptions[0];
                    //Load some labels
                    LoadLabelsFiles();
                    //we want to save the state of pagescale
                    NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
                }
                else
                    HideUtilities();
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleErrorOnLoadScale"));
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
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleNonExistLabels"));
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
            scaleTimer.Stop();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            Frame.Navigate(typeof(PageLogin), null);
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            scaleTimer.Stop();
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
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
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleErrorScaleValues"));
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
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgGrSupplier"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleLabelError"));
                return;
            }
            if (String.IsNullOrEmpty(SelectedLot.Code.ToString()))
            {
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgLot"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleLabelError"));
                return;
            }
            //Check printer settings
            if (AppSettings.SumPrints > 1 || AppSettings.PalletsNum > 1)
            {
                step++;
                sum += Double.Parse(txtNetW.Text.Remove(txtNetW.Text.Length - 3, 3));
                txtSum.Text = ResourceLoader.GetForCurrentView().GetString("lblPallet") + pallet.ToString() + " - " + ResourceLoader.GetForCurrentView().GetString("Step") + step.ToString() + "/" + AppSettings.SumPrints + " - " + ResourceLoader.GetForCurrentView().GetString("lblTotal") + sum.ToString() + AppSettings.TrailingUnit;
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
                await Task.Delay(TimeSpan.FromSeconds(2));
                //Thread.Sleep(2000);
                if (pallet == AppSettings.PalletsNum)
                {
                    AppSettings.CopiesPrints = 1;
                    AppSettings.SumPrints = 1;
                    AppSettings.PalletsNum = 1;
                    txtSum.Text = String.Empty;
                }
                else
                {
                    PrinterUtil.sendTestToPrinter(sum.ToString() + AppSettings.TrailingUnit, AppSettings.CopiesPrints.ToString());
                    pallet++;
                    step = 0;
                    sum = 0;
                    txtSum.Text = ResourceLoader.GetForCurrentView().GetString("lblPallet") + pallet.ToString() + " - " + ResourceLoader.GetForCurrentView().GetString("Step") + step.ToString() + "/" + AppSettings.SumPrints + " - " + ResourceLoader.GetForCurrentView().GetString("lblTotal") + sum.ToString() + AppSettings.TrailingUnit;
                }
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
                    CBoxLotNums.Text = "";
                    LotOptions = new ObservableCollection<LotItem>();
                    LotOptions = DBinit.GetLotsOfProduct(SelectedMaterial.code);
                    RaisePropertyChanged("LotOptions");
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
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgNonExistLabels"), ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleLabelError"));
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
        private void CBoxLotNums_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) {
                var matchingLot = LotOptions.ToList().Where(c => c.Code.IndexOf(sender.Text, StringComparison.CurrentCultureIgnoreCase) > -1).OrderByDescending(c => c.Code.StartsWith(sender.Text, StringComparison.CurrentCultureIgnoreCase));
                sender.ItemsSource = matchingLot.ToList();
            }
        }
        private void CBoxLotNums_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                LotItem chsLotItem = args.ChosenSuggestion as LotItem;
                CBoxLotNums.Text = chsLotItem.GetLot;
            }
            else
            {
                var matchingLot = LotOptions.ToList().Where(c => c.Code.IndexOf(sender.Text, StringComparison.CurrentCultureIgnoreCase) > -1).OrderByDescending(c => c.Code.StartsWith(sender.Text, StringComparison.CurrentCultureIgnoreCase));
                if (matchingLot.Count() >= 1)
                    CBoxLotNums.Text = matchingLot.FirstOrDefault().GetLot;
            }
        }
        private void CBoxLotNums_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            LotItem selectedLot = args.SelectedItem as LotItem;
            sender.Text = selectedLot.GetLot;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.Current.IsIdleChanged += onIsIdleChanged;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.Current.IsIdleChanged -= onIsIdleChanged;
            scaleTimer.Stop();
        }
        private void onIsIdleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
