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
    /// Summary description for PostCompDetail
    /// </summary>
    public class PostCompDetail : IHttpHandler
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
                var compDet = jsonString;

                string[] strCompDet = compDet.Split('%');

                string result = string.Empty;
                DataTable dtDCComp = new DataTable();
                if (strCompDet[5] == "btnCompAdd")
                {
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand cmd1 = new MySqlCommand("INSERT INTO tblCompany VALUES (@compid,@compname,@compadd,@compphone,@comploc)", conn);
                    cmd1.Parameters.AddWithValue("@compid", strCompDet[0]);
                    cmd1.Parameters.AddWithValue("@compname", strCompDet[1]);
                    cmd1.Parameters.AddWithValue("@compadd", strCompDet[2]);
                    cmd1.Parameters.AddWithValue("@compphone", strCompDet[3]);
                    cmd1.Parameters.AddWithValue("@comploc", strCompDet[4]);
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    result = strCompDet[1] + " Added Successfully";
                }
                else
                {
                    MySqlConnection conn = new MySqlConnection(connString);
                    MySqlCommand cmd1 = new MySqlCommand("Update tblCompany SET Name=@compname,Address=@compadd,Phone=@compphone,Location=@comploc where CompanyId=@compid", conn);
                    cmd1.Parameters.AddWithValue("@compid", strCompDet[0]);
                    cmd1.Parameters.AddWithValue("@compname", strCompDet[1]);
                    cmd1.Parameters.AddWithValue("@compadd", strCompDet[2]);
                    cmd1.Parameters.AddWithValue("@compphone", strCompDet[3]);
                    cmd1.Parameters.AddWithValue("@comploc", strCompDet[4]);
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    result = strCompDet[1] + " Updated Successfully";
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