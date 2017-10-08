using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWebSqlMVC.Models
{
    public class MyRezerwacjeItem
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
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
        public int PokojNr { get; set; }

    }
}