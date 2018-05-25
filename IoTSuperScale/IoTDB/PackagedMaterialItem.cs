namespace IoTSuperScale.IoTDB
{
    public class PackagedMaterialItem
    {
        public enum materialType
        {
            BIO,
            SEMIBIO,
            CONVENTIONAL
        }
        public enum recipeType
        {
            RECIPELUX,
            RECIPESIMPLE,
        }
        public string code { get; set; }
        public string displayCodeDescr { get; set; }
        public string materialDescr { get; set; }
        public string materialReadableDescr { get; set; }
        public recipeType? recipe { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public materialType type { get; set; }
        public bool isEnabled { get; set; }
        public bool isWeighed { get; set; }
        public double tarePack { get; set; }
        public double tarePrecentage { get; set; }
        public bool isEEcountry { get; set; }
        public string category { get; set; }
        public string variety { get; set; }
    }
}
