using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Bhanusa.Get
{
    /// <summary>
    /// Summary description for GetRentHistory
    /// </summary>
    public class GetRentHistory : IHttpHandler
    {
        static string strCon = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            string strItmRow = string.Empty;
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
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tblRentHistory Order By EndDate Desc", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                for (int i = 0; i <= dt.Rows.Count - 1; i++) 
                {
                    string strEnddt = dt.Rows[i]["EndDate"].ToString();
                    DateTime dtime = Convert.ToDateTime(strEnddt);
                    strEnddt = dtime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (i == 0)
                    {
                        strItmRow = dt.Rows[i]["DCNo"].ToString() + "^" + dt.Rows[i]["Company"].ToString() + "^" + dt.Rows[i]["SerialNumber"].ToString() + "^" + dt.Rows[i]["ModelNumber"].ToString() + "^" + dt.Rows[i]["Configuration"].ToString() + "^" + dt.Rows[i]["Quantity"].ToString() + "^" + dt.Rows[i]["StartDate"].ToString() + "^" + strEnddt;
                    }
                    else
                    {
                        strItmRow = strItmRow + "%" + dt.Rows[i]["DCNo"].ToString() + "^" + dt.Rows[i]["Company"].ToString() + "^" + dt.Rows[i]["SerialNumber"].ToString() + "^" + dt.Rows[i]["ModelNumber"].ToString() + "^" + dt.Rows[i]["Configuration"].ToString() + "^" + dt.Rows[i]["Quantity"].ToString() + "^" + dt.Rows[i]["StartDate"].ToString() + "^" + strEnddt;
                    }
                }

                    context.Response.Write(jSerialiser.Serialize(new
                    {
                        Response = strItmRow
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