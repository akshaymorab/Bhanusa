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
    /// Summary description for GetDesktopDetails
    /// </summary>
    public class GetDesktopDetails : IHttpHandler
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
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + jsonStr, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                
                string[] strRent = null;
                if (dt.Rows.Count >= 0)
                {

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string st = string.Empty;
                        st = dt.Rows[i]["RentCode"].ToString();

                        if (st != "")
                        {
                            strRent = st.Split('r');
                            if (strRent[0] != "")
                            {
                                int dcno = Convert.ToInt32(strRent[0]);
                                DataTable dt1 = new DataTable();
                                MySqlConnection con1 = new MySqlConnection(strCon);
                                con1.Open();
                                MySqlCommand cmd1 = new MySqlCommand("SELECT Configuration, Quantity, Status, (SELECT Company From tblDC WHERE DCNo=" + dcno + ") AS Company FROM tblRentItem WHERE RentCode='" + st + "'", con1);
                                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                                da1.Fill(dt1);
                                con1.Close();
                                int sts =Convert.ToInt32(dt.Rows[i]["Status"].ToString());
                                if (dt1.Rows.Count != 0 && sts != 0)
                                {
                                    if (i == 0)
                                    {
                                        str = dt.Rows[i]["SerialNumber"].ToString() + ";" + dt.Rows[i]["ModelNumber"].ToString() + ";" + dt.Rows[i]["Status"].ToString() + ";" + strRent[0] + ";" + dt1.Rows[0]["Company"].ToString() + ";" + dt1.Rows[0]["Configuration"].ToString() + ";" + dt1.Rows[0]["Quantity"].ToString() + ";" + dt1.Rows[0]["Status"].ToString();

                                    }
                                    else
                                    {
                                        str = str + "%" + dt.Rows[i]["SerialNumber"].ToString() + ";" + dt.Rows[i]["ModelNumber"].ToString() + ";" + dt.Rows[i]["Status"].ToString() + ";" + strRent[0] + ";" + dt1.Rows[0]["Company"].ToString() + ";" + dt1.Rows[0]["Configuration"].ToString() + ";" + dt1.Rows[0]["Quantity"].ToString() + ";" + dt1.Rows[0]["Status"].ToString();

                                    }
                                }
                            }

                        }

                        else
                        {
                            if (i == 0)
                            {
                                str = dt.Rows[i]["SerialNumber"].ToString() + ";" + dt.Rows[i]["ModelNumber"].ToString() + ";" + dt.Rows[i]["Status"].ToString() + ";" + "--" + ";" + "--" + ";" + "--"  +";" + "--"  +";" + "--";

                            }
                            else
                            {
                                str = str + "%" + dt.Rows[i]["SerialNumber"].ToString() + ";" + dt.Rows[i]["ModelNumber"].ToString() + ";" + dt.Rows[i]["Status"].ToString() + ";" + "--" + ";" + "--" + ";" + "--" + ";" + "--" + ";" + "--";

                            }
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