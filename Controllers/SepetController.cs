using FSWEBAPP.Controllers;
using FSWEBAPP.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

public class SepetController : Controller
{
    private FLOWERSEntities db = new FLOWERSEntities();

    [Authorize]
    public ActionResult Index()
    {
        var currentUser = db.Kisiler.Where(u => u.Email == User.Identity.Name).FirstOrDefault();

        if (currentUser != null)
        {
            var sepet = db.Sepet
                .Include(s => s.Siparisler)
                .Include(s => s.Urunler)
                .Where(s => s.KisiID == currentUser.KisiID)
                .ToList();

            return View(sepet);
        }

        return RedirectToAction("Login", "Kisiler");
    }

    [Authorize]
    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        Sepet sepet = db.Sepet.Find(id);

        if (sepet == null || sepet.Kisiler.Email != User.Identity.Name)
        {
            return HttpNotFound();
        }

        return View(sepet);
    }

    [Authorize]
    public ActionResult Create()
    {
        var currentUser = db.Kisiler.Where(u => u.Email == User.Identity.Name).FirstOrDefault();

        if (currentUser != null)
        {
            ViewBag.KisiID = currentUser.KisiID;
            ViewBag.SiparisID = new SelectList(db.Siparisler, "SiparisID", "UrunAd");
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "UrunAd");
            return View();
        }

        return RedirectToAction("Login", "Kisiler");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public ActionResult Create([Bind(Include = "KisiID,UrunID,UrunAd,UrunMiktar,UrunFiyat,ToplamSepet,SiparisID,SepetID")] Sepet sepet)
    {
        var currentUser = db.Kisiler.Where(u => u.Email == User.Identity.Name).FirstOrDefault();

        if (currentUser != null && ModelState.IsValid)
        {
            sepet.KisiID = currentUser.KisiID;
            db.Sepet.Add(sepet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(sepet);
    }

    [Authorize]
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        Sepet sepet = db.Sepet.Find(id);

        if (sepet == null || sepet.Kisiler.Email != User.Identity.Name)
        {
            return HttpNotFound();
        }

        ViewBag.KisiID = sepet.KisiID;
        ViewBag.SiparisID = new SelectList(db.Siparisler, "SiparisID", "UrunAd", sepet.SiparisID);
        ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "UrunAd", sepet.UrunID);
        return View(sepet);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public ActionResult Edit([Bind(Include = "KisiID,UrunID,UrunAd,UrunMiktar,UrunFiyat,ToplamSepet,SiparisID,SepetID")] Sepet sepet)
    {
        if (ModelState.IsValid)
        {
            db.Entry(sepet).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.KisiID = sepet.KisiID;
        ViewBag.SiparisID = new SelectList(db.Siparisler, "SiparisID", "UrunAd", sepet.SiparisID);
        ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "UrunAd", sepet.UrunID);
        return View(sepet);
    }

    [Authorize]
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        Sepet sepet = db.Sepet.Find(id);

        if (sepet == null || sepet.Kisiler.Email != User.Identity.Name)
        {
            return HttpNotFound();
        }

        return View(sepet);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize]
    public ActionResult DeleteConfirmed(int id)
    {
        Sepet sepet = db.Sepet.Find(id);
        db.Sepet.Remove(sepet);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    [Authorize]
    [ValidateAntiForgeryToken]
    public ActionResult SepeteEkle(int urunID)
    {
        var user = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

        if (user == null)
        {
            return RedirectToAction("Login", "Kisiler");
        }

        var urun = db.Urunler.Find(urunID);

        if (urun == null || urun.Miktar <= 0)
        {
            return RedirectToAction("Index", "Urunler");
        }

        byte sepeteEklenecekMiktar = 1;

        if (urun.Miktar < sepeteEklenecekMiktar)
        {
            sepeteEklenecekMiktar = (byte)urun.Miktar;
        }

        // Kullanıcıya ait sepeti al veya oluştur
        var sepet = db.Sepet.FirstOrDefault(s => s.KisiID == user.KisiID && s.SiparisID == null);

        if (sepet == null)
        {
            // Kullanıcıya ait sepet bulunamazsa yeni bir sepet oluştur
            sepet = new Sepet
            {
                KisiID = user.KisiID,
                SiparisID = null,
                UrunID = urun.UrunID,
                UrunAd = urun.UrunAd,
                UrunMiktar = sepeteEklenecekMiktar,
                UrunFiyat = urun.Fiyat
            };

            db.Sepet.Add(sepet);
        }
        else
        {

            // Sepet zaten varsa, mevcut ürünü güncelle
            sepet.UrunMiktar += sepeteEklenecekMiktar;
            sepet.UrunFiyat += urun.Fiyat;

        }

        db.SaveChanges();

        // İlgili bilgileri ViewBag veya ViewData üzerinden taşıyabilirsiniz
        return RedirectToAction("Index", "Sepet");
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
