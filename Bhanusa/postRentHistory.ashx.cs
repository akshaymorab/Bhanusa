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
    /// Summary description for postRentHistory
    /// </summary>
    public class postRentHistory : IHttpHandler
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
                string reDet = jsonString;
                string[] strReDetails = reDet.Split('>');
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO tblRentHistory VALUES DCNo=@dcno, SerialNumber=@serialnumber, ModelNumber=@modelnumber,Configuration=@configutation, Quantity=@quantity, Company=@company",conn);
                cmd.Parameters.AddWithValue("@dcno", strReDetails[0]);
                cmd.Parameters.AddWithValue("@company", strReDetails[1]);
                cmd.Parameters.AddWithValue("@serialnumber", strReDetails[14]);
                cmd.Parameters.AddWithValue("@modelnumber", strReDetails[8]);
                cmd.Parameters.AddWithValue("@configuration", strReDetails[9]);
                cmd.Parameters.AddWithValue("@quantity", strReDetails[10]);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT  Date FROM tblDC WHERE DCNo="+strReDetails[0], conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var date = dt.Rows[0]["Date"];
                conn.Close();
                conn.Open();
                MySqlCommand cmd2 = new MySqlCommand("INSERT INTO tblRentHistory VALUES StartDate=@startdate",conn);
                cmd2.Parameters.AddWithValue("@startdate",date);
                conn.Close();

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