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
    /// Summary description for GetCompDetails
    /// </summary>
    public class GetCompDetails : IHttpHandler
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

                DataTable dtDCComp = new DataTable();
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM tblCompany", conn);
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                da1.Fill(dtDCComp);
                List<string> lstCompdet = new List<string>();
                string compDet = string.Empty;
                for (int i = 0; i <= dtDCComp.Rows.Count - 1; i++)
                {
                    compDet = dtDCComp.Rows[i]["Name"].ToString() + '%' + dtDCComp.Rows[i]["CompanyId"].ToString() + '%' + dtDCComp.Rows[i]["Address"].ToString() + '%' + dtDCComp.Rows[i]["Phone"].ToString() + '%' + dtDCComp.Rows[i]["Location"].ToString();
                    lstCompdet.Add(compDet);
                }

                conn.Close();
                string result = string.Empty;
                context.Response.Write(jSerialize.Serialize(lstCompdet));
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