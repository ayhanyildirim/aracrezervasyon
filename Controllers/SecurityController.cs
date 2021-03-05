using randevu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace randevu.Controllers
{
    public class SecurityController : Controller
    {
         AracımızEntities db = new AracımızEntities();
        // GET: Security
        public ActionResult Login()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanici t)
        {
            var bilgiler = db.Kullanici.FirstOrDefault(x => x.Ad == t.Ad && x.Sifre == t.Sifre);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Ad, false);
                return RedirectToAction("Index", "AracTabloesYonet1");
            }
            else
            {
                ViewBag.Message="Kullanıcı Adı Veya Şifre Hatalı";
                return View();
            }

           
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}