using IoTSuperScale.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static IoTSuperScale.Models.DBinit;

namespace IoTSuperScale.DB
{
    public class DBOptionsManager
    {
        public static ObservableCollection<PackagedMaterialItem> materialsCollection;
        public static ObservableCollection<SupplierItem> suppliersCollection;
        public static ObservableCollection<CustomerItem> customersCollection;

        public static void GetAllPackMaterialList(ObservableCollection<PackagedMaterialItem> ComboBoxItems)
        {
            ComboBoxItems.Clear();
            materialsCollection.ToList().ForEach(p => ComboBoxItems.Add(p));
        }
        public static void GetEnabledPackMaterialsList(ObservableCollection<PackagedMaterialItem> ComboBoxItems)
        {
            ComboBoxItems.Clear();
            materialsCollection.ToList().ForEach(p => ComboBoxItems.Add(p));
        }
        public static void GetAllSuppliersList(ObservableCollection<SupplierItem> ComboBoxItems)
        {
            var allItems = new List<SupplierItem>
            {
                new SupplierItem("000", AppSettings.LangConfig == "GR" ? "Καμία επιλογή" : "None selected")
            };
            if (SingletonERP.GetERPDbInstance().TestERPDBConnection())
            {
                allItems.AddRange(DBinit.GetAllPrintableSuppliers().ToList());
                ComboBoxItems.Clear();
                allItems.ForEach(p => ComboBoxItems.Add(p));
            }
            else
                suppliersCollection.ToList().ForEach(p => ComboBoxItems.Add(p));
        }
        public static void GetAllCustomersList(ObservableCollection<CustomerItem> ComboBoxItems)
        {
            var allItems = new List<CustomerItem>
            {
                new CustomerItem("000", AppSettings.LangConfig == "GR" ? "Καμία επιλογή" : "None selected")
            };
            if (SingletonERP.GetERPDbInstance().TestERPDBConnection())
            {
                allItems.AddRange(DBinit.GetAllPrintableCustomers().ToList());
                ComboBoxItems.Clear();
                allItems.ForEach(p => ComboBoxItems.Add(p));
            }
            else
                customersCollection.ToList().ForEach(p => ComboBoxItems.Add(p));
        }
        public static ObservableCollection<LotItem> GetLotsOfProduct(string MaterialCode)
        {
            var allItems = new ObservableCollection<LotItem>();
            if (SingletonERP.GetERPDbInstance().TestERPDBConnection())
                return DBinit.GetLotsOfProduct(MaterialCode);
            return allItems;
            
        }
        
    }
}
