using System.Collections.Generic;

namespace FSWEBAPP.Models.ViewModels
{
    public class SiparisOlusturViewModel
    {
        public List<Adresler> UserAddresses { get; set; }
        public int SelectedAddressId { get; set; }
        public List<Sepet> SelectedItems { get; set; }
        public string UrunDetay { get; set; }
        public int ToplamTutar { get; set; }
    }
}

