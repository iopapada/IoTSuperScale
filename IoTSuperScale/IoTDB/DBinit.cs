using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Windows.ApplicationModel.Resources;

namespace IoTSuperScale.IoTDB
{
    public class DBinit
    {
        public sealed class SingletonMRP
        {
            private static SingletonMRP dbMRPInstance = null;
            private SqlConnection mrpDBconn = new SqlConnection(AppSettings.MRPDBConnectionString);
            public static SingletonMRP GetMRPDbInstance()
            {
                if (dbMRPInstance == null)
                {
                    dbMRPInstance = new SingletonMRP();
                }
                return dbMRPInstance;
            }
            public SqlConnection GetMRPDBConnection()
            {
                SqlConnection mrpDBconn = new SqlConnection(AppSettings.MRPDBConnectionString);
                try
                {
                    mrpDBconn.Open();
                }
                catch (SqlException ex)
                {
                    App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleMRPerrorDBConnection"));
                }
                return mrpDBconn;
            }
            public void CloseMRPDBConnection()
            {
                if (SingletonMRP.GetMRPDbInstance().GetMRPDBConnection().State == ConnectionState.Open) mrpDBconn.Close();
            }
        }
        public sealed class SingletonERP
        {
            private static SingletonERP dbERPInstance = null;
            private readonly SqlConnection erpDBconn = new SqlConnection(AppSettings.ERPDBConnectionString);

            private SingletonERP()
            {
            }
            public static SingletonERP GetERPDbInstance()
            {
                if (dbERPInstance == null)
                {
                    dbERPInstance = new SingletonERP();
                }
                return dbERPInstance;
            }
            public SqlConnection GetERPDBConnection()
            {
                SqlConnection erpDBconn = new SqlConnection(AppSettings.ERPDBConnectionString);
                try
                {
                    erpDBconn.Open();
                }
                catch (SqlException ex)
                {
                    App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("titleERPerrorDBConnection"));
                }
                return erpDBconn;
            }
            public void CloseERPDBConnection()
            {
                if (SingletonERP.GetERPDbInstance().GetERPDBConnection().State == ConnectionState.Open) erpDBconn.Close();
            }
        }
        public static ObservableCollection<PackagedMaterialItem> GetAllSuppliers()
        {
            return null;
        }
        public static ObservableCollection<PackagedMaterialItem> GetAllCustomers()
        {
            return null;
        }
        public static ObservableCollection<PackagedMaterialItem> GetAllMaterials()
        {
            return null;
        }
        public static ObservableCollection<LotItem> GetLotsOfProduct(string itemCode)
        {
            //string testt = @"select * from HEITEMS";
            string GetLotsQuery = @"select lot.HECREATIONDATE, lot.HECODE, lot.HEBLOCKSALES, data.HECODE, data.HENAME," +
                                   "attr.HEABALANCE, attr.HEBBALANCE, attr.HEABILLEDPURQTY, attr.HEBBILLEDPURQTY, " +
                                   "attr.HEABILLEDSALQTY, attr.HEBBILLEDSALQTY, attr.HEASALQTY, attr.HEBSALQTY, " +
                                   "attr.HEAIMPQTY, attr.HEBIMPQTY " +
                                   "from HEITEMS as data " +
                                   "join HELOTNUMBERS as lot on data.HEID = lot.HEITEMID " +
                                   "join HELOTIATTRFINDATA as attr on lot.HEID = attr.HELOTNID " +
                                   "where data.HECODE = @itemCode and lot.HEBLOCKSALES = 0 and attr.HEABALANCE > 0 " +
                                   "Order By lot.HECREATIONDATE Desc ";

            var lots = new ObservableCollection<LotItem>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(GetLotsQuery, SingletonERP.GetERPDbInstance().GetERPDBConnection()))
                {
                    //cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.AddWithValue("@itemCode",itemCode.ToString());
                    cmd.Parameters.Add("@itemCode", SqlDbType.NVarChar);
                    cmd.Parameters["@itemCode"].Value = itemCode;
                    //SqlDataAdapter test = new SqlDataAdapter(cmd);
                    //test.Fill(ds,"orders");
                    using (SqlDataReader myReader = cmd.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            var material = new LotItem();
                            material.Code = myReader.GetString(1);
                            material.Qty1 = Double.Parse(myReader.GetDecimal(5).ToString());
                            material.Qty2 = Double.Parse(myReader.GetDecimal(6).ToString());

                            lots.Add(material);
                        }
                        //DataTable myTable = new DataTable();
                        //myTable.Load(myReader);
                        SingletonERP.GetERPDbInstance().CloseERPDBConnection();
                    }
                }
                return lots;
            }
            catch (Exception ex)
            {
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgLotNumsQuery"));
                return lots;
            }
        }
    }
}
