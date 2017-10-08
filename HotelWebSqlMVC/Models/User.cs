using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//https://www.codeproject.com/Articles/482546/Creating-a-custom-user-login-form-with-NET-Csharp
namespace HotelWebSqlMVC.Models
{
    public class User
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }
        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public string IsEmpoyee(string _username, string _password)
        {
            using (var cn = new SqlConnection(@"Data Source=DESKTOP-M4NPT9R; Initial Catalog=Database_1;Integrated Security=SSPI"))
            {
                string _sql = @"select Pra_Id from Database_1.dbo.Pracownik where Pra_Login='" + _username + "' and Pra_Haslo='" + _password + "'";
                var cmd = new SqlCommand(_sql, cn);
                cn.Open();
                string id = "";
                try
                {
                    id = cmd.ExecuteScalar().ToString();
                    cmd.Dispose();
                    cn.Close();
                    return id;
                }
                catch
                {                     
                    cmd.Dispose();
                    cn.Close();
                    return "-1";
                }
            }
        }
        public string IsCustomer(string _username, string _password)
        {
            using (var cn = new SqlConnection(@"Data Source=DESKTOP-M4NPT9R; Initial Catalog=Database_1;Integrated Security=SSPI"))
            {
                string query = @"select Osoba.O_ID from Database_1.dbo.Osoba where Osoba.O_Imie=@Login and Osoba.O_Nazwisko=@Password";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@Login", _username);
                    cmd.Parameters.AddWithValue("@Password", _password);
                    string x = "-1";
                    try { x = cmd.ExecuteScalar().ToString(); }
                    catch { cn.Close(); return "-1"; }
                    cn.Close();
                    return x;
                }
            }
        }
    }
}