using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelWebSQL.Users.Pracownik
{
    public partial class PracownikForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void PrzegladajDaneKlientowBT_Click(object sender, EventArgs e)
        {
            GridKlienci.Visible = true;
            RezerwacjeGrid.Visible = false;
            GridPokoje.Visible = false;
        }
        private string daneRezerwacji =
            "select Osoba.O_Imie as Imie,Osoba.O_Nazwisko as Nazwisko,Rezerwacja.R_NaKiedy as Od,Rezerwacja.R_DoKiedy as Do,Pokoj.P_Nr as NrPokoju,Pokoj.P_KosztDzienny as KosztDzienny from Rezerwacja" +
            " inner join Osoba on Rezerwacja.O_ID=Osoba.O_ID" +
            " inner join Relationship_2 on Relationship_2.R_ID=Rezerwacja.R_ID" +
            " inner join Pokoj on Pokoj.P_ID = Relationship_2.P_ID" +
            " order by Osoba.O_Imie, Osoba.O_Nazwisko, Rezerwacja.R_NaKiedy, Rezerwacja.R_DoKiedy asc";
        protected void PrzegladajRezerwacjeBT_Click(object sender, EventArgs e)
        {
            GridKlienci.Visible = false;
            RezerwacjeGrid.Visible = true;
            GridPokoje.Visible = false;
            GridView temp = new GridView();
            Helper.Select(temp, daneRezerwacji);
            Session["daneRezerwacji"] = temp;
            RezerwacjeGrid.DataSource = temp.DataSource;
            RezerwacjeGrid.DataBind();

        }

        protected void PrzegladajPokojeBT_Click(object sender, EventArgs e)
        {
            GridKlienci.Visible = false;
            RezerwacjeGrid.Visible = false;
            GridPokoje.Visible = true;
        }


        protected void WylogujBT_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Users/Logowanie.aspx");
        }


        protected void SqlDataSource5_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            for (int i = 0; i < RezerwacjeGrid.Rows.Count; i++)
            {
                RezerwacjeGrid.DataBind();
                GridPokoje.Visible = false;

            }
        }

        protected void RezerwacjeGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            var id = e.RowIndex;
            var nrPokoju=(Session["daneRezerwacji"] as GridView).Rows[id].Cells[4].Text;

        }

        protected void RezerwacjeGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RezerwacjeGrid.EditIndex = e.NewEditIndex;
            RezerwacjeGrid.DataSource = ((GridView)Session["daneRezerwacji"]).DataSource;
            RezerwacjeGrid.DataBind();
        }

        protected void RezerwacjeGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var temp =e.NewValues;

        }
    }
    public class Pracownik
    {
        public int Pr_ID { get; set; }
        public string Pr_Imie { get; set; }
        public string Pr_Nazwisko { get; set; }
        public string Pr_AdresPracy { get; set; }
    }
}