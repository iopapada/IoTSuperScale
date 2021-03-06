﻿using IoTSuperScale.Core;
using IoTSuperScale.Models;
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
using Windows.UI.Xaml.Media.Animation;
using IoTSuperScale.DB;

namespace IoTSuperScale
{
    public sealed partial class PageScale : Page, INotifyPropertyChanged
    {
        //Helper UI values
        public static DispatcherTimer scaleTimer;
        //Helper values
        public static double tareweight;
        private ObservableCollection<PackagedMaterialItem> MaterialOptions;
        private ObservableCollection<SupplierItem> SupplierOptions;
        public ObservableCollection<LotItem> LotOptions;
        private PackagedMaterialItem _SelectedMaterial;
        private SupplierItem _SelectedSupplier;
        private LotItem _SelectedLot;
        public event PropertyChangedEventHandler PropertyChanged;
        StorageFile currentLabel;
        StorageFile dataLabel;
        
        //Printer values
        int step;
        int pallet;
        double sum;
        //Scale values
        private double lastFV = -1;
        private double bLastFV = -1;
        private double FV = -1;

        public PageScale()
        {
            try
            {
                this.InitializeComponent();
                txtScaleName.Text = AppSettings.ScaleName+" ("+AppSettings.LCcapacity+")";
                txtFooter.Text = App.GetAppTextFooter();
                //IoTCore.SerialCOM.SerialConfig();
                string temp = App.s.CreateZeroPoint();
                double zeroPoint = Double.Parse(temp);
                App.s.zeroPointString = temp + AppSettings.TrailingUnit;

                SetupScaleTimer();
             
                //By authenticated access
                if (App.isAuthenticated)
                {
                    //Check printer settings
                    if (AppSettings.SumPrints > 1)
                    {
                        step = 0;
                        sum = 0;
                        pallet = 0;
                        txtSum.Text = ResourceLoader.GetForCurrentView().GetString("lblPallet") + pallet.ToString() + " - " + ResourceLoader.GetForCurrentView().GetString("Step") + step.ToString()+"/"+AppSettings.SumPrints+ " - " + ResourceLoader.GetForCurrentView().GetString("lblTotal") + sum.ToString() + AppSettings.TrailingUnit;
                    }
                    DisplayUtilities();
                    //Load materials in ComboBox
                    MaterialOptions = new ObservableCollection<PackagedMaterialItem>();
                    DBOptionsManager.GetEnabledPackMaterialsList(MaterialOptions);
                    _SelectedMaterial = MaterialOptions[0];
                    SelectedMaterial = MaterialOptions[0];
                    //Load empty lot
                    LotOptions = new ObservableCollection<LotItem>();
                    RaisePropertyChanged("LotOptions");
                    //Load suppliers in ComboBox
                    SupplierOptions = new ObservableCollection<SupplierItem>();
                    DBOptionsManager.GetAllSuppliersList(SupplierOptions);
                    _SelectedSupplier = SupplierOptions[0];
                    SelectedSupplier = SupplierOptions[0];
                    //Load some labels
                    LoadLabelsFiles();
                    //we want to save the state of pagescale
                    NavigationCacheMode = NavigationCacheMode.Required;
                }
                else
                    HideUtilities();
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleErrorOnLoadScale"));
            }
        }

        private async void LoadLabelsFiles()
        {
            try
            {
                currentLabel = await ApplicationData.Current.LocalFolder.GetFileAsync(@"Labels\Material.x");
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleNonExistLabels"));
            }
        }

    #region UI functions & bar buttons
    private void DisplayUtilities()
        {
            btnBack.Visibility = Visibility.Visible;
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

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            scaleTimer.Stop();
            NavigationCacheMode = NavigationCacheMode.Disabled;
            Frame.Navigate(typeof(PageLogin), null, new SuppressNavigationTransitionInfo());
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationCacheMode = NavigationCacheMode.Disabled;
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
                //txtWeight.Text = IoTCore.SerialCOM.SerialReadAsync().ToString();
                txtRv.Text = App.s.voltOutput.ToString();
                //calculation net weight
                if (App.isAuthenticated && !SelectedMaterial.Code.Equals("000") && !txtWeight.Text.Equals(App.s.zeroPointString))
                    txtNetW.Text = CalculateNetW(App.s.finalDigitVal, SelectedMaterial.TarePack, SelectedMaterial.TarePrecentage, Int32.Parse(qtySpinner.TextValueProperty));
                else
                    txtNetW.Text = App.s.zeroPointString;
                //Repaint weight indicator
                OnChangeWeightDigit();
                //Broadcast scale value only when we have real weigth and only three attempt for zero values
                bLastFV = lastFV;
                lastFV = FV;
                FV = App.s.finalDigitVal;
                if (!(FV == 0 && bLastFV == 0 && lastFV == 0) && AppSettings.BroadcastPcksConfig)
                    App.s.BroadcastScaleVal(App.s.finalStringVal);
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleErrorScaleValues"));
            }
        }
        private string CalculateNetW(double weight, double tareweight, double precentage, int qty)
        {
            if (weight <= 0)
                return App.s.zeroPointString;
            double w = (weight - (tareweight * qty)) * (1 - precentage);
            w = Math.Round(w, 1);
            return w.ToString() + AppSettings.TrailingUnit;
        }
        private void EqualityOnFirstDec()
        {
            txtWeightBorder.Background = new SolidColorBrush(Color.FromArgb(191, 0, 245, 56));
        }
        private void OnChangeWeightDigit()
        {
            if (FV == lastFV)
                txtWeightBorder.Style = Resources["NeutralTextBoxStyle"] as Style;
            //txtWeightBorder.Background = new SolidColorBrush(Color.FromArgb(191, 0, 245, 56));
            else
                txtWeightBorder.Style = Resources["WarnTextBoxStyle"] as Style;
            //txtWeightBorder.Background = new SolidColorBrush(Color.FromArgb(191, 245, 43, 43));
        }
        private void BtnTare_Click(object sender, RoutedEventArgs e)
        {
            //tareweight = App.s.finalDigitVal;
        }
        private void BtnZero_Click(object sender, RoutedEventArgs e)
        {
            txtWeight.Text = App.s.ZeroWithoutPrework();
        }
        private async void BtnPrnt_Click(object sender, RoutedEventArgs e)
        {
            //edit label with real data
            if (SelectedSupplier.Code == "000" && (SelectedMaterial.Type == PackagedMaterialItem.MaterialType.BIO || SelectedMaterial.Type == PackagedMaterialItem.MaterialType.SEMIBIO))
            {
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Resources").GetString("msgGrSupplier"), ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleLabelError"));
                return;
            }
            if (String.IsNullOrEmpty(CBoxLotNums.Text) || String.IsNullOrEmpty(SelectedLot.Code))
            {
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Resources").GetString("msgLot"), ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleLabelError"));
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
                string newVal = protoVal.Replace("materialdescr", SelectedMaterial.MaterialReadableDescr);
                newVal = newVal.Replace("country", SelectedMaterial.Country);
                newVal = newVal.Replace("region", SelectedMaterial.Region);
                newVal = newVal.Replace("grsupplier", SelectedSupplier.GrSupplier+SelectedMaterial.GRmaterial);
                newVal = newVal.Replace("category", SelectedMaterial.Category);
                newVal = newVal.Replace("variety", SelectedMaterial.Variety);
                newVal = newVal.Replace("datereceipt", DateTime.Now.ToString("dd-MM-yyyy"));
                if (Decimal.Parse(txtNetW.Text.Substring(0,txtNetW.Text.Length-3))>0)
                {
                    newVal = newVal.Replace("descrweightval", "Καθαρό Βάρος:");
                    newVal = newVal.Replace("weightval", txtNetW.Text);
                }
                else
                {
                    newVal = newVal.Replace("descrweightval", " ");
                    newVal = newVal.Replace("weightval", " ");
                }
                newVal = newVal.Replace("lot", SelectedLot.Code);
                newVal = newVal.Replace("nums", printsSpinner.TextValueProperty.ToString());

                dataLabel = await ApplicationData.Current.LocalFolder.CreateFileAsync("Data" + currentLabel.Name, CreationCollisionOption.ReplaceExisting);
                File.WriteAllText(dataLabel.Path, newVal, App.encoding);
                //StorageFolder publicFolder = ApplicationData.Current.LocalFolder;
                //dataLabelFile = await publicFolder.GetFileAsync("WeightData.x");
            }
            PrinterUtil.SendToPrinterFile(dataLabel);
            if (step == AppSettings.SumPrints)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                //Thread.Sleep(2000);
                if ((pallet+1) == AppSettings.PalletsNum)
                {
                    //print last pallet sum
                    PrinterUtil.SendTestToPrinter(sum.ToString() + AppSettings.TrailingUnit, AppSettings.CopiesPrints.ToString());
                    AppSettings.CopiesPrints = 1;
                    AppSettings.SumPrints = 1;
                    AppSettings.PalletsNum = 1;
                    txtSum.Text = String.Empty;
                }
                else
                {
                    PrinterUtil.SendTestToPrinter(sum.ToString() + AppSettings.TrailingUnit, AppSettings.CopiesPrints.ToString());
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
                    LotOptions = DBOptionsManager.GetLotsOfProduct(SelectedMaterial.Code);
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
                if (SelectedMaterial.Type == PackagedMaterialItem.MaterialType.BIO)
                {
                    if(SelectedMaterial.IsEEcountry==true)
                        currentLabel = await ApplicationData.Current.LocalFolder.GetFileAsync(@"Labels\Bio.x");
                    else
                        currentLabel = await ApplicationData.Current.LocalFolder.GetFileAsync(@"Labels\BioOutEE.x");
                }
                else if (SelectedMaterial.Type == PackagedMaterialItem.MaterialType.SEMIBIO)
                    currentLabel = await ApplicationData.Current.LocalFolder.GetFileAsync(@"Labels\SemiBio.x");
                else if (SelectedMaterial.Type == PackagedMaterialItem.MaterialType.CONVENTIONAL)
                    currentLabel = await ApplicationData.Current.LocalFolder.GetFileAsync(@"Labels\Material.x");
            }
            catch (Exception)
            {
                App.PrintOkMessage(ResourceLoader.GetForViewIndependentUse("Resources").GetString("msgNonExistLabels"), ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleLabelError"));
            }
        }
        void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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
                CBoxLotNums.Text = chsLotItem.Code;
            }
            else
            {
                var matchingLot = LotOptions.ToList().Where(c => c.Code.IndexOf(sender.Text, StringComparison.CurrentCultureIgnoreCase) > -1).OrderByDescending(c => c.Code.StartsWith(sender.Text, StringComparison.CurrentCultureIgnoreCase));
                if (matchingLot.Count() >= 1)
                    CBoxLotNums.Text = matchingLot.FirstOrDefault().Code;
            }
        }
        private void CBoxLotNums_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            SelectedLot = args.SelectedItem as LotItem;
            sender.Text = SelectedLot.Code;
        }
        private void CBoxLotNums_LostFocus(object sender, RoutedEventArgs e)
        {
            SelectedLot.Code = CBoxLotNums.Text;
        }
        private void CBoxLotNums_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
            SelectedLot = new LotItem();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.Current.IsIdleChanged += OnIsIdleChanged;
            if (scaleTimer != null)
                scaleTimer.Start();
            else
                SetupScaleTimer();
        }
        private void SetupScaleTimer()
        {
            //Start scale timer tick
            scaleTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(AppSettings.ScaleTimer)
            };
            scaleTimer.Tick += Timer_Tick;
            scaleTimer.Start();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.Current.IsIdleChanged -= OnIsIdleChanged;
            scaleTimer.Stop();
        }
        private void OnIsIdleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
