//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FSWEBAPP.Models
{
    using System;
    using System.Collections.Generic;
    public partial class Siparisler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Siparisler()
        {
            this.Sepet = new HashSet<Sepet>();
        }
    
        public Nullable<int> KisiID { get; set; }
        public Nullable<int> UrunID { get; set; }
        public string UrunAd { get; set; }
        public Nullable<short> UrunFiyat { get; set; }
        public Nullable<System.DateTime> SiparisTarih { get; set; }
        public Nullable<int> AdresID { get; set; }
        public string TeslimatAdres { get; set; }
        public Nullable<byte> UrunMiktar { get; set; }
        public int SiparisID { get; set; }
        public string UrunDetay { get; set; }
        public Nullable<int> SiparisTutar { get; set; }
    
        public virtual Adresler Adresler { get; set; }
        public virtual Kisiler Kisiler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }
        public virtual ICollection<Urunler> Urunler { get; set; }
    }
}