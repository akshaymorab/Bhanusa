using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Bhanusa
{
    /// <summary>
    /// Summary description for GetbtnDCDetails
    /// </summary>
    public class GetbtnDCDetails : IHttpHandler
    {

        static string strCon = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            string jsonStr = string.Empty;
            HttpContext.Current.Request.InputStream.Position = 0;
            using (System.IO.StreamReader instr = new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
            {
                System.Web.Script.Serialization.JavaScriptSerializer jSerialiser = new System.Web.Script.Serialization.JavaScriptSerializer();
                jsonStr = instr.ReadToEnd();
                string str = string.Empty;
                DataTable dt = new DataTable();
                MySqlConnection con = new MySqlConnection(strCon);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT DCNo, Company, Date from tblDC ORDER BY DCNo ASC", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        DateTime dtime = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                        string strDate = dtime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (i == 0)
                        {
                            str = dt.Rows[i]["DCNo"].ToString() + ";" + dt.Rows[i]["Company"].ToString() + ";" + strDate;
                        }
                        else
                        {
                            str = str + "%" + dt.Rows[i]["DCNo"].ToString() + ";" + dt.Rows[i]["Company"].ToString() + ";" + strDate;
                        }
                    }
                    context.Response.Write(jSerialiser.Serialize(new
                    {
                        Response = str
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