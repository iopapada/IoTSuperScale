using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IoTSuperScale.IoTDB
{
    public class ComboBoxOptionsManager
    {
        public static void GetAllPackMaterialList(ObservableCollection<PackagedMaterialItem> ComboBoxItems)
        {
            var allItems = GetPackagedMaterialItems();
            ComboBoxItems.Clear();
            allItems.ForEach(p => ComboBoxItems.Add(p));
        }
        public static void GetAllSuppliersList(ObservableCollection<SupplierItem> ComboBoxItems)
        {
            var allItems = GetSupplierItems();
            ComboBoxItems.Clear();
            allItems.ForEach(p => ComboBoxItems.Add(p));
        }
        public static void GetAllCustomersList(ObservableCollection<CustomerItem> ComboBoxItems)
        {
            var allItems = GetCustomerItems();
            ComboBoxItems.Clear();
            allItems.ForEach(p => ComboBoxItems.Add(p));
        }
        public static void GetEnabledPackMaterialsList(ObservableCollection<PackagedMaterialItem> ComboBoxItems)
        {
            var allItems = GetPackagedMaterialItems().Where(p => p.IsEnabled == true);
            ComboBoxItems.Clear();
            allItems.ToList().ForEach(p => ComboBoxItems.Add(p));
        }

        private static List<PackagedMaterialItem> GetPackagedMaterialItems()
        {
            var items = new List<PackagedMaterialItem>();
            //local products
            items.Add(new PackagedMaterialItem() { Code = "000", DisplayCodeDescr = "Καμία επιλογή", MaterialDescr = "Καμία επιλογή", MaterialReadableDescr = "Καμία επιλογή", IsEnabled=true });
            items.Add(new PackagedMaterialItem() { Code = "004", DisplayCodeDescr = "004 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΠΡΟΙΟΝ ΒΙΟΛΟΓΙΚΗΣ ΓΕΩΡΓΙΑΣ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΠΡΟΙΟΝ ΒΙΟΛΟΓΙΚΗΣ ΓΕΩΡΓΙΑΣ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΠΡΟΙΟΝ ΒΙΟΛΟΓΙΚΗΣ ΓΕΩΡΓΙΑΣ", Country = "ΕΛΛΑΔΑ", Region = "ΑΡΒΗ-ΚΡΗΤΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.BIO, IsEnabled = true, TarePack = 0, TarePrecentage = 0, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "004-1", DisplayCodeDescr = "004-1 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΥΠΟ ΜΕΤΑΤΡΟΠΗ ΣΤΗ ΒΙΟΛΟΓΙΚΗ ΓΕΩΡΓΙΑ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΥΠΟ ΜΕΤΑΤΡΟΠΗ ΣΤΗ ΒΙΟΛΟΓΙΚΗ ΓΕΩΡΓΙΑ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΥΠΟ ΜΕΤΑΤΡΟΠΗ ΣΤΗ ΒΙΟΛΟΓΙΚΗ ΓΕΩΡΓΙΑ", Country = "ΕΛΛΑΔΑ", Region = "ΑΡΒΗ-ΚΡΗΤΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.SEMIBIO, IsEnabled = true, TarePack = 0, TarePrecentage = 0, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "005-1", DisplayCodeDescr = "005-1 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΧΑΡΤΟΚΙΒΩΤΙΟ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΧΑΡΤΟΚΙΒΩΤΙΟ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΧΑΡΤΟΚΙΒΩΤΙΟ", Country = "ΕΛΛΑΔΑ", Region = "ΑΡΒΗ-ΚΡΗΤΗ", Recipe = PackagedMaterialItem.RecipeType.RECIPELUX, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = true, TarePack = 8, TarePrecentage = 0.07, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "005", DisplayCodeDescr = "005 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", Country = "ΕΛΛΑΔΑ", Region = "ΑΡΒΗ-ΚΡΗΤΗ", Recipe = PackagedMaterialItem.RecipeType.RECIPESIMPLE, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = true, TarePack = 6.5, TarePrecentage = 0.07, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "005", DisplayCodeDescr = "005 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ-(7,5kg ΑΠΟΒΑΡΟ)", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ-(7,5kg ΑΠΟΒΑΡΟ)", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", Country = "ΕΛΛΑΔΑ", Region = "ΑΡΒΗ-ΚΡΗΤΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = true, TarePack = 7.5, TarePrecentage = 0, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "005", DisplayCodeDescr = "005 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΞΥΛΙΝΟ ΣΤΑΝΤ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΞΥΛΙΝΟ ΣΤΑΝΤ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΞΥΛΙΝΟ ΣΤΑΝΤ", Country = "ΕΛΛΑΔΑ", Region = "ΑΡΒΗ-ΚΡΗΤΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = true, TarePack = 8, TarePrecentage = 0, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "011", DisplayCodeDescr = "011 - ΜΠΑΝΑΝΕΣ ΚΥΠΡΟΥ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΥΠΡΟΥ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΥΠΡΟΥ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", Country = "ΚΥΠΡΟΣ", Region = "ΠΑΦΟΣ", Recipe = PackagedMaterialItem.RecipeType.RECIPESIMPLE, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 6.5, TarePrecentage = 0.07, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "011", DisplayCodeDescr = "011 - ΜΠΑΝΑΝΕΣ ΚΥΠΡΟΥ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ-(7,5kg ΑΠΟΒΑΡΟ)", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΥΠΡΟΥ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ-(7,5kg ΑΠΟΒΑΡΟ)", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΥΠΡΟΥ ΣΕ ΜΕΤΑΛΛΙΝΚΟ ΣΤΑΝΤ", Country = "ΚΥΠΡΟΣ", Region = "ΠΑΦΟΣ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 7.5, TarePrecentage = 0, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "011", DisplayCodeDescr = "011 - ΜΠΑΝΑΝΕΣ ΚΥΠΡΟΥ ΣΕ ΞΥΛΙΝΟ ΣΤΑΝΤ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΥΠΡΟΥ ΣΕ ΞΥΛΙΝΟ ΣΤΑΝΤ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΥΠΡΟΥ ΣΕ ΞΥΛΙΝΟ ΣΤΑΝΤ", Country = "ΚΥΠΡΟΣ", Region = "ΠΑΦΟΣ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 8, TarePrecentage = 0, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            //future codes
            items.Add(new PackagedMaterialItem() { Code = "xxx01", DisplayCodeDescr = "xxx01 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΑΛΟΥΜΙΝΕΝΙΟ ΣΤΑΝΤ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΑΛΟΥΜΙΝΕΝΙΟ ΣΤΑΝΤ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΑΛΟΥΜΙΝΕΝΙΟ ΣΤΑΝΤ", Country = "ΕΛΛΑΔΑ", Region = "ΑΡΒΗ-ΚΡΗΤΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 3, TarePrecentage = 0, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "xxx02", DisplayCodeDescr = "xxx02 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΤΡΟΧΗΛΑΤΟ ΚΑΡΟΤΣΙ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΤΡΟΧΗΛΑΤΟ ΚΑΡΟΤΣΙ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΣΕ ΤΡΟΧΗΛΑΤΟ ΚΑΡΟΤΣΙ", Country = "ΕΛΛΑΔΑ", Region = "ΑΡΒΗ-ΚΡΗΤΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 120, TarePrecentage = 0, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "xxx03", DisplayCodeDescr = "xxx03 - ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΧΥΜΑ ΣΕ ΚΟΥΤΑ", MaterialDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΧΥΜΑ ΣΕ ΚΟΥΤΑ", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΚΡΗΤΗΣ ΧΥΜΑ ΣΕ ΚΟΥΤΑ", Country = "ΕΛΛΑΔΑ", Region = "ΑΡΒΗ-ΚΡΗΤΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 0, TarePrecentage = 0, IsEEcountry = true, Category = "II", Variety = "Cavendish" });
            //imported products
            items.Add(new PackagedMaterialItem() { Code = "1", DisplayCodeDescr = "1 - DOLE", MaterialDescr = "DOLE", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ DOLE", Country = "ΕΚΟΥΑΔΟΡ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = true, TarePack = 1.5, TarePrecentage = 0, IsEEcountry = false, Category = "Extra", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "002", DisplayCodeDescr = "002 - DELORO", MaterialDescr = "DELORO", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ DELORO", Country = "ΕΚΟΥΑΔΟΡ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 1.5, TarePrecentage = 0, IsEEcountry = false, Category = "Extra", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "003", DisplayCodeDescr = "003 - BIO DOLE", MaterialDescr = "BIO DOLE", MaterialReadableDescr = "ΒΙΟΛΟΓΙΚΕΣ ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ BIO DOLE", Country = "ΕΚΟΥΑΔΟΡ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.BIO, IsEnabled = true, TarePack = 1.5, TarePrecentage = 0, IsEEcountry = false, Category = "Extra", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "006", DisplayCodeDescr = "006 - CABANA", MaterialDescr = "CABANA", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ CABANA", Country = "ΕΚΟΥΑΔΟΡ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 1.5, TarePrecentage = 0, IsEEcountry = false, Category = "Extra", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "007", DisplayCodeDescr = "007 - BAJELA", MaterialDescr = "BAJELA", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ BAJELA", Country = "ΕΚΟΥΑΔΟΡ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 1.5, TarePrecentage = 0, IsEEcountry = false, Category = "Extra", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "009", DisplayCodeDescr = "009 - SIMBA", MaterialDescr = "SIMBA", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ SIMBA", Country = "ΚΟΛΟΜΒΙΑ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 1.5, TarePrecentage = 0, IsEEcountry = false, Category = "Extra", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "012", DisplayCodeDescr = "012 - FLESTA", MaterialDescr = "FLESTA", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ FLESTA", Country = "ΕΚΟΥΑΔΟΡ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 1.5, TarePrecentage = 0, IsEEcountry = false, Category = "Extra", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "014", DisplayCodeDescr = "014 - ORSERO", MaterialDescr = "ORSERO", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ ORSERO", Country = "ΕΚΟΥΑΔΟΡ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 1.5, TarePrecentage = 0, IsEEcountry = false, Category = "Extra", Variety = "Cavendish" });
            items.Add(new PackagedMaterialItem() { Code = "015", DisplayCodeDescr = "015 - TROPY", MaterialDescr = "TROPY", MaterialReadableDescr = "ΜΠΑΝΑΝΕΣ ΕΙΣΑΓΩΓΗΣ TROPY", Country = "ΚΟΛΟΜΒΙΑ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = false, TarePack = 1.5, TarePrecentage = 0, IsEEcountry = false, Category = "Extra", Variety = "Cavendish" });
            //other products
            items.Add(new PackagedMaterialItem() { Code = "018", DisplayCodeDescr = "018 - ΑΝΑΝΑΣ DOLE", MaterialDescr = "ΑΝΑΝΑΣ DOLE", MaterialReadableDescr = "ΑΝΑΝΑΣ ΕΙΣΑΓΩΓΗΣ DOLE", Country = "ΚΟΣΤΑ ΡΙΚΑ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = true, TarePack = 0.9, TarePrecentage = 0, IsEEcountry = false, Category = null, Variety = null });
            items.Add(new PackagedMaterialItem() { Code = "019", DisplayCodeDescr = "019 - ΑΝΑΝΑΣ SIMBA", MaterialDescr = "ΑΝΑΝΑΣ SIMBA", MaterialReadableDescr = "ΑΝΑΝΑΣ ΕΙΣΑΓΩΓΗΣ SIMBA", Country = "ΚΟΣΤΑ ΡΙΚΑ", Region = "ΚΕΝ. ΑΜΕΡΙΚΗ", Recipe = null, Type = PackagedMaterialItem.MaterialType.CONVENTIONAL, IsEnabled = true, TarePack = 0.9, TarePrecentage = 0, IsEEcountry = false, Category = null, Variety = null });
            return items;
        }

        private static List<SupplierItem> GetSupplierItems()
        {
            var items = new List<SupplierItem>();
            items.Add(new SupplierItem() { Code = "000", SupplierDescr = "Καμία επιλογή" });
            items.Add(new SupplierItem() { Code = "001", SupplierDescr = "ΠΕΤΡΑΚΗΣ ΧΑΡΑΛΑΜΠΟΣ", GrSupplier = "GR010323B00000902", SupplierRegion = "ΑΡΒΗ-ΚΡΗΤΗΣ" });
            items.Add(new SupplierItem() { Code = "002", SupplierDescr = "ΜΑΥΡΑΚΗ ΑΣΗΜΙΝΑ", GrSupplier = "GR055490B00000902", SupplierRegion = "ΤΕΡΤΣΑ-ΚΡΗΤΗΣ" });
            items.Add(new SupplierItem() { Code = "003", SupplierDescr = "ΜΑΘΙΟΥΔΑΚΗΣ ΣΤΕΦΑΝΙΑ", GrSupplier = "GR050841B00000902", SupplierRegion = "ΑΡΒΗ-ΚΡΗΤΗΣ" });
            items.Add(new SupplierItem() { Code = "004", SupplierDescr = "ΜΑΥΡΙΔΗΣ ΕΛΕΥΘΕΡΙΟΣ", GrSupplier = "GR050680B00000902", SupplierRegion = "ΤΕΡΤΣΑ-ΚΡΗΤΗΣ" });
            items.Add(new SupplierItem() { Code = "005", SupplierDescr = "DOLE HELLAS LTD", GrSupplier = "GR0100401B", SupplierRegion = "ΚΕΝ. ΑΜΕΡΙΚΗ" });
            items.Add(new SupplierItem() { Code = "005", SupplierDescr = "BELLA FRUTTA", GrSupplier = "GR-----", SupplierRegion = "ΚΕΝ. ΑΜΕΡΙΚΗ" });
            return items;
        }

        private static List<CustomerItem> GetCustomerItems()
        {
            var items = new List<CustomerItem>();
            items.Add(new CustomerItem() { Code = "000", CustomerDescr = "Καμία επιλογή" });
            items.Add(new CustomerItem() { Code = "001", CustomerDescr = "ΠΑΥΛΑΚΗΣ Α.Ε" });
            items.Add(new CustomerItem() { Code = "002", CustomerDescr = "ΜΠΟΛΙΟΥΔΑΚΗΣ" });
            items.Add(new CustomerItem() { Code = "003", CustomerDescr = "ΤΖΑΓΚΑΡΑΚΗΣ" });
            items.Add(new CustomerItem() { Code = "004", CustomerDescr = "ΤΣΙΧΛΑΚΗΣ" });
            items.Add(new CustomerItem() { Code = "005", CustomerDescr = "STARFRESH Λ13/Λ15/Λ17" });
            items.Add(new CustomerItem() { Code = "006", CustomerDescr = "ΤΣΟΛΑΚΗΣ Δ5" });
            items.Add(new CustomerItem() { Code = "007", CustomerDescr = "ΜΠΑΦΕΤΗΣ Κ37/39/41/43" });
            items.Add(new CustomerItem() { Code = "008", CustomerDescr = "PLANET FRUITS" });
            items.Add(new CustomerItem() { Code = "009", CustomerDescr = "ΑΒ ΒΑΣΙΛΟΠΟΥΛΟΣ" });
            items.Add(new CustomerItem() { Code = "010", CustomerDescr = "BELLA FRUTTA" });
            items.Add(new CustomerItem() { Code = "011", CustomerDescr = "LIBERO" });
            items.Add(new CustomerItem() { Code = "012", CustomerDescr = "ΧΑΛΚΙΑΔΑΚΗΣ Α.Ε" });
            return items;
        }
    }
}
