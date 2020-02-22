using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using static IoTSuperScale.IoTDB.DBinit;

namespace IoTSuperScale.Configuration
{
    static public class Startup
    {
        public static SqlConnection sin;
        static public async Task ConfigureAsync()
        {
            //just prepare the singleton object avoiding latency
            try
            {
                sin = SingletonERP.GetERPDbInstance().GetERPDBConnection();
                SingletonERP.GetERPDbInstance().CloseERPDBConnection();
                await EnsureLabelsExistance();
                //sin2 = SingletonMRP.getMRPDbInstance().GetMRPDBConnection();
                //SingletonMRP.getMRPDbInstance().CloseMRPDBConnection();
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleERPerrorDBConnection"));
            }
        }

        static private async Task EnsureLabelsExistance()
        {
            try
            {
                StorageFolder targetFolder = ApplicationData.Current.LocalFolder;
                IReadOnlyList<IStorageItem> fileList = await targetFolder.GetFilesAsync();

                StorageFolder appFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\IoTLabels");
                IReadOnlyList<IStorageItem> labels = await appFolder.GetFilesAsync();

                if (fileList.Count != labels.Count)
                {
                    foreach (StorageFile item in labels)
                    {
                        await item.CopyAsync(targetFolder,item.Name,NameCollisionOption.ReplaceExisting);
                    }
                }
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Resources").GetString("titleERPerrorDBConnection"));
            }
        }
    }
}
