using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FSWEBAPP.Models;

namespace FSWEBAPP.Controllers
{
    public class KisilerController : Controller
    {
        private FLOWERSEntities db = new FLOWERSEntities();

        // GET: Kisiler
        public ActionResult Index()
        {
            return View(db.Kisiler.ToList());
        }

        // GET: Kisiler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisiler kisiler = db.Kisiler.Find(id);
            if (kisiler == null)
            {
                return HttpNotFound();
            }
            return View(kisiler);
        }

        // GET: Kisiler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kisiler/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ad,Soyad,Email,Sifre,KisiID")] Kisiler kisiler)
        {
            if (ModelState.IsValid)
            {
                db.Kisiler.Add(kisiler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kisiler);
        }

        // GET: Kisiler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisiler kisiler = db.Kisiler.Find(id);
            if (kisiler == null)
            {
                return HttpNotFound();
            }
            return View(kisiler);
        }

        // POST: Kisiler/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ad,Soyad,Email,Sifre,KisiID")] Kisiler kisiler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kisiler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kisiler);
        }

        // GET: Kisiler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisiler kisiler = db.Kisiler.Find(id);
            if (kisiler == null)
            {
                return HttpNotFound();
            }
            return View(kisiler);
        }

        // POST: Kisiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kisiler kisiler = db.Kisiler.Find(id);
            db.Kisiler.Remove(kisiler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        // POST: Kisiler/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.ViewModels.Register model)
        {
            if (ModelState.IsValid)
            {
                // E-posta kontrolü
                var existingUser = db.Kisiler.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılmaktadır.");
                    return View(model);
                }

                // Kullanıcıyı veritabanına ekle
                var kisi = new Kisiler
                {
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    Email = model.Email,
                    Sifre = model.Sifre,
                    IsAdmin = false //default olarak 0, admin olacak kullanıcılar databaseden 1 olarak guncellenicek.
                };

                db.Kisiler.Add(kisi);
                db.SaveChanges();

                // Kullanıcıyı oturum açık olarak işaretlemek için Web.config de timeout kullandım.
                
               
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: Kisiler/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.ViewModels.Login model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı e-posta ve şifre ile kontrol et
                var user = db.Kisiler.FirstOrDefault(u => u.Email == model.Email && u.Sifre == model.Sifre);

                if (user != null)
                {

                    FormsAuthentication.SetAuthCookie(model.Email, false);

                    // Index sayfasına yönlendirme
                    return RedirectToAction("Index", "Home");
                   
                }

                ModelState.AddModelError("Email", "Geçersiz e-posta veya şifre.");
            }

            return View(model);
        }


        [Authorize]
        public ActionResult Hesap()
        {
            var userName = User.Identity.Name;

            if (string.IsNullOrEmpty(userName))
            {
                // Kullanıcı oturumu açık değilse
                return RedirectToAction("Login", "Kisiler");
            }

            // Kullanıcıyı veritabanından al
            var user = db.Kisiler.FirstOrDefault(u => u.Email == userName);

            if (user == null)
            {
                // Kullanıcı bulunamadı
                return RedirectToAction("Login", "Kisiler");
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cikis()
        {
            // Oturumu kapat
            FormsAuthentication.SignOut();

            // Ana sayfaya yönlendir
            return RedirectToAction("Index", "Home");
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
