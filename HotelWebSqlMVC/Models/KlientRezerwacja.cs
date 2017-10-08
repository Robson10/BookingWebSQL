using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotelWebSqlMVC.Models
{
    public class KlientRezerwacja
    {
        public int O_ID;
        public int R_ID;

        private DateTime kiedy = DateTime.MinValue;
        public string Kiedy
        {
            get { return kiedy.ToShortDateString(); }
            set { kiedy = DateTime.Parse(value); }
        }

        private DateTime odkiedy = DateTime.MinValue;
        public string OdKiedy
        {
            get { return odkiedy.ToShortDateString(); }
            set { odkiedy = DateTime.Parse(value); }
        }
        public DateTime doKiedy;
        public string DoKiedy
        {
            get { return doKiedy.ToShortDateString(); }
            set { doKiedy = DateTime.Parse(value); }
        }
        public int PokojID { get; private set; }
        private int pokojNr;
        public int PokojNr
        {
            get { return pokojNr; }
            set
            {
                pokojNr = value;
                setPokojID(value);
            }
        }
        private void setPokojID(int pokojNr)
        {
            string ConnectionString = @"Data Source=DESKTOP-M4NPT9R; Initial Catalog=Database_1;Integrated Security=SSPI"; //important!!!
            string query = "select Pokoj.P_ID from Pokoj where Pokoj.P_NR=" + pokojNr;

            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                string x = "-1";
                try { x = cmd.ExecuteScalar().ToString(); }
                catch { sqlConn.Close(); x= "-1"; }
                sqlConn.Close();
                PokojID=Convert.ToInt32(x);
            }           
        }

    }
}