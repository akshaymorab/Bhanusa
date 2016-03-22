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
    /// Summary description for PostPODetail
    /// </summary>
    public class PostPODetail : IHttpHandler
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
                    MySqlConnection conn = new MySqlConnection(connString);
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("INSERT INTO tblPO VALUES (@pono,@pocomp,@strtdte,@enddte,@renewdt)", conn);
                    cmd1.Parameters.AddWithValue("@pono", itmDet[0]);
                    cmd1.Parameters.AddWithValue("@pocomp", itmDet[1]);
                    cmd1.Parameters.AddWithValue("@strtdte", itmDet[2]);
                    cmd1.Parameters.AddWithValue("@enddte", itmDet[3]);
                    cmd1.Parameters.AddWithValue("@renewdt", itmDet[4]);
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    result = ex.Message.ToString();
                }

                result = itmDet[1] + " Added Successfully";

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