using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Bhanusa
{
    /// <summary>
    /// Summary description for ConfirmUser
    /// </summary>
    public class chkUser : IHttpHandler
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
                if (chkUser != null)
                {

                    DataTable dtUserDetails = new DataTable();

                    if (chkUser.Contains(','))
                    {
                        MySqlConnection conn = new MySqlConnection(connString);
                        string[] chkUserDetails = chkUser.Split(',');
                        conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM tblEmployeeDetails Where EmpId = '" + chkUserDetails[0] + "' AND EmpPass = '" + chkUserDetails[1] + "'", conn);
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                        da1.Fill(dtUserDetails);
                        conn.Close();

                        string result = string.Empty;

                        if (dtUserDetails.Rows.Count != 0)
                        {
                            result = "Welcome, " + dtUserDetails.Rows[0]["EmpName"].ToString() + ";";
                            context.Response.Write(jSerialize.Serialize(
                                    new
                                    {
                                        Response = result
                                    }));
                        }
                    }
                    else
                    {
                        MySqlConnection conn = new MySqlConnection(connString);
                        string[] chkUserDetails = chkUser.Split(',');
                       conn.Open();
                        MySqlCommand cmd2 = new MySqlCommand("SELECT * FROM tblEmployeeDetails Where EmpId = '" + chkUser + "'", conn);
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd2);
                        da1.Fill(dtUserDetails);
                        conn.Close();

                        string result = string.Empty;

                        if (dtUserDetails.Rows.Count != 0)
                        {
                            result = dtUserDetails.Rows[0]["EmpName"].ToString() + "," + dtUserDetails.Rows[0]["EmpRole"].ToString();
                            context.Response.Write(jSerialize.Serialize(
                                    new
                                    {
                                        Response = result
                                    }));
                        }
                        if (dtUserDetails.Rows.Count == 0)
                        {
                            result = "";
                            context.Response.Write(jSerialize.Serialize(
                                    new
                                    {
                                        Response = result
                                    }));
                        }

                    }
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