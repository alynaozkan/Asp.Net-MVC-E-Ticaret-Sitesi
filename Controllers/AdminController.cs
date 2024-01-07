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
public class AdminController : Controller
{
    private FLOWERSEntities db = new FLOWERSEntities();

    [Authorize]
    public ActionResult AdminPaneli()
    {
        var currentUser = db.Kisiler.FirstOrDefault(u => u.Email == User.Identity.Name);

        //// IsAdmin değeri varsa ve trueysa kullanıcıları listele.
        if (currentUser != null && currentUser.IsAdmin.HasValue && currentUser.IsAdmin.Value)
        {
            var users = db.Kisiler.ToList();
            ViewBag.AdminName = $"{currentUser.Ad} {currentUser.Soyad}";
            return View(users);
        }
        else
        {
            // IsAdmin null veya false ise yetkisiz erişim sayfasına yönlendir
            return RedirectToAction("YetkisizErisim", "Home");
        }

    }

    
}

