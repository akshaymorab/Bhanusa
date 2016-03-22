using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Bhanusa
{
    /// <summary>
    /// Summary description for GetStockDetails
    /// </summary>
    public class GetStockDetails : IHttpHandler
    {

        static string connString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            string jsonString = String.Empty;
            HttpContext.Current.Request.InputStream.Position = 0;
            using (System.IO.StreamReader inputStream =
            new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
                System.Web.Script.Serialization.JavaScriptSerializer jSerialize =
                    new System.Web.Script.Serialization.JavaScriptSerializer();
                var stkDet = jsonString;
                string res = string.Empty;
                string stkRowRes = string.Empty;

                DataTable dtStkDetails = new DataTable();
                if (stkDet != "")
                {
                    MySqlConnection conn = new MySqlConnection(connString);
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM tbl" + stkDet, conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtStkDetails);
                    conn.Close();

                    if (dtStkDetails.Rows.Count != 0)
                    {
                        if (stkDet != "Company")
                        {
                            for (int i = 0; i <= dtStkDetails.Rows.Count - 1; i++)
                            {
                                string sts = dtStkDetails.Rows[i]["Status"].ToString();
                                if (sts == "0")
                                {
                                    sts = "In Stock";
                                }
                                else
                                {
                                    sts = "Rent";
                                }
                                if (i == 0)
                                {

                                    stkRowRes = dtStkDetails.Rows[i]["SerialNumber"].ToString() + "^" + dtStkDetails.Rows[i]["Particular"].ToString() + "^" + dtStkDetails.Rows[i]["Type"].ToString() + "^" + dtStkDetails.Rows[i]["Brand"].ToString() + "^" + dtStkDetails.Rows[i]["PurchaseDate"].ToString() + "^" + dtStkDetails.Rows[i]["ModelNumber"].ToString() + "^" + dtStkDetails.Rows[i]["Remarks"].ToString() + "^" + sts + "^" + dtStkDetails.Rows[i]["RentCode"].ToString();
                                }
                                else
                                {
                                    stkRowRes = stkRowRes + ";" + dtStkDetails.Rows[i]["SerialNumber"].ToString() + "^" + dtStkDetails.Rows[i]["Particular"].ToString() + "^" + dtStkDetails.Rows[i]["Type"].ToString() + "^" + dtStkDetails.Rows[i]["Brand"].ToString() + "^" + dtStkDetails.Rows[i]["PurchaseDate"].ToString() + "^" + dtStkDetails.Rows[i]["ModelNumber"].ToString() + "^" + dtStkDetails.Rows[i]["Remarks"].ToString() + "^" + sts + "^" + dtStkDetails.Rows[i]["RentCode"].ToString();
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i <= dtStkDetails.Rows.Count - 1; i++)
                            {
                                if (i == 0)
                                {

                                    stkRowRes = dtStkDetails.Rows[i]["CompanyId"].ToString() + "%" + dtStkDetails.Rows[i]["Name"].ToString() + "%" + dtStkDetails.Rows[i]["Address"].ToString() + "%" + dtStkDetails.Rows[i]["Phone"].ToString() + "%" + dtStkDetails.Rows[i]["Location"].ToString();
                                }
                                else
                                {
                                    stkRowRes = stkRowRes + ";" + dtStkDetails.Rows[i]["CompanyId"].ToString() + "%" + dtStkDetails.Rows[i]["Name"].ToString() + "%" + dtStkDetails.Rows[i]["Address"].ToString() + "%" + dtStkDetails.Rows[i]["Phone"].ToString() + "%" + dtStkDetails.Rows[i]["Location"].ToString();
                                }
                            }
                        }

                    }
                    res = stkRowRes;
                }
                else
                {
                    res = "Could not load!";
                }

                

                context.Response.Write(jSerialize.Serialize(
                        new
                        {
                            Response = res
                        }));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}