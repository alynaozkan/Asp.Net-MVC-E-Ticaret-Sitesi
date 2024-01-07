using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSWEBAPP.Models;


namespace FSWEBAPP.Controllers
{
    public class UrunlerController : Controller
    {
        private FLOWERSEntities db = new FLOWERSEntities();

        // GET: Urunler
        public ActionResult Index()
        {
            return View(db.Urunler.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);

        }

        [Authorize]
        public ActionResult Create()
        {
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

            // Kullanıcı var mı ve admin mi?
            if (currentUser != null && currentUser.IsAdmin.HasValue && currentUser.IsAdmin.Value)
            {
                // Admin kullanıcısı, ilgili sayfaya erişim sağlayabilir.
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunAd,Miktar,Fiyat,ResimURL,UrunID,KategoriID")] Urunler urunler)
        {
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

           
            if (currentUser != null && currentUser.IsAdmin.HasValue && currentUser.IsAdmin.Value)
            {
                
                if (ModelState.IsValid)
                {
                    db.Urunler.Add(urunler);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(urunler);
            }
            else
            {
                
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

            
            if (currentUser != null && currentUser.IsAdmin.HasValue && currentUser.IsAdmin.Value)
            {
                
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Urunler urunler = db.Urunler.Find(id);
                if (urunler == null)
                {
                    return HttpNotFound();
                }
                return View(urunler);
            }
            else
            {
                
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunAd,Miktar,Fiyat,ResimURL,UrunID,KategoriID")] Urunler urunler)
        {
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

            
            if (currentUser != null && currentUser.IsAdmin.HasValue && currentUser.IsAdmin.Value)
            {
                
                if (ModelState.IsValid)
                {
                    db.Entry(urunler).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(urunler);
            }
            else
            {
                
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

            
            if (currentUser != null && currentUser.IsAdmin.HasValue && currentUser.IsAdmin.Value)
            {
                
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Urunler urunler = db.Urunler.Find(id);
                if (urunler == null)
                {
                    return HttpNotFound();
                }
                return View(urunler);
            }
            else
            {
                
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

           
            if (currentUser != null && currentUser.IsAdmin.HasValue && currentUser.IsAdmin.Value)
            {
                
                Urunler urunler = db.Urunler.Find(id);
                db.Urunler.Remove(urunler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                
                return RedirectToAction("Index", "Home");
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
