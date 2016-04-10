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
    /// Summary description for PostStockDetail
    /// </summary>
    public class PostStockDetail : IHttpHandler
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
                var stkItm = jsonString;
                string[] itmDet = stkItm.Split('%');
                //DateTime dt = new DateTime();
                //dt = DateTime.Parse(itmDet[4]);
                string result = string.Empty;
                try
                {
                    if (itmDet[9] == "btnStkAdd")
                    {
                        MySqlConnection conn = new MySqlConnection(connString);
                        conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand("INSERT INTO tbl" + itmDet[0] + " VALUES (@srno,@parti,@type,@brand,@date,@mdln,@remarks,@status,@rentcode)", conn);
                        cmd1.Parameters.AddWithValue("@srno", itmDet[1]);
                        cmd1.Parameters.AddWithValue("@parti", itmDet[0]);
                        cmd1.Parameters.AddWithValue("@type", itmDet[2]);
                        cmd1.Parameters.AddWithValue("@brand", itmDet[3]);
                        cmd1.Parameters.AddWithValue("@date", itmDet[4]);
                        cmd1.Parameters.AddWithValue("@mdln", itmDet[5]);
                        cmd1.Parameters.AddWithValue("@remarks", itmDet[6]);
                        cmd1.Parameters.AddWithValue("@status", itmDet[7]);
                        cmd1.Parameters.AddWithValue("@rentcode", itmDet[8]);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        result = itmDet[1] + " Added Successfully";
                    }
                    else
                    {
                        MySqlConnection conn = new MySqlConnection(connString);
                        conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand("Update tbl" + itmDet[0] + " SET Type=@type,Brand=@brand,PurchaseDate=@date,ModelNumber=@mdln,Remarks=@remarks,Status=@status,RentCode=@rentcode Where SerialNumber=@srno", conn);
                        cmd1.Parameters.AddWithValue("@srno", itmDet[1]);
                        cmd1.Parameters.AddWithValue("@parti", itmDet[0]);
                        cmd1.Parameters.AddWithValue("@type", itmDet[2]);
                        cmd1.Parameters.AddWithValue("@brand", itmDet[3]);
                        cmd1.Parameters.AddWithValue("@date", itmDet[4]);
                        cmd1.Parameters.AddWithValue("@mdln", itmDet[5]);
                        cmd1.Parameters.AddWithValue("@remarks", itmDet[6]);
                        cmd1.Parameters.AddWithValue("@status", itmDet[7]);
                        cmd1.Parameters.AddWithValue("@rentcode", itmDet[8]);
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                        result = itmDet[1] + " Updated Successfully";
                    }
                }
                catch (MySqlException ex)
                {
                    result = ex.Message.ToString();
                }

                context.Response.Write(jSerialize.Serialize(
                        new
                        {
                            Response = result
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