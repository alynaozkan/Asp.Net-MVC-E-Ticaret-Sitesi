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
    public class AdreslerController : Controller
    {
        private FLOWERSEntities db = new FLOWERSEntities();

        // GET: Adresler
        public ActionResult Index()
        {
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account"); // Eğer kullanıcı giriş yapmamışsa, giriş yapma sayfasına yönlendir
            }

            List<Adresler> kullaniciAdresleri;

            if (currentUser.IsAdmin.HasValue && currentUser.IsAdmin.Value)
            {
                // Eğer kullanıcı admin ise, tüm adresleri getir
                kullaniciAdresleri = db.Adresler.ToList();
            }
            else
            {
                // Eğer kullanıcı admin değilse, sadece kendi adreslerini getir
                kullaniciAdresleri = db.Adresler.Where(a => a.KisiID == currentUser.KisiID).ToList();
            }

            return View(kullaniciAdresleri);
        }


        // GET: Adresler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresler adresler = db.Adresler.Find(id);
            if (adresler == null)
            {
                return HttpNotFound();
            }
            return View(adresler);
        }


        public ActionResult Create()
        {
            var userEmail = User.Identity.Name;
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == userEmail);

            if (currentUser != null)
            {
                ViewBag.KisiID = new SelectList(new List<Kisiler> { currentUser }, "KisiID", "Ad");
                return View();
            }
            else
            {
                // Hata durumunu yönetmek için gerekli işlemleri ekleyin
                return RedirectToAction("Hata", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdresTanim,AdresID")] Adresler adresler)
        {
            var userEmail = User.Identity.Name;
            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == userEmail);

            if (currentUser != null)
            {
                adresler.KisiID = currentUser.KisiID;

                if (ModelState.IsValid)
                {
                    db.Adresler.Add(adresler);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.KisiID = new SelectList(new List<Kisiler> { currentUser }, "KisiID", "Ad", adresler.KisiID);
                return View(adresler);
            }
            else
            {
                return RedirectToAction("Hata", "Home");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Adresler adresler = db.Adresler.Find(id);
            if (adresler == null)
            {
                return HttpNotFound();
            }

            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

            if (currentUser == null || (adresler.KisiID != currentUser.KisiID && !currentUser.IsAdmin.HasValue && !currentUser.IsAdmin.Value))
            {
                return RedirectToAction("YetkisizErisim", "Home");
            }

            if (!currentUser.IsAdmin.HasValue || !currentUser.IsAdmin.Value)
            {
                ViewBag.KisiID = new SelectList(db.Kisiler.Where(k => k.KisiID == currentUser.KisiID), "KisiID", "Ad", adresler.KisiID);
            }
            else
            {
                ViewBag.KisiID = new SelectList(db.Kisiler, "KisiID", "Ad", adresler.KisiID);
            }

            return View(adresler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdresTanim,KisiID,AdresID")] Adresler adresler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adresler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

            if (!currentUser.IsAdmin.HasValue || !currentUser.IsAdmin.Value)
            {
                ViewBag.KisiID = new SelectList(db.Kisiler.Where(k => k.KisiID == currentUser.KisiID), "KisiID", "Ad", adresler.KisiID);
            }
            else
            {
                ViewBag.KisiID = new SelectList(db.Kisiler, "KisiID", "Ad", adresler.KisiID);
            }

            return View(adresler);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresler adresler = db.Adresler.Find(id);
            if (adresler == null)
            {
                return HttpNotFound();
            }
            return View(adresler);
        }

        // POST: Adresler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adresler adresler = db.Adresler.Find(id);
            db.Adresler.Remove(adresler);
            db.SaveChanges();
            return RedirectToAction("Index");
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
