using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelWebSQL
{
    public class Helper
    {
        public static string ConnectionString = @"Data Source=DESKTOP-RA0M75M\XXX; Initial Catalog=Database_1;Integrated Security=SSPI"; //important!!!

        public static void Select(GridView Grid, string query)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    conn.Close();

                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Grid.DataSource = ds.Tables[0];
                    Grid.DataBind();
                }
                catch (Exception ex)
                { }
            }
        }
        public static void InsertData(string querry, string tableName)
        {
            using (SqlConnection con = new SqlConnection() { ConnectionString = ConnectionString })
            {
                con.Open();
                SqlCommand command = new SqlCommand(querry, con);
                command.ExecuteNonQuery();
                con.Close();
            }
        }
        public static void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }
}