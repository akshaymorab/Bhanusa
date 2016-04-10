/*<!--/***********************************************************************

Page Title: Bhanusaa Super Admin

Author: Akshay R Morab

Company: Souratron Pvt. Ltd.

Decription: Contains the following Inventory Application of
            DC, Stocks, Employee for Bhanusaa.

Created Date: 05/12/2015 02:19

Modified Date: 02/01/2016 17:33

************************************************************************/
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
    /// Summary description for PostUserDetail
    /// </summary>
    public class PostUserDetail : IHttpHandler
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

                DataTable dtUserDetail = new DataTable();

                MySqlConnection conn = new MySqlConnection(connString);
                string[] chkUserDetails = chkUser.Split('*');
                
                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO tblEmployeeDetails VALUES (@empid,@empname,@emprole,@emppass,@empmobile,@empemail,@empaddress)", conn);
                var mobile = chkUserDetails[4];
                cmd1.Parameters.AddWithValue("@empid", chkUserDetails[0]);
                cmd1.Parameters.AddWithValue("@empname", chkUserDetails[1]);
                cmd1.Parameters.AddWithValue("@emprole", chkUserDetails[2]);
                cmd1.Parameters.AddWithValue("@emppass", chkUserDetails[3]);
                cmd1.Parameters.AddWithValue("@empmobile", mobile);
                cmd1.Parameters.AddWithValue("@empemail", chkUserDetails[5]);
                cmd1.Parameters.AddWithValue("@empaddress", chkUserDetails[6]);
                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();

                string result = string.Empty;
                result = chkUserDetails[1] + " Added Successfully";

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