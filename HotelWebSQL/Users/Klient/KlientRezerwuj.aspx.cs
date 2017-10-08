using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelWebSQL.Users.Klient
{
    public partial class KlientRezerwuj : System.Web.UI.Page
    {
        List<Pokoj> NowaListaTowarow = new List<Pokoj>();
        List<Pokoj> Rachunek = new List<Pokoj>();
        int KlientID = 0;//trzeba pobierać z wczesniej przy logowaniu
        DateTime KiedyKupiono = new DateTime();
        protected void Page_Load(object sender, EventArgs e)
        {
            var temp = (List<Pokoj>)ViewState["NowaListaTowarow"];
            if (temp != null && temp.Count > 0)
                NowaListaTowarow = (List<Pokoj>)ViewState["NowaListaTowarow"];
            else
            {
                if (PokojIluOsobowy.Items.Count < 1)
                {
                    string query = "select Pokoj.P_IluOsobowy from Pokoj group by Pokoj.P_IluOsobowy";
                    BaseOperation(new GridView(), PokojIluOsobowy, query, 1);
                    Page.Session.Add("NowaListaTowarow", NowaListaTowarow);
                    ViewState["NowaListaTowarow"] = NowaListaTowarow;
                }
            }
        }

        private void BaseOperation(GridView Grid, DropDownList combo, string query, int ListType)
        {
            using (SqlConnection sqlConn = new SqlConnection(Helper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                Grid.DataSource = dt;
                if (ListType == 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow x = dt.Rows[i];
                        NowaListaTowarow.Add(new Pokoj(x.Field<Int32>(0)));
                        combo.Items.Add(NowaListaTowarow[i].IluOsobowy.ToString());
                    }
                }
            }
        }
        private void aktualizujGrida()
        {
            Grid.DataSource = null;
            Grid.DataSource = (Rachunek);
            Grid.DataBind();
        }
        protected void Szuakj_Click(object sender, EventArgs e)
        {
            if (PokojIluOsobowy.SelectedIndex == -1)
                return;
            var x = KalendarzOd.calendar.SelectedDate;
            string RezerwacjaOd = "'" + x.Year + "." +
                                  ((x.Month < 10) ? ("0" + x.Month.ToString()) : x.Month.ToString()) + "." +
                                  ((x.Day < 10) ? "0" + x.Day.ToString() : x.Day.ToString()) + "'";
            x = KalendarzDo.calendar.SelectedDate;
            string RezerwacjaDo = "'" + x.Year + "." +
                                  ((x.Month < 10) ? ("0" + x.Month.ToString()) : x.Month.ToString()) + "." +
                                  ((x.Day < 10) ? "0" + x.Day.ToString() : x.Day.ToString()) + "'";
            var query = "select DISTINCT " +
                        "Pokoj.P_Nr as NrPokoju, " +
                        "Pokoj.P_KosztDzienny as KosztDzienny" +
                        " from Pokoj" +
                        " where " +
                        " Pokoj.P_IluOsobowy = " + PokojIluOsobowy.Items[PokojIluOsobowy.SelectedIndex].ToString() + " and" +
                        " Pokoj.P_CzyGotowy != 0 and" +
                        " Pokoj.P_ID not in" +
                        " (" +
                        " select Pokoj.P_ID" +
                        " from Pokoj" +
                        " inner join Relationship_2 on Relationship_2.P_ID = Pokoj.P_ID" +
                        " inner join Rezerwacja on Rezerwacja.R_ID = Relationship_2.R_ID" +
                        " where" +
                        " Pokoj.P_IluOsobowy = " + PokojIluOsobowy.Items[PokojIluOsobowy.SelectedIndex].ToString() +
                        " and" +
                        " (" +
                        " Rezerwacja.R_NaKiedy <= " + RezerwacjaOd + " and" +
                        " Rezerwacja.R_DoKiedy >= " + RezerwacjaOd + "" +
                        " or" +
                        " Rezerwacja.R_NaKiedy <= " + RezerwacjaDo + " and" +
                        " Rezerwacja.R_DoKiedy >= " + RezerwacjaDo + "" +
                        " )" +
                        " )";
            GridView temp = new GridView();
            Helper.Select(temp,query);
            Session["Rezerwacje"] = temp;
            Grid.DataSource = temp.DataSource;
            Grid.DataBind();
            if (Grid.Columns.Count > 1)
                Grid.Columns[1].Visible = false;
        }

        protected void Zatwierdz_Click(object sender, EventArgs e)
        {
            KiedyKupiono = DateTime.Now;
            if (KalendarzOd.SelectedDate > KalendarzDo.SelectedDate)
            {
                Helper.MsgBox("Ustawiłeś błędny zakres dni rezerwacji", Page, this);
            }
            else if (KalendarzOd.SelectedDate < KiedyKupiono.AddDays(0))
            {
                Helper.MsgBox("Nie możesz dokonać rezerwacji na dni ktore przeminęły",Page,this);
            }
            else
            {
                string idPokoju;
                try
                {
                    var tempId = (GridView) Session["Rezerwacje"];
                    idPokoju = tempId.Rows[Grid.SelectedIndex].Cells[0].Text;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Helper.MsgBox("Zaznacz interesujący cię pokój (wiersz)",Page,this);
                    return;
                }
                string DataRezerwacji = "'" + KiedyKupiono.Year + "." +
                                        ((KiedyKupiono.Month < 10)
                                            ? ("0" + KiedyKupiono.Month.ToString())
                                            : KiedyKupiono.Month.ToString()) + "." +
                                        ((KiedyKupiono.Day < 10)
                                            ? "0" + KiedyKupiono.Day.ToString()
                                            : KiedyKupiono.Day.ToString()) + "'";
                var x = KalendarzOd.SelectedDate;
                string RezerwacjaOd = "'" + x.Year + "." +
                                      ((x.Month < 10) ? ("0" + x.Month.ToString()) : x.Month.ToString()) + "." +
                                      ((x.Day < 10) ? "0" + x.Day.ToString() : x.Day.ToString()) + "'";
                x = KalendarzDo.SelectedDate;
                string RezerwacjaDo = "'" + x.Year + "." +
                                      ((x.Month < 10) ? ("0" + x.Month.ToString()) : x.Month.ToString()) + "." +
                                      ((x.Day < 10) ? "0" + x.Day.ToString() : x.Day.ToString()) + "'";

                var query = "insert into Database_1.dbo.Rezerwacja(R_Kiedy, R_NaKiedy, R_DoKiedy, O_ID)" +
                            "values(" + DataRezerwacji + "," + RezerwacjaOd + "," + RezerwacjaDo + "," + (int)Session["UserID"] +
                            ")";
                Helper.InsertData(query, "Zamowienia");
                //dodanie jeszcze do asocjacji połaczenie z pokojem
                GridView temp = new GridView();
                query =
                    "select Rezerwacja.R_ID ,Rezerwacja.R_Kiedy from Database_1.dbo.Rezerwacja where Rezerwacja.R_Kiedy = " +
                    DataRezerwacji + "";
                Helper.Select(temp,query);
                int idRezerwacji = Int32.Parse(temp.Rows[0].Cells[0].Text);
                query = "insert into Database_1.dbo.Relationship_2(P_ID, R_ID)" +
                        "values(" + idPokoju + "," + idRezerwacji + ")";
                Helper.InsertData(query, "Relationsip_2");
                Response.Redirect("~/Users/Klient/KlientForm.aspx");
            }

            
        }

        protected void Anuluj_Click(object sender, EventArgs e)
        {
            //zamkniecie strony i powrot do glownej strony klienta
            Response.Redirect("~/Users/Klient/KlientForm.aspx");
        }
    }
    [Serializable]
    class Pokoj
    {
        public Pokoj(int id)
        {
            IluOsobowy = id;
        }
        public int IluOsobowy { get; private set; }
    }
}