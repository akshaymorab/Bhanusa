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
    /// Summary description for GetPODetails
    /// </summary>
    public class GetPODetails : IHttpHandler
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
                var chkItem = jsonString;
                string poDet = string.Empty;
                string result = string.Empty;

                DataTable dtPOComp = new DataTable();
                if (chkItem == "hi")
                {
                    MySqlConnection conn = new MySqlConnection(connString);
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM tblPO", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtPOComp);
                    List<string> lstPOdet = new List<string>();

                    for (int i = 0; i <= dtPOComp.Rows.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            poDet = dtPOComp.Rows[i]["PoNo"].ToString() + '%' + dtPOComp.Rows[i]["PoComp"].ToString() + '%' + dtPOComp.Rows[i]["StartDate"].ToString() + '%' + dtPOComp.Rows[i]["EndDate"].ToString() + '%' + dtPOComp.Rows[i]["RenewDate"].ToString();
                        }
                        else
                        {
                            poDet = poDet + ";" + dtPOComp.Rows[i]["PoNo"].ToString() + '%' + dtPOComp.Rows[i]["PoComp"].ToString() + '%' + dtPOComp.Rows[i]["StartDate"].ToString() + '%' + dtPOComp.Rows[i]["EndDate"].ToString() + '%' + dtPOComp.Rows[i]["RenewDate"].ToString();
                        }
                    }

                    conn.Close();
                    result = poDet;
                    context.Response.Write(jSerialize.Serialize(result));
                }
                else
                {
                    MySqlConnection conn = new MySqlConnection(connString);
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT PoNo FROM tblPO", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtPOComp);
                    List<string> lstPOdet = new List<string>();

                    for (int i = 0; i <= dtPOComp.Rows.Count - 1; i++)
                    {
                        poDet = dtPOComp.Rows[i]["PoNo"].ToString();
                        lstPOdet.Add(poDet);
                    }
                    
                    conn.Close();
                    context.Response.Write(jSerialize.Serialize(lstPOdet));
                }
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