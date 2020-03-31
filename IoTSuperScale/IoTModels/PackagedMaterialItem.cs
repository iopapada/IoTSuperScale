namespace IoTSuperScale.Models
{
    public class PackagedMaterialItem
    {
        public enum MaterialType
        {
            BIO,
            SEMIBIO,
            CONVENTIONAL
        }
        public enum RecipeType
        {
            RECIPELUX,
            RECIPESIMPLE,
        }
        public string Code { get; set; }
        public string DisplayCodeDescr { get; set; }
        public string MaterialDescr { get; set; }
        public string MaterialReadableDescr { get; set; }
        public RecipeType? Recipe { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public MaterialType Type { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsWeighed { get; set; }
        public double TarePack { get; set; }
        public double TarePrecentage { get; set; }
        public bool IsEEcountry { get; set; }
        public string Category { get; set; }
        public string Variety { get; set; }
        public string GRmaterial { get; set; }
    }
}
