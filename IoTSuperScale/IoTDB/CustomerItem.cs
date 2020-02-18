namespace IoTSuperScale.IoTDB
{
    public class CustomerItem
    {
        public string Code { get; set; }
        public string CustomerDescr { get; set; }
        public CustomerItem(string code, string descr)
        {
            Code = code;
            CustomerDescr = descr;
        }

        public CustomerItem(string v1, string v2, string v3, string v4)
        {
        }

    }
}
