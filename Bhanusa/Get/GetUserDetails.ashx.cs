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
    /// Summary description for GetUserDetails
    /// </summary>
    public class GetUserDetails : IHttpHandler
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
                var chkUser = jsonString;

                DataTable dtUserDetails = new DataTable();

                MySqlConnection conn = new MySqlConnection(connString);
                string[] chkUserDetails = chkUser.Split(',');
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM tblEmployeeDetails", conn);
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                da1.Fill(dtUserDetails);
                conn.Close();

                string result = string.Empty;
                string strRowRes = string.Empty;

                if (dtUserDetails.Rows.Count != 0)
                {
                    for (int i = 0; i <= dtUserDetails.Rows.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            strRowRes = dtUserDetails.Rows[i]["EmpId"].ToString() + "*" + dtUserDetails.Rows[i]["EmpName"].ToString() + "*" + dtUserDetails.Rows[i]["EmpRole"].ToString() + "*" + dtUserDetails.Rows[i]["EmpPass"].ToString() + "*" + dtUserDetails.Rows[i]["EmpMobile"].ToString() + "*" + dtUserDetails.Rows[i]["EmpEmail"].ToString() + "*" + dtUserDetails.Rows[i]["EmpAddress"].ToString();
                        }
                        else
                        {
                            strRowRes = strRowRes + ";" + dtUserDetails.Rows[i]["EmpId"].ToString() + "*" + dtUserDetails.Rows[i]["EmpName"].ToString() + "*" + dtUserDetails.Rows[i]["EmpRole"].ToString() + "*" + dtUserDetails.Rows[i]["EmpPass"].ToString() + "*" + dtUserDetails.Rows[i]["EmpMobile"].ToString() + "*" + dtUserDetails.Rows[i]["EmpEmail"].ToString() + "*" + dtUserDetails.Rows[i]["EmpAddress"].ToString();
                        }
                    }

                    result = strRowRes;

                    context.Response.Write(jSerialize.Serialize(
                            new
                            {
                                Response = result
                            }));
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