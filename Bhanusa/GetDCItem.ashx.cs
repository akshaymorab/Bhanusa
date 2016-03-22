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
    /// Summary description for GetDCItem
    /// </summary>
    public class GetDCItem : IHttpHandler
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
                var dcno = jsonString;

                DataTable dtDC = new DataTable();
                DataTable dtDCRent = new DataTable();
                DataTable dtDCItem = new DataTable();

                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Date FROM tblDC where DCNo = '" + dcno + "'", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dtDC);
                conn.Close();

                string dt = dtDC.Rows[0]["Date"].ToString();

                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM tblRentItem where DCNo = '" + dcno + "'", conn);
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                da1.Fill(dtDCRent);
                conn.Close();
                List<string> lstRentCode = new List<string>();
                List<string> lstParticular = new List<string>();
                List<string> lstConfig = new List<string>();
                List<string> lstQty = new List<string>();
                List<string> lstSts = new List<string>();
                string strRtCode = string.Empty;
                string strRtPart = string.Empty;
                string strRtConfig = string.Empty;
                string strRtQty = string.Empty;
                string strRtSts = string.Empty;
                string strItem = string.Empty;

                for (int k = 0; k <= dtDCRent.Rows.Count - 1; k++)
                {
                    strRtCode = dtDCRent.Rows[k]["RentCode"].ToString();
                    strRtPart = dtDCRent.Rows[k]["Particular"].ToString();
                    strRtConfig = dtDCRent.Rows[k]["Configuration"].ToString();
                    strRtQty = dtDCRent.Rows[k]["Quantity"].ToString();
                    strRtSts = dtDCRent.Rows[k]["Status"].ToString();
                    if (!lstRentCode.Contains(strRtCode))
                    {
                        lstRentCode.Add(strRtCode);
                        lstParticular.Add(strRtPart);
                        lstConfig.Add(strRtConfig);
                        lstQty.Add(strRtQty);
                        lstSts.Add(strRtSts);
                    }
                }

                string strRent = string.Empty;
                string strRes = string.Empty;
                for (int i = 0; i <= lstRentCode.Count - 1; i++)
                {
                    conn.Open();
                    MySqlCommand cmd2 = new MySqlCommand("SELECT SerialNumber, ModelNumber FROM tbl" + lstParticular[i].ToString() + " where RentCode = '" + lstRentCode[i].ToString() + "'", conn);
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    da2.Fill(dtDCItem);
                    conn.Close();
                    
                    for (int j = 0; j <= dtDCItem.Rows.Count - 1; j++)
                    {
                        if (j == 0)
                        {
                            strItem = dtDCItem.Rows[j]["SerialNumber"].ToString() + "$" + dtDCItem.Rows[j]["ModelNumber"].ToString();
                        }
                        else
                        {
                            strItem = strItem + "%" + dtDCItem.Rows[j]["SerialNumber"].ToString() + "$" + dtDCItem.Rows[j]["ModelNumber"].ToString();
                        }
                    }
                    if (i == 0)
                    {
                        strRes = lstParticular[i].ToString() + ">" + lstConfig[i].ToString() + ">" + lstQty[i].ToString() + ">" + lstSts[i].ToString() + ">" + lstRentCode[i].ToString() + "?" + strItem;
                    }
                    else
                    {
                        strRes = strRes + "%" + lstParticular[i].ToString() + ">" + lstConfig[i].ToString() + ">" + lstQty[i].ToString() + ">" + lstSts[i].ToString() + ">" + lstRentCode[i].ToString() + "?" + strItem;
                    }
                    
                    dtDCItem.Rows.Clear();
                }

                string result = strRes + "&" + dt;
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