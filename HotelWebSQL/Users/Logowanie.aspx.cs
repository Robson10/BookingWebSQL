using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelWebSQL.Users
{
    public partial class Logowanie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserID"] = -1;
        }

        protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            if (!tryConnectAsClient(Login1.UserName, Login1.Password))
            {
                if (!tryConnectAsEmpoyee(Login1.UserName, Login1.Password))
                {
                    Response.Redirect(Request.RawUrl);
                }
                else Response.Redirect("~/Users/Pracownik/PracownikForm.aspx");
            }
            else Response.Redirect("~/Users/Klient/KlientForm.aspx");
        }

        private bool tryConnectAsEmpoyee(string login, string password)
        {
            string EmployeeQuerry = "select Pracownik.Pra_ID from Database_1.dbo.Pracownik where Pracownik.Pra_Login=@Login and Pracownik.Pra_Haslo=@Password";
            int result = GetUserData(EmployeeQuerry, login, password);
            if (result == -1)
            {
                return false;
            }
            Session["UserID"] = result;
            return true;
        }
        private bool tryConnectAsClient(string login, string password)
        {
            string KlienciQuerry = "select Osoba.O_ID from Database_1.dbo.Osoba where Osoba.O_Imie=@Login and Osoba.O_Nazwisko=@Password";
            int result = GetUserData(KlienciQuerry, login, password);
            if (result == -1)
            {
                return false;
            }
            Session["UserID"] = result;
            return true;
        }

        private int GetUserData(string query, string Login, string Password)
        {
            using (SqlConnection sqlConn = new SqlConnection(Helper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                cmd.Parameters.AddWithValue("@Login", Login);
                cmd.Parameters.AddWithValue("@Password", Password);
                string x = "-1";
                try { x = cmd.ExecuteScalar().ToString(); }
                catch { sqlConn.Close(); return -1; }
                sqlConn.Close();
                return Convert.ToInt32(x);
            }
        }
    }
}