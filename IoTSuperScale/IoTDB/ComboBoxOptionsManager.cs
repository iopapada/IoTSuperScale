using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IoTSuperScale.IoTDB
{
    public class ComboBoxOptionsManager
    {
        public static void GetAllPackMaterialList(ObservableCollection<PackagedMaterialItem> ComboBoxItems)
        {
            var allItems = getPackagedMaterialItems();
            ComboBoxItems.Clear();
            allItems.ForEach(p => ComboBoxItems.Add(p));
        }
        public static void GetAllSppliersList(ObservableCollection<SupplierItem> ComboBoxItems)
        {
            var allItems = getSupplierItems();
            ComboBoxItems.Clear();
            allItems.ForEach(p => ComboBoxItems.Add(p));
        }
        public static void GetAllCustomersList(ObservableCollection<CustomerItem> ComboBoxItems)
        {
            var allItems = getCustomerItems();
            ComboBoxItems.Clear();
            allItems.ForEach(p => ComboBoxItems.Add(p));
        }
        public static void GetEnabledPackMaterialsList(ObservableCollection<PackagedMaterialItem> ComboBoxItems)
        {
            var allItems = getPackagedMaterialItems().Where(p => p.isEnabled == true);
            ComboBoxItems.Clear();
            allItems.ToList().ForEach(p => ComboBoxItems.Add(p));
        }

        private static List<PackagedMaterialItem> getPackagedMaterialItems()
        {
            var items = new List<PackagedMaterialItem>();
            items.Add(new PackagedMaterialItem() { code = "000", displayCodeDescr = "Καμία επιλογή", materialDescr = "Καμία επιλογή", materialReadableDescr = "Καμία επιλογή", isEnabled=true });
            items.Add(new PackagedMaterialItem() { code = "001", displayCodeDescr = "001 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΠΡΟΙΟΝ ΒΙΟΛΟΓΙΚΗΣ ΓΕΩΡΓΙΑΣ", materialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΠΡΟΙΟΝ ΒΙΟΛΟΓΙΚΗΣ ΓΕΩΡΓΙΑΣ", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΠΡΟΙΟΝ ΒΙΟΛΟΓΙΚΗΣ ΓΕΩΡΓΙΑΣ", country = "ΕΛΛΑΔΑ", region = "ΑΡΒΗ-ΚΡΗΤΗ", recipe = null, type = PackagedMaterialItem.materialType.BIO, isEnabled = true, tarePack = 0, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "002", displayCodeDescr = "002 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΥΠΟ ΜΕΤΑΤΡΟΠΗ ΣΤΗ ΒΙΟΛΟΓΙΚΗ ΓΕΩΡΓΙΑ", materialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΥΠΟ ΜΕΤΑΤΡΟΠΗ ΣΤΗ ΒΙΟΛΟΓΙΚΗ ΓΕΩΡΓΙΑ", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΥΠΟ ΜΕΤΑΤΡΟΠΗ ΣΤΗ ΒΙΟΛΟΓΙΚΗ ΓΕΩΡΓΙΑ", country = "ΕΛΛΑΔΑ", region = "ΑΡΒΗ-ΚΡΗΤΗ", recipe = null, type = PackagedMaterialItem.materialType.SEMIBIO, isEnabled = true, tarePack = 0, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "003", displayCodeDescr = "003 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΧΑΡΤΟΚΙΒΩΤΙΟ", materialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΧΑΡΤΟΚΙΒΩΤΙΟ", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΧΑΡΤΟΚΙΒΩΤΙΟ", country = "ΕΛΛΑΔΑ", region = "ΑΡΒΗ-ΚΡΗΤΗ", recipe = PackagedMaterialItem.recipeType.RECIPELUX, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = true, tarePack = 8, tarePrecentage = 0.07 });
            items.Add(new PackagedMaterialItem() { code = "004", displayCodeDescr = "004 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", materialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", country = "ΕΛΛΑΔΑ", region = "ΑΡΒΗ-ΚΡΗΤΗ", recipe = PackagedMaterialItem.recipeType.RECIPESIMPLE, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = true, tarePack = 6.5, tarePrecentage = 0.07 });
            items.Add(new PackagedMaterialItem() { code = "005", displayCodeDescr = "005 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ-(7,5kg ΑΠΟΒΑΡΟ)", materialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ-(7,5kg ΑΠΟΒΑΡΟ)", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", country = "ΕΛΛΑΔΑ", region = "ΑΡΒΗ-ΚΡΗΤΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = true, tarePack = 7.5, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "006", displayCodeDescr = "006 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΞΥΛΙΝΟ ΣΤΑΝΤ", materialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΞΥΛΙΝΟ ΣΤΑΝΤ", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΞΥΛΙΝΟ ΣΤΑΝΤ", country = "ΕΛΛΑΔΑ", region = "ΑΡΒΗ-ΚΡΗΤΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = true, tarePack = 8, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "007", displayCodeDescr = "007 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΑΛΟΥΜΙΝΕΝΙΟ ΣΤΑΝΤ", materialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΑΛΟΥΜΙΝΕΝΙΟ ΣΤΑΝΤ", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΑΛΟΥΜΙΝΕΝΙΟ ΣΤΑΝΤ", country = "ΕΛΛΑΔΑ", region = "ΑΡΒΗ-ΚΡΗΤΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = false, tarePack = 3, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "008", displayCodeDescr = "008 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΤΡΟΧΗΛΑΤΟ ΚΑΡΟΤΣΙ", materialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΤΡΟΧΗΛΑΤΟ ΚΑΡΟΤΣΙ", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΤΡΟΧΗΛΑΤΟ ΚΑΡΟΤΣΙ", country = "ΕΛΛΑΔΑ", region = "ΑΡΒΗ-ΚΡΗΤΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = false, tarePack = 120, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "009", displayCodeDescr = "009 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΧΥΜΑ ΣΕ ΚΟΥΤΑ", materialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΧΥΜΑ ΣΕ ΚΟΥΤΑ", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΧΥΜΑ ΣΕ ΚΟΥΤΑ", country = "ΕΛΛΑΔΑ", region = "ΑΡΒΗ-ΚΡΗΤΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = false, tarePack = 0, tarePrecentage = 0 });

            items.Add(new PackagedMaterialItem() { code = "010", displayCodeDescr = "010 - DOLE", materialDescr = "DOLE", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ DOLE", country = "ΕΚΟΥΑΔΟΡ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = true, tarePack = 1.5, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "011", displayCodeDescr = "011 - BIO DOLE", materialDescr = "BIO DOLE", materialReadableDescr = "ΒΙΟΛΟΓΙΚΕΣ ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ BIO DOLE", country = "ΕΚΟΥΑΔΟΡ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.BIO, isEnabled = false, tarePack = 1.5, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "012", displayCodeDescr = "012 - FLESTA", materialDescr = "FLESTA", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ FLESTA", country = "ΕΚΟΥΑΔΟΡ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = true, tarePack = 1.5, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "013", displayCodeDescr = "013 - CABANA", materialDescr = "CABANA", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ CABANA", country = "ΕΚΟΥΑΔΟΡ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = true, tarePack = 1.5, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "014", displayCodeDescr = "014 - BAJELA", materialDescr = "BAJELA", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ BAJELA", country = "ΕΚΟΥΑΔΟΡ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = false, tarePack = 1.5, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "015", displayCodeDescr = "015 - ORSERO", materialDescr = "ORSERO", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ ORSERO", country = "ΕΚΟΥΑΔΟΡ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = true, tarePack = 1.5, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "016", displayCodeDescr = "016 - SIMBA", materialDescr = "SIMBA", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ SIMBA", country = "ΚΟΛΟΜΒΙΑ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = false, tarePack = 1.5, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "017", displayCodeDescr = "017 - TROPY", materialDescr = "TROPY", materialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ TROPY", country = "ΚΟΛΟΜΒΙΑ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = false, tarePack = 1.5, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "018", displayCodeDescr = "018 - ΑΝΑΝΑΣ DOLE", materialDescr = "ΑΝΑΝΑΣ DOLE", materialReadableDescr = "ΑΝΑΝΑΣ ΕΙΣΑΓΩΓΗΣ DOLE", country = "ΚΟΣΤΑ ΡΙΚΑ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = true, tarePack = 0.9, tarePrecentage = 0 });
            items.Add(new PackagedMaterialItem() { code = "019", displayCodeDescr = "019 - ΑΝΑΝΑΣ SIMBA", materialDescr = "ΑΝΑΝΑΣ SIMBA", materialReadableDescr = "ΑΝΑΝΑΣ ΕΙΣΑΓΩΓΗΣ SIMBA", country = "ΚΟΣΤΑ ΡΙΚΑ", region = "ΚΕΝ. ΑΜΕΡΙΚΗ", recipe = null, type = PackagedMaterialItem.materialType.CONVENTIONAL, isEnabled = false, tarePack = 0.9, tarePrecentage = 0 });
            return items;
        }

        private static List<SupplierItem> getSupplierItems()
        {
            var items = new List<SupplierItem>();
            items.Add(new SupplierItem() { code = "000", supplierDescr = "Καμία επιλογή" });
            items.Add(new SupplierItem() { code = "001", supplierDescr = "ΠΕΤΡΑΚΗΣ ΧΑΡΑΛΑΜΠΟΣ", grSupplier = "GR010323B00000902", supplierRegion = "ΑΡΒΗ-ΚΡΗΤΗΣ" });
            items.Add(new SupplierItem() { code = "002", supplierDescr = "ΜΑΥΡΑΚΗ ΑΣΗΜΙΝΑ", grSupplier = "GR055490B00000902", supplierRegion = "ΤΕΡΤΣΑ-ΚΡΗΤΗΣ" });
            items.Add(new SupplierItem() { code = "003", supplierDescr = "ΜΑΘΙΟΥΔΑΚΗΣ ΣΤΕΦΑΝΙΑ", grSupplier = "GR050841B00000902", supplierRegion = "ΑΡΒΗ-ΚΡΗΤΗΣ" });
            items.Add(new SupplierItem() { code = "004", supplierDescr = "ΜΑΥΡΙΔΗΣ ΕΛΕΥΘΕΡΙΟΣ", grSupplier = "GR050680B00000902", supplierRegion = "ΤΕΡΤΣΑ-ΚΡΗΤΗΣ" });
            items.Add(new SupplierItem() { code = "005", supplierDescr = "DOLE HELLAS LTD", grSupplier = "GR-----", supplierRegion = "ΚΕΝ. ΑΜΕΡΙΚΗ" });
            items.Add(new SupplierItem() { code = "005", supplierDescr = "BELLA FRUTTA", grSupplier = "GR-----", supplierRegion = "ΚΕΝ. ΑΜΕΡΙΚΗ" });
            return items;
        }

        private static List<CustomerItem> getCustomerItems()
        {
            var items = new List<CustomerItem>();
            items.Add(new CustomerItem() { code = "000", customerDescr = "Καμία επιλογή" });
            items.Add(new CustomerItem() { code = "001", customerDescr = "ΠΑΥΛΑΚΗΣ Α.Ε" });
            items.Add(new CustomerItem() { code = "002", customerDescr = "ΜΠΟΛΙΟΥΔΑΚΗΣ" });
            items.Add(new CustomerItem() { code = "003", customerDescr = "ΤΖΑΓΚΑΡΑΚΗΣ" });
            items.Add(new CustomerItem() { code = "004", customerDescr = "ΤΣΙΧΛΑΚΗΣ" });
            items.Add(new CustomerItem() { code = "005", customerDescr = "STARFRESH Λ13-Λ15-Λ17" });
            items.Add(new CustomerItem() { code = "006", customerDescr = "ΤΣΟΛΑΚΗΣ Δ5" });
            items.Add(new CustomerItem() { code = "007", customerDescr = "ΑΒ ΒΑΣΙΛΟΠΟΥΛΟΣ" });
            items.Add(new CustomerItem() { code = "008", customerDescr = "BELLA FRUTTA" });
            return items;
        }
    }
}
