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
    /// Summary description for GetItemDetails
    /// </summary>
    public class GetItemDetails : IHttpHandler
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

                DataTable dtDCItem = new DataTable();
                MySqlConnection conn = new MySqlConnection(connString);
                string[] chkDCItem = chkItem.Split(',');
                conn.Open();
                if (chkItem == "Desktop")
                {
                    MySqlCommand cmd1 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tblDesktop where Status = 0", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtDCItem);
                }
                if (chkItem == "Laptop")
                {
                    MySqlCommand cmd1 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tblLaptop where Status = 0", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtDCItem);
                }
                if (chkItem == "Server")
                {
                    MySqlCommand cmd1 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tblServer where Status = 0", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtDCItem);
                }
                if (chkItem == "Printer")
                {
                    MySqlCommand cmd1 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tblPrinter where Status = 0", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtDCItem);
                }
                if (chkItem == "Projector")
                {
                    MySqlCommand cmd1 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tblProjector where Status = 0", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtDCItem);
                }
                if (chkItem == "Mobile")
                {
                    MySqlCommand cmd1 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tblMobile where Status = 0", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtDCItem);
                }
                if (chkItem == "Tablet")
                {
                    MySqlCommand cmd1 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tblTablet where Status = 0", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtDCItem);
                }
                if (chkItem == "Accessories")
                {
                    MySqlCommand cmd1 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tblAccessories where Status = 0", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtDCItem);
                }
                if (chkItem == "Others")
                {
                    MySqlCommand cmd1 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tblOthers where Status = 0", conn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dtDCItem);
                }
                
                conn.Close();

                string result = string.Empty;
                List<string> lstStrRow = new List<string>();

                if (dtDCItem.Rows.Count != 0)
                {
                    for (int i = 0; i <= dtDCItem.Rows.Count - 1; i++)
                    {
                        lstStrRow.Add(dtDCItem.Rows[i]["SerialNumber"].ToString() + "^" + dtDCItem.Rows[i]["ModelNumber"].ToString());
                    }

                    context.Response.Write(jSerialize.Serialize(lstStrRow));
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