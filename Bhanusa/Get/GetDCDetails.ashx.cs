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
    /// Summary description for GetDCDetails
    /// </summary>
    public class GetDCDetails : IHttpHandler
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
                var chkDC = jsonString;

                DataTable dtDCDetails = new DataTable();

                MySqlConnection conn = new MySqlConnection(connString);
                string[] chkDCDetails = chkDC.Split(',');
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM tblDC ORDER BY DCNo DESC", conn);
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                da1.Fill(dtDCDetails);
                conn.Close();

                string result = string.Empty;
                string strRowRes = string.Empty;

                if (dtDCDetails.Rows.Count != 0)
                {
                    for (int i = 0; i <= dtDCDetails.Rows.Count - 1; i++)
                    {
                        if (i == 0)
                        {
                            strRowRes = dtDCDetails.Rows[i]["DCNo"].ToString() + "*" + dtDCDetails.Rows[i]["Company"].ToString() + "*" + dtDCDetails.Rows[i]["Address"].ToString() + "*" + dtDCDetails.Rows[i]["Location"].ToString() + "*" + dtDCDetails.Rows[i]["Date"].ToString() + "*" + dtDCDetails.Rows[i]["Remarks"].ToString() + "*" + dtDCDetails.Rows[i]["Updated"].ToString() + "*" + dtDCDetails.Rows[i]["UpdateTime"].ToString();
                        }
                        else
                        {
                            strRowRes = strRowRes + ";" + dtDCDetails.Rows[i]["DCNo"].ToString() + "*" + dtDCDetails.Rows[i]["Company"].ToString() + "*" + dtDCDetails.Rows[i]["Address"].ToString() + "*" + dtDCDetails.Rows[i]["Location"].ToString() + "*" + dtDCDetails.Rows[i]["Date"].ToString() + "*" + dtDCDetails.Rows[i]["Remarks"].ToString() + "*" + dtDCDetails.Rows[i]["Updated"].ToString() + "*" + dtDCDetails.Rows[i]["UpdateTime"].ToString();
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