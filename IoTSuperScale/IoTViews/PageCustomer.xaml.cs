﻿using IoTSuperScale.Core;
using IoTSuperScale.Models;
using IoTSuperScale.DB;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace IoTSuperScale.IoTViews
{
    public sealed partial class PageCustomer : Page
    {
        private ObservableCollection<CustomerItem> CustomerOptions;
        private CustomerItem _SelectedCustomer;
        public event PropertyChangedEventHandler PropertyChanged;
        StorageFile protoWeightLabel;
        StorageFile dataWeightLabel;
        
        public PageCustomer()
        {
            this.InitializeComponent();
            txtFooter.Text = App.GetAppTextFooter();
            //Load Customers in ComboBox
            CustomerOptions = new ObservableCollection<CustomerItem>();
            DBOptionsManager.GetAllCustomersList(CustomerOptions);
            _SelectedCustomer = CustomerOptions[0];
            SelectedCustomer = CustomerOptions[0];
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
        }
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            App.isAuthenticated = false;
            Frame.Navigate(typeof(PageLogin), null, new SuppressNavigationTransitionInfo());
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
        }
        public CustomerItem SelectedCustomer
        {
            get
            {
                return _SelectedCustomer;
            }
            set
            {
                if (_SelectedCustomer != value)
                {
                    _SelectedCustomer = value;
                    RaisePropertyChanged("SelectedCustomer");
                }
            }
        }
        void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private async void BtnPrnt_Click(object sender, RoutedEventArgs e)
        {
            protoWeightLabel = await ApplicationData.Current.LocalFolder.GetFileAsync(@"Labels\Customer.x");
            //edit label with real data
            if (protoWeightLabel != null)
            {
                //fill the data
                IBuffer bf = await FileIO.ReadBufferAsync(protoWeightLabel);
                DataReader reader = DataReader.FromBuffer(bf);
                byte[] fileContent = new byte[reader.UnconsumedBufferLength];
                reader.ReadBytes(fileContent);
                string protoVal = App.encoding.GetString(fileContent, 0, fileContent.Length);
                string newVal = protoVal.Replace("customerdescr", SelectedCustomer.CustomerDescr);
                newVal = newVal.Replace("nums", printsSpinner.TextValueProperty.ToString());

                dataWeightLabel = await ApplicationData.Current.LocalFolder.CreateFileAsync("Data" + protoWeightLabel.Name, CreationCollisionOption.ReplaceExisting);
                File.WriteAllText(dataWeightLabel.Path, newVal, App.encoding);
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
