using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelWebSqlMVC.Models;
using System.Data.SqlClient;
using System.Data;

namespace HotelWebSqlMVC.Controllers
{
    public class PracownikController : Controller
    {
        private KlienciEntities _dbKlienci = new KlienciEntities();
        private PokojEntities _dbPokoj= new PokojEntities();
        private RezerwacjaEntities _dbRezerwacja = new RezerwacjaEntities();
        // GET: Pracownik/Index
        public ActionResult Index()
        {
            return View();
        }

        #region Pracownik - tabela Klienci
        public ActionResult IndexKlienci()
        {
            return View(_dbKlienci.Osoba.ToList());
        }
        public ActionResult CreateKlienci()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateKlienci(Osoba newOsoba)
        {
            if (ModelState.IsValid)
            {
                _dbKlienci.Osoba.Add(newOsoba);
                _dbKlienci.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(newOsoba);
            }
        }
        public ActionResult EditKlienci(int id)
        {
            var temp = (from Osoba in _dbKlienci.Osoba where Osoba.O_ID == id select Osoba).First();
            return View(temp);
        }
        [HttpPost]
        public ActionResult EditKlienci(Osoba osobaToEdit)
        {
            var temp = (from Osoba in _dbKlienci.Osoba where Osoba.O_ID== osobaToEdit.O_ID select Osoba).First();
            if (!ModelState.IsValid) return View(temp);

            _dbKlienci.Entry(temp).CurrentValues.SetValues(osobaToEdit);
            _dbKlienci.SaveChanges();
            return RedirectToAction("Index");
        }        public ActionResult DetailsKlienci(int id)
        {
            var temp = (from Osoba in _dbKlienci.Osoba where Osoba.O_ID == id select Osoba).First();
            return View(temp);
        }
        #endregion


        #region Pracownik - tabela Pokoje
        public ActionResult IndexPokoje()
        {
            return View(_dbPokoj.Pokoj.ToList());
        }
        public ActionResult CreatePokoje()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePokoje(Pokoj newObj)
        {
            if (ModelState.IsValid)
            {
                _dbPokoj.Pokoj.Add(newObj);
                _dbPokoj.SaveChanges();
                return RedirectToAction("IndexPokoje","Pracownik");
            }
            else
            {
                return View(newObj);
            }
        }
        public ActionResult EditPokoje(int id)
        {
            var temp = (from Pokoj in _dbPokoj.Pokoj where Pokoj.P_ID == id select Pokoj).First();
            return View(temp);
        }
        [HttpPost]
        public ActionResult EditPokoje(Pokoj objToEdit)
        {
            var temp = (from Pokoj in _dbPokoj.Pokoj where Pokoj.P_ID == objToEdit.P_ID select Pokoj).First();
            if (!ModelState.IsValid) return View(temp);

            _dbPokoj.Entry(temp).CurrentValues.SetValues(objToEdit);
            _dbPokoj.SaveChanges();
            return RedirectToAction("IndexPokoje", "Pracownik");
        }        public ActionResult DetailsPokoje(int id)
        {
            var temp = (from Pokoj in _dbPokoj.Pokoj where Pokoj.P_ID == id select Pokoj).First();
            return View(temp);
        }
        public ActionResult DeletePokoje(int id)
        {
            var temp = (from Pokoj in _dbPokoj.Pokoj where Pokoj.P_ID == id select Pokoj).First();
            return View(temp);
        }
        //
        // POST: /Home/Delete/5
        [HttpPost]
        public ActionResult DeletePokoje(Pokoj objToDelete)
        {
            var temp = (from Pokoj in _dbPokoj.Pokoj where Pokoj.P_ID == objToDelete.P_ID select Pokoj).First();
            if (!ModelState.IsValid)
                return View(temp);
            _dbPokoj.Entry(temp).State=System.Data.Entity.EntityState.Deleted;
            _dbPokoj.SaveChanges();
            return RedirectToAction("IndexPokoje", "Pracownik");
        }
        #endregion


        #region Pracownik - tabela rezerwacje
        public ActionResult IndexRezerwacje()
        {
         string ConnectionString = @"Data Source=DESKTOP-M4NPT9R; Initial Catalog=Database_1;Integrated Security=SSPI"; //important!!!
         string query =
 "select Osoba.O_Imie as Imie,Osoba.O_Nazwisko as Nazwisko,Rezerwacja.R_NaKiedy as Od,Rezerwacja.R_DoKiedy as Do,Pokoj.P_Nr as NrPokoju,Pokoj.P_KosztDzienny as KosztDzienny from Rezerwacja" +
 " inner join Osoba on Rezerwacja.O_ID=Osoba.O_ID" +
 " inner join Relationship_2 on Relationship_2.R_ID=Rezerwacja.R_ID" +
 " inner join Pokoj on Pokoj.P_ID = Relationship_2.P_ID" +
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
                            Nazwisko= (string)ds.Tables[0].Rows[i][1],
                            OdKiedy = ((DateTime) ds.Tables[0].Rows[i][2]).ToShortDateString(),
                            DoKiedy = ((DateTime)ds.Tables[0].Rows[i][3]).ToShortDateString(),
                            PokojNr = (Int32)ds.Tables[0].Rows[i][4]
                        });
                    }
                    return View(temp);
                }
                catch (Exception ex)
                { return View(); }
            }
        }
       

        #endregion
    }
}
