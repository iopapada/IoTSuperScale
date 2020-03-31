using IoTSuperScale.DB;
using IoTSuperScale.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using static IoTSuperScale.Models.DBinit;

namespace IoTSuperScale.Configuration
{
    static public class Startup
    {
        public static SqlConnection sin;
        static public async Task ConfigureAsync()
        {
            try
            {
                await CopyFilesToLocalState();
                if (SingletonERP.GetERPDbInstance().TestERPDBConnection())
                {
                    //just prepare the singleton object avoiding latency
                    SingletonERP.GetERPDbInstance().GetERPDBConnection();
                    SingletonERP.GetERPDbInstance().CloseERPDBConnection();
                    //sin2 = SingletonMRP.getMRPDbInstance().GetMRPDBConnection();
                    //SingletonMRP.getMRPDbInstance().CloseMRPDBConnection();
                }
                else
                    await LoadModelsFromJSONFilesAsync();

                var items = new List<PackagedMaterialItem>
                {
                    new PackagedMaterialItem() { Code = "000", DisplayCodeDescr = AppSettings.LangConfig == "GR" ? "Καμία επιλογή" : "None selected", MaterialDescr = AppSettings.LangConfig == "GR" ? "Καμία επιλογή" : "None selected", MaterialReadableDescr = AppSettings.LangConfig == "GR" ? "Καμία επιλογή" : "None selected", IsEnabled = true }
                };
                StorageFile materialModel = await ApplicationData.Current.LocalFolder.GetFileAsync(@"JSON_Files\materials.json");
                //string fileString = File.ReadAllText(materialModel.Path);
                string fileString = await FileIO.ReadTextAsync(materialModel);
                items.AddRange(JsonConvert.DeserializeObject<List<PackagedMaterialItem>>(fileString));

                DBOptionsManager.materialsCollection = new ObservableCollection<PackagedMaterialItem>(items.Where(p => p.IsEnabled == true));
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleERPerrorDBConnection"));
            }
        }

        private static async Task LoadModelsFromJSONFilesAsync()
        {
            var suppliersItems = new List<SupplierItem>
            {
                new SupplierItem() { Code = "000", SupplierDescr = AppSettings.LangConfig == "GR" ? "Καμία επιλογή" : "None selected" }
            };
            //DBOptionsManager.materialsCollection = new ObservableCollection<PackagedMaterialItem>();
            StorageFile supplierModel = await ApplicationData.Current.LocalFolder.GetFileAsync(@"JSON_Files\suppliers.json");
            //string fileString = File.ReadAllText(materialModel.Path);
            string fileString1 = await FileIO.ReadTextAsync(supplierModel);
            suppliersItems.AddRange(JsonConvert.DeserializeObject<List<SupplierItem>>(fileString1));

            DBOptionsManager.suppliersCollection = new ObservableCollection<SupplierItem>(suppliersItems);

            var customersItems = new List<CustomerItem>
            {
                new CustomerItem() { Code = "000", CustomerDescr = AppSettings.LangConfig == "GR" ? "Καμία επιλογή" : "None selected" }
            };
            //DBOptionsManager.materialsCollection = new ObservableCollection<PackagedMaterialItem>();
            StorageFile customerModel = await ApplicationData.Current.LocalFolder.GetFileAsync(@"JSON_Files\customers.json");
            //string fileString = File.ReadAllText(materialModel.Path);
            string fileString2 = await FileIO.ReadTextAsync(customerModel);
            customersItems.AddRange(JsonConvert.DeserializeObject<List<CustomerItem>>(fileString2));

            DBOptionsManager.customersCollection = new ObservableCollection<CustomerItem>(customersItems);
        }

        private static async Task CopyFilesToLocalState()
        {
            try
            {
                StorageFolder targetJSONFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("JSON_Files", CreationCollisionOption.OpenIfExists);
                IReadOnlyList<IStorageItem> jsonList = await targetJSONFolder.GetFilesAsync();

                StorageFolder appJSONFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\JsonModels");
                IReadOnlyList<IStorageItem> jsonFiles = await appJSONFolder.GetFilesAsync();

                if (jsonList.Count != jsonFiles.Count)
                {
                    foreach (StorageFile item in jsonFiles)
                    {
                        await item.CopyAsync(targetJSONFolder, item.Name, NameCollisionOption.ReplaceExisting);
                    }
                }

                StorageFolder targetLabelFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Labels", CreationCollisionOption.OpenIfExists);
                IReadOnlyList<IStorageItem> labelsList = await targetLabelFolder.GetFilesAsync();

                StorageFolder appLabelFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\IoTLabels");
                IReadOnlyList<IStorageItem> labels = await appLabelFolder.GetFilesAsync();

                if (labelsList.Count != labels.Count)
                {
                    foreach (StorageFile item in labels)
                    {
                        await item.CopyAsync(targetLabelFolder, item.Name,NameCollisionOption.ReplaceExisting);
                    }
                }

            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titlErrorDuringCopyingFile"));
            }
        }
    }
}
