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
        public ActionResult Rezerwacje(KlientRezerwacja newRezerwacja)
        {
            if (ModelState.IsValid)
            {
                newRezerwacja.O_ID=(int)Session["UserID"];
                newRezerwacja.Kiedy = DateTime.Now.ToShortDateString();
                //sprawdzenie czy wolny pokoj
                //_dbRezerwacja.Rezerwacja.Add(newRezerwacja);
                //_dbRezerwacja.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(newRezerwacja);//to samo dla błędnej daty lub błędnego pokoju (jako new view)
            }
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
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
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
                catch (Exception ex)
                { return View(); }
            }
            return View();
        }
        public ActionResult Pokoje()
        {           
            return View(_dbPokoj.Pokoj.ToList());
        }
    }
}