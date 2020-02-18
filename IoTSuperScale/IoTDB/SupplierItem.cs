namespace IoTSuperScale.IoTDB
{
    public class SupplierItem
    {
        public string Code { get; set; }
        public string SupplierDescr { get; set; }
        public string GrSupplier { get; set; }
        public string SupplierRegion { get; set; }

        public SupplierItem(string code, string descr, string gr, string reg)
        {
            Code = code;
            SupplierDescr = descr;
            GrSupplier = gr;
            SupplierRegion = reg;
        }

        public SupplierItem(string code, string descr)
        {
            Code = code;
            SupplierDescr = descr;
        }
    }
}
