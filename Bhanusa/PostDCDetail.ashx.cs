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
    /// Summary description for PostDCDetail
    /// </summary>
    public class PostDCDetail : IHttpHandler
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
                var dcDet = jsonString;

                DataTable dtDCDetail = new DataTable();
                DataTable dtDC = new DataTable();

                MySqlConnection conn = new MySqlConnection(connString);
                string[] strDCDetails = dcDet.Split('>');


                var startdate = Convert.ToDateTime(DateTime.Now);
                var user = strDCDetails[5].Split(',');
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("Select DCNo from tblDC Order By Date Desc", conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dtDC);
                    conn.Close();

                    List<string> lstDC = new List<string>();

                    for (int k = 0; k <= dtDC.Rows.Count - 1; k++)
                    {
                        lstDC.Add(dtDC.Rows[k]["DCNo"].ToString());
                    }

                    string[] strItem = strDCDetails[6].Split('%');
                    string[] strMdlno = strDCDetails[7].Split('%');

                    string[] strConfig = strDCDetails[8].Split('%');
                    string[] strQty = strDCDetails[9].Split('%');
                    string[] strStatus = strDCDetails[10].Split('%');
                    string[] strRTCode = strDCDetails[11].Split('%');

                    if (strDCDetails[12] == "btnAddDCItem")
                    {
                        if (!lstDC.Contains(strDCDetails[0]))
                        {
                            MySqlCommand cmd1 = new MySqlCommand("INSERT INTO tblDC VALUES (@dcno,@company,@address,@location,@date,@remarks,@updated,@updtTime)", conn);
                            cmd1.Parameters.AddWithValue("@dcno", strDCDetails[0]);
                            cmd1.Parameters.AddWithValue("@company", strDCDetails[1]);
                            cmd1.Parameters.AddWithValue("@address", strDCDetails[2]);
                            cmd1.Parameters.AddWithValue("@location", strDCDetails[3]);
                            cmd1.Parameters.AddWithValue("@date", startdate);
                            cmd1.Parameters.AddWithValue("@remarks", strDCDetails[4]);
                            cmd1.Parameters.AddWithValue("@updated", strDCDetails[5]);
                            cmd1.Parameters.AddWithValue("@updtTime", startdate);
                            conn.Open();
                            cmd1.ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            MySqlCommand cmdRent = new MySqlCommand("Select RentCode from tblRentItem where DCNo ='" + strDCDetails[0] + "'", conn);
                            MySqlDataAdapter daRent = new MySqlDataAdapter(cmdRent);
                            DataTable dtRent = new DataTable();
                            daRent.Fill(dtRent);
                            conn.Close();

                            List<string> lstRent = new List<string>();

                            for (int j = 0; j <= dtRent.Rows.Count - 1; j++)
                            {
                                lstRent.Add(dtRent.Rows[j]["RentCode"].ToString());
                            }

                            for (int i = 0; i <= strRTCode.Length - 1; i++)
                            {
                                MySqlCommand cmd2 = new MySqlCommand("INSERT INTO tblRentItem VALUES (@rentcode,@dcno,@particular,@modelnumber, @config, @qty, @rntstatus)", conn);
                                cmd2.Parameters.AddWithValue("@rentcode", strRTCode[i]);
                                cmd2.Parameters.AddWithValue("@dcno", strDCDetails[0]);
                                cmd2.Parameters.AddWithValue("@particular", strItem[i]);
                                cmd2.Parameters.AddWithValue("@modelnumber", strMdlno[i]);
                                cmd2.Parameters.AddWithValue("@config", strConfig[i]);
                                cmd2.Parameters.AddWithValue("@qty", strQty[i]);
                                cmd2.Parameters.AddWithValue("@rntstatus", strStatus[i]);
                                conn.Open();
                                cmd2.ExecuteNonQuery();
                                conn.Close();

                                MySqlCommand cmd3 = new MySqlCommand("Update tbl" + strItem[i] + " SET Status = @status, RentCode = @rntcode where SerialNumber = @modelnumber", conn);
                                cmd3.Parameters.AddWithValue("@modelnumber", strMdlno[i]);
                                cmd3.Parameters.AddWithValue("@status", 1);
                                cmd3.Parameters.AddWithValue("@rntcode", strRTCode[i]);
                                conn.Open();
                                cmd3.ExecuteNonQuery();
                                conn.Close();
                            }

                            string result = string.Empty;
                            result = "DC Number " + strDCDetails[0] + " Added Successfully";

                            context.Response.Write(jSerialize.Serialize(
                                    new
                                    {
                                        Response = result
                                    }));
                        }
                        else
                        {
                            string result = string.Empty;
                            result = "DC Number " + strDCDetails[0] + " Already Exists";

                            context.Response.Write(jSerialize.Serialize(
                                    new
                                    {
                                        Response = result
                                    }));
                        }
                    }
                    else
                    {
                        MySqlCommand cmdU1 = new MySqlCommand("Update tblDC SET Company=@company,Address=@address,Location=@location,Remarks=@remarks,Updated=@updated,UpdateTime=@update where DCNo=@dcno", conn);
                        cmdU1.Parameters.AddWithValue("@dcno", strDCDetails[0]);
                        cmdU1.Parameters.AddWithValue("@company", strDCDetails[1]);
                        cmdU1.Parameters.AddWithValue("@address", strDCDetails[2]);
                        cmdU1.Parameters.AddWithValue("@location", strDCDetails[3]);
                        cmdU1.Parameters.AddWithValue("@remarks", strDCDetails[4]);
                        cmdU1.Parameters.AddWithValue("@updated", strDCDetails[5]);
                        cmdU1.Parameters.AddWithValue("@update", startdate);
                        conn.Open();
                        cmdU1.ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        MySqlCommand cmdRent = new MySqlCommand("Select RentCode from tblRentItem where DCNo ='" + strDCDetails[0] + "'", conn);
                        MySqlDataAdapter daRent = new MySqlDataAdapter(cmdRent);
                        DataTable dtRent = new DataTable();
                        daRent.Fill(dtRent);
                        conn.Close();

                        List<string> lstRent = new List<string>();

                        for (int j = 0; j <= dtRent.Rows.Count - 1; j++)
                        {
                            lstRent.Add(dtRent.Rows[j]["RentCode"].ToString());
                        }

                        for (int i = 0; i <= strRTCode.Length - 1; i++)
                        {
                            if (lstRent.Contains(strRTCode[i]))
                            {
                                MySqlCommand cmd2 = new MySqlCommand("Update tblRentItem SET DCNo = @dcno, Particular = @particular, ModelNumber= @modelnumber, Configuration = @config, Quantity = @qty, Status = @rntstatus where RentCode = @rentcode", conn);
                                cmd2.Parameters.AddWithValue("@rentcode", strRTCode[i]);
                                cmd2.Parameters.AddWithValue("@dcno", strDCDetails[0]);
                                cmd2.Parameters.AddWithValue("@particular", strItem[i]);
                                cmd2.Parameters.AddWithValue("@modelnumber", strMdlno[i]);
                                cmd2.Parameters.AddWithValue("@config", strConfig[i]);
                                cmd2.Parameters.AddWithValue("@qty", strQty[i]);
                                cmd2.Parameters.AddWithValue("@rntstatus", strStatus[i]);
                                conn.Open();
                                cmd2.ExecuteNonQuery();
                                conn.Close();

                                MySqlCommand cmd3 = new MySqlCommand("Update tbl" + strItem[i] + " SET Status = @status, RentCode = @rentcode where SerialNumber = @modelnumber", conn);
                                cmd3.Parameters.AddWithValue("@modelnumber", strMdlno[i]);
                                cmd3.Parameters.AddWithValue("@status", 1);
                                cmd3.Parameters.AddWithValue("@rentcode", strRTCode[i]);
                                conn.Open();
                                cmd3.ExecuteNonQuery();
                                conn.Close();

                                //Updt history with condition
                            }
                            else
                            {
                                MySqlCommand cmd2 = new MySqlCommand("INSERT INTO tblRentItem VALUES (@rentcode,@dcno,@particular,@modelnumber, @config, @qty, @rntstatus)", conn);
                                cmd2.Parameters.AddWithValue("@rentcode", strRTCode[i]);
                                cmd2.Parameters.AddWithValue("@dcno", strDCDetails[0]);
                                cmd2.Parameters.AddWithValue("@particular", strItem[i]);
                                cmd2.Parameters.AddWithValue("@modelnumber", strMdlno[i]);
                                cmd2.Parameters.AddWithValue("@config", strConfig[i]);
                                cmd2.Parameters.AddWithValue("@qty", strQty[i]);
                                cmd2.Parameters.AddWithValue("@rntstatus", strStatus[i]);
                                conn.Open();
                                cmd2.ExecuteNonQuery();
                                conn.Close();

                                MySqlCommand cmd3 = new MySqlCommand("Update tbl" + strItem[i] + " SET Status = @status, RentCode = @rntcode where SerialNumber = @modelnumber", conn);
                                cmd3.Parameters.AddWithValue("@modelnumber", strMdlno[i]);
                                cmd3.Parameters.AddWithValue("@status", 1);
                                cmd3.Parameters.AddWithValue("@rntcode", strRTCode[i]);
                                conn.Open();
                                cmd3.ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        string result = string.Empty;
                        result = "DC Number " + strDCDetails[0] + " Updated Successfully";

                        context.Response.Write(jSerialize.Serialize(
                                new
                                {
                                    Response = result
                                }));
                    }


                }
                catch (Exception e)
                {
                    string result = string.Empty;
                    result = e.Message.ToString();

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