//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelWebSqlMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Osoba
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Osoba()
        {
            this.Rezerwacja = new HashSet<Rezerwacja>();
        }
    
        public int O_ID { get; set; }
        public string O_Imie { get; set; }
        public string O_Nazwisko { get; set; }
        public string O_Mail { get; set; }
        public string O_Tel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rezerwacja> Rezerwacja { get; set; }
    }
}
