using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSWEBAPP.Models;
using FSWEBAPP.Models.ViewModels; 


namespace FSWEBAPP.Controllers
{
    public class SiparislerController : Controller
    {
        private FLOWERSEntities db = new FLOWERSEntities();

        // GET: Siparisler
        // GET: Siparisler
        public ActionResult Index()
        {
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<Siparisler> siparisler;

            if (currentUser.IsAdmin.HasValue && currentUser.IsAdmin.Value)
            {
                // Eğer kullanıcı admin ise, tüm siparişleri getir
                siparisler = db.Siparisler.ToList();
            }
            else
            {
                // Eğer kullanıcı admin değilse, sadece kendi siparişlerini getir
                siparisler = db.Siparisler.Where(s => s.KisiID == currentUser.KisiID).ToList();
            }

            return View(siparisler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SiparisOlusturFromSepet(List<int> selectedSepetItems)
        {
            Kisiler user = null;

            if (User.Identity.IsAuthenticated)
            {
                user = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);
            }

            if (user == null)
            {
                return RedirectToAction("Login", "Kisiler");
            }

            var userAddresses = db.Adresler.Where(a => a.KisiID == user.KisiID).ToList();

            if (!userAddresses.Any())
            {
                TempData["Error"] = "Lütfen önce bir teslimat adresi ekleyin.";
                return RedirectToAction("Index", "Adresler");
            }

            var selectedItems = selectedSepetItems != null && selectedSepetItems.Any()
                ? db.Sepet
                    .Where(s => s.KisiID == user.KisiID && selectedSepetItems.Contains(s.SepetID))
                    .ToList()
                : db.Sepet
                    .Where(s => s.KisiID == user.KisiID)
                    .ToList();

            string urunDetay = "";
            int toplamTutar = 0;

            foreach (var sepetItem in selectedItems)
            {
                urunDetay += $"{sepetItem.UrunAd}: {sepetItem.UrunMiktar} adet, ";
                toplamTutar += (int)sepetItem.UrunFiyat;
            }

            var siparisViewModel = new SiparisOlusturViewModel
            {
                UserAddresses = userAddresses,
                SelectedItems = selectedItems,
                UrunDetay = urunDetay,
                ToplamTutar = toplamTutar
            };

            return View("SecilenAdresiSec", siparisViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OnaylaVeTamamla(SiparisOlusturViewModel model)
        {
            // Kullanıcı oturum açmış mı kontrol et
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Kisiler");
            }

            // Kullanıcı bilgilerini al
            var user = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

            // Kullanıcı bulunamazsa giriş sayfasına yönlendir
            if (user == null)
            {
                return RedirectToAction("Login", "Kisiler");
            }

            // Seçilen adresi kontrol et
            var selectedAddress = db.Adresler.FirstOrDefault(a => a.AdresID == model.SelectedAddressId);

            if (selectedAddress == null)
            {
                TempData["Error"] = "Seçilen adres bulunamadı.";
                return RedirectToAction("Index", "Adresler");
            }

            // Sepeti kontrol et
            var selectedItems = db.Sepet.Where(s => s.KisiID == user.KisiID).ToList();

            if (selectedItems == null || !selectedItems.Any())
            {
                TempData["Error"] = "Seçilen ürün bulunamadı.";
                return RedirectToAction("Index", "Siparisler"); // Ya da başka bir sayfaya yönlendirme yapabilirsiniz.
            }

            // Sipariş bilgilerini oluştur
            var urunDetay = "";
            var toplamTutar = 0;

            foreach (var sepetItem in selectedItems)
            {
                urunDetay += $"{sepetItem.UrunAd}: {sepetItem.UrunMiktar} adet, ";
                toplamTutar += (int)sepetItem.UrunFiyat;
            }

            var siparis = new Siparisler
            {
                KisiID = user.KisiID,
                SiparisTarih = DateTime.Now,
                AdresID = selectedAddress.AdresID,
                TeslimatAdres = selectedAddress.AdresTanim,
                SiparisTutar = toplamTutar,
                Sepet = selectedItems,
                UrunDetay = urunDetay
            };

            try
            {
                // Siparişi veritabanına ekle
                db.Siparisler.Add(siparis);
                db.SaveChanges();

                // Sepetten seçilen ürünleri kaldır
                foreach (var item in selectedItems.ToList())
                {
                    db.Sepet.Remove(item);
                }
                db.SaveChanges();

                TempData["Success"] = "Sipariş başarıyla oluşturuldu.";
                return RedirectToAction("Index", "Siparisler");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Sipariş oluşturulurken bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index", "Siparisler");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
