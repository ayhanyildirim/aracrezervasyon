using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using randevu.Models;

namespace randevu.Controllers
{
    public class AracTabloesController : Controller
    {
        AracımızEntities db = new AracımızEntities();
        
        public ActionResult Index()
        {
            return View(db.Arac_tbl.ToList());
        }
        public ActionResult Details( int? id)
        {
            Arac_tbl arac_bilgileri = db.Arac_tbl.Find(id);
            return View(arac_bilgileri);
        }
        [HttpGet]
        public ActionResult Rezervation(int? id)
        {
            Arac_tbl arac_bilgileri = db.Arac_tbl.Find(id);//marka model ve fiyatı sistemden çekip göstermeye burada yarıyor viewdata.
            ViewData["Marka"] =  arac_bilgileri.Marka; 
            ViewData["Model"] = arac_bilgileri.Model;
            
            ViewData["Fiyat"] = arac_bilgileri.Fiyat;
          

            return View();
          
        }
        [HttpPost]
        public ActionResult Rezervation([Bind(Include ="RezervasyonId,AracId,TcKimlik,AdSoyad,AlmaTarihi,TeslimTarihi,Ucret")]Rezervasyon_tbl rezervasyon)//burada belirttiklerimi bağla metodu kullandık(bind).
        {
            if (ModelState.IsValid)
            {
                db.Rezervasyon_tbl.Add(rezervasyon);//eğer rezerv yerimiz müsaitse add yapacak.
                db.SaveChanges();
            }
            ViewBag.Message = "Rezervasyon işleminiz Tamamlandı.";
            return View();
        }
    }
}