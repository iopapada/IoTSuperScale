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

        public static ObservableCollection<LotItem> GetLotsOfProduct(string connectionString, string itemCode)
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand(GetLotsQuery, conn))
                    {
                        //cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.AddWithValue("@itemCode",itemCode.ToString());
                        cmd.Parameters.Add("@itemCode", SqlDbType.NVarChar);
                        cmd.Parameters["@itemCode"].Value = itemCode;
                        //SqlDataAdapter test = new SqlDataAdapter(cmd);
                        //test.Fill(ds,"orders");

                        conn.Open();
                        if (conn.State == ConnectionState.Open)
                        {
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
                                conn.Close();
                            }
                        }
                    }
                }
                return lots;
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("Exception: " + ex.Message);
                App.PrintOkMessage(ex.Message, ResourceLoader.GetForViewIndependentUse("Messages").GetString("msgReceiptEncoding"));
                return lots;
            }
        }
    }
}
