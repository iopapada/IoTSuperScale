using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace IoTSuperScale.IoTDB
{
    public class LotItem
    {
        public string Code { get; set; }
        public double Qty1 { get; set; }
        public string MUQ1 { get; set; }
        public double Qty2 { get; set; }
        public string MUQ2 { get; set; }
        public string Description{ get; set;}
        public string GetInfo
        {
            get
            {
                if(Qty2!=0.0)
                    return string.Format("{0} - {1} {2}", Code, Qty2, ResourceLoader.GetForCurrentView().GetString("Boxes"));
                else if(Qty1!=0.0)
                    return string.Format("{0} - {1} {2}", Code, Qty1, ResourceLoader.GetForCurrentView().GetString("Kilos"));
                else
                    return string.Format("{0}", Code);
            }
            //set => string.Format("{0} - {1} {2}", Code, Qty2, MUQ2);
        }
    }
}
