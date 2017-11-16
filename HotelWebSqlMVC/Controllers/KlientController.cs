using HotelWebSqlMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelWebSqlMVC.Controllers
{
    public class KlientController : Controller
    {
        int KlientID=0;
        private RezerwacjaEntities _dbRezerwacja = new RezerwacjaEntities();
        private PokojEntities _dbPokoj = new PokojEntities();
        //get: Klient/Rezerwacja
        public ActionResult Rezerwacje()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Rezerwacje(KlientRezerwacja newRezerwacja, string ConnectionString = @"Data Source=DESKTOP-M4NPT9R; Initial Catalog=Database_1;Integrated Security=SSPI")
        {
            Session["RezerwacjaTextErr"] = "";
            var isCorrect = true;
            if (ModelState.IsValid)
            {
                newRezerwacja.O_ID = (int)Session["UserID"];
                newRezerwacja.Kiedy = DateTime.Now.ToShortDateString();
                if (DateTime.Parse(newRezerwacja.OdKiedy) < DateTime.Parse(newRezerwacja.Kiedy))
                {
                    Session["RezerwacjaTextErr"] += "Musisz dokonać rezerwacji na minimum 1 dzień na przód" + Environment.NewLine;
                    isCorrect = false;
                }
                if (DateTime.Parse(newRezerwacja.OdKiedy) <= DateTime.Parse(newRezerwacja.Kiedy))
                {
                    Session["RezerwacjaTextErr"] += "Data zakończenia rezerwacji pokoju jest błędna" + Environment.NewLine;
                    isCorrect = false;
                }
                if (DateTime.Parse(newRezerwacja.OdKiedy) >= DateTime.Parse(newRezerwacja.DoKiedy))
                {
                    Session["RezerwacjaTextErr"] += "Ilość rezerwowanych dni jest <= 0" + Environment.NewLine;
                    isCorrect = false;
                }
                if (newRezerwacja.PokojID < 0)
                {
                    Session["RezerwacjaTextErr"] += "Podany nr pokoju nie istnieje" + Environment.NewLine;
                    isCorrect = false;
                }
                if (!isCorrect)
                {
                    return View(newRezerwacja);
                }
                else
                {
                    var query = "";
                    string RezerwacjaOd = DateToSqlStringDate(DateTime.Parse(newRezerwacja.OdKiedy));
                    string RezerwacjaDo = DateToSqlStringDate(DateTime.Parse(newRezerwacja.DoKiedy));
                    string RezerwacjaKiedy = DateToSqlStringDate(DateTime.Parse(newRezerwacja.Kiedy));
                    query = "insert into Database_1.dbo.Rezerwacja(R_Kiedy, R_NaKiedy, R_DoKiedy, O_ID)" +
                                "values(" + RezerwacjaKiedy + "," + RezerwacjaOd + "," + RezerwacjaDo + "," + (int)Session["UserID"] +
                                ")";
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        con.Open();
                        SqlCommand command = new SqlCommand(query, con);
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    query = "select Rezerwacja.R_ID from Database_1.dbo.Rezerwacja where Rezerwacja.R_Kiedy = " +
                        DateToSqlStringDate(DateTime.Parse(newRezerwacja.Kiedy)) + "";
                    int idRezerwacji = -1;
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        string x = "-1";
                        try { x = cmd.ExecuteScalar().ToString(); }
                        catch { con.Close(); x = "-1"; }
                        con.Close();
                        idRezerwacji = Convert.ToInt32(x);
                    }
                    //dodanie jeszcze do asocjacji połaczenie z pokojem
                    query = "insert into Database_1.dbo.Relationship_2(P_ID, R_ID)" +
                            "values(" + newRezerwacja.PokojID + "," + idRezerwacji + ")";
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        con.Open();
                        SqlCommand command = new SqlCommand(query, con);
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(newRezerwacja);
            }
        }
        private string DateToSqlStringDate(DateTime x)
        {
            return "'" + x.Year + "." +
                  ((x.Month < 10) ? ("0" + x.Month.ToString()) : x.Month.ToString()) + "." +
                  ((x.Day < 10) ? "0" + x.Day.ToString() : x.Day.ToString()) + "'";
        }
        //GET: Klient/Index
        public ActionResult Index()
        {
            KlientID = (int)Session["UserID"];
            string ConnectionString = @"Data Source=DESKTOP-M4NPT9R; Initial Catalog=Database_1;Integrated Security=SSPI"; //important!!!
            string query =
    "select Osoba.O_Imie as Imie,Osoba.O_Nazwisko as Nazwisko,Rezerwacja.R_NaKiedy as Od,Rezerwacja.R_DoKiedy as Do,Pokoj.P_Nr as NrPokoju,Pokoj.P_KosztDzienny as KosztDzienny from Rezerwacja" +
    " inner join Osoba on Rezerwacja.O_ID=Osoba.O_ID" +
    " inner join Relationship_2 on Relationship_2.R_ID=Rezerwacja.R_ID" +
    " inner join Pokoj on Pokoj.P_ID = Relationship_2.P_ID" +
    " where Osoba.O_ID="+KlientID+
    " order by Osoba.O_Imie, Osoba.O_Nazwisko, Rezerwacja.R_NaKiedy, Rezerwacja.R_DoKiedy asc";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    List<Models.MyRezerwacjeItem> temp = new List<MyRezerwacjeItem>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        temp.Add(new MyRezerwacjeItem()
                        {
                            Imie = (string)ds.Tables[0].Rows[i][0],
                            Nazwisko = (string)ds.Tables[0].Rows[i][1],
                            OdKiedy = ((DateTime)ds.Tables[0].Rows[i][2]).ToShortDateString(),
                            DoKiedy = ((DateTime)ds.Tables[0].Rows[i][3]).ToShortDateString(),
                            PokojNr = (Int32)ds.Tables[0].Rows[i][4]
                        });
                    }
                    return View(temp);

            }
            return View();
        }
        public ActionResult Pokoje()
        {           
            return View(_dbPokoj.Pokoj.ToList());
        }
    }
}