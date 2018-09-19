using IoTSuperScale.IoTDB;
using System;
using Windows.UI.Xaml.Data;

namespace IoTSuperScale.IoTControls
{

    public class ConvertersUI : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is PackagedMaterialItem)
                return value as PackagedMaterialItem;
            else if (value is SupplierItem)
                return value as SupplierItem;
            else if (value is CustomerItem)
                return value as CustomerItem;
            else if (value is LotItem)
                return value as LotItem;
            else
                return null;
        }
    }
}
