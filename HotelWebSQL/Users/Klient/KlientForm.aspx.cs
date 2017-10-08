using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace HotelWebSQL.Users.Klient
{
    public partial class KlientForm : System.Web.UI.Page
    {
        private int KlientID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            WebPartZone.EditVerb.Enabled = true;

        }
        protected void TwojeRezerwacje_Click(object sender, EventArgs e)
        {
            KlientID = (int)Session["UserID"];
            string przeglądanieRezerwacji =
                "select Rezerwacja.R_NaKiedy as Od, Rezerwacja.R_DoKiedy as Do, Pokoj.P_Nr as NrPokoju, Pokoj.P_KosztDzienny as KoszDzienny, Pokoj.P_IluOsobowy as IluOsobowy" +
                " from Rezerwacja" +
                " inner join Osoba on Rezerwacja.O_ID = Osoba.O_ID" +
                " inner join Relationship_2 on Rezerwacja.R_ID = Relationship_2.R_ID" +
                " inner join Pokoj on Relationship_2.P_ID = Pokoj.P_ID" +
                " where Osoba.O_ID = "+ KlientID.ToString();
            Helper.Select(Grid, przeglądanieRezerwacji);
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                Grid.Rows[i].Cells[0].Text= "  "+Grid.Rows[i].Cells[0].Text.Substring(0, 10)+ "  ";
                Grid.Rows[i].Cells[1].Text = "  " + Grid.Rows[i].Cells[1].Text.Substring(0, 10)+ "  ";
            }
            Grid.Visible = true;
        }

        protected void Rezerwuj_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Users/Klient/KlientRezerwuj.aspx");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Users/Logowanie.aspx");
        }
    }
}