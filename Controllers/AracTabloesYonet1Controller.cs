using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using randevu.Models;


namespace randevu.Controllers
{
    [Authorize]
    //Araç oluşturmak için kurduğumuz controller.
    public class AracTabloesYonet1Controller : Controller
    {

        private AracımızEntities db = new AracımızEntities();
     
        // GET: AracTabloesYonet1
        public ActionResult Index()
        {
            db.Rezervasyon_tbl.ToList();
            return View(db.Arac_tbl.ToList());
        }

        // GET: AracTabloesYonet1/Details/5
   
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arac_tbl aracTablo = db.Arac_tbl.Find(id);
            if (aracTablo == null)
            {
                return HttpNotFound();
            }
            return View(aracTablo);
        }
        
        // GET: AracTabloesYonet1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AracTabloesYonet1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AracId,Marka,Model,ModelYili,Yakit,Vites,Fiyat")] Arac_tbl aracTablo)
        {
            if (ModelState.IsValid)
            {
                db.Arac_tbl.Add(aracTablo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aracTablo);
        }

        // GET: AracTabloesYonet1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arac_tbl aracTablo = db.Arac_tbl.Find(id);
            if (aracTablo == null)
            {
                return HttpNotFound();
            }
            return View(aracTablo);
        }

        // POST: AracTabloesYonet1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AracId,Marka,Model,ModelYili,Yakit,Vites,Fiyat")] Arac_tbl aracTablo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aracTablo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aracTablo);
        }

        // GET: AracTabloesYonet1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arac_tbl aracTablo = db.Arac_tbl.Find(id);
            if (aracTablo == null)
            {
                return HttpNotFound();
            }
            return View(aracTablo);
        }

        // POST: AracTabloesYonet1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Arac_tbl aracTablo = db.Arac_tbl.Find(id);
            db.Arac_tbl.Remove(aracTablo);
            db.SaveChanges();
            //Resim Siliniyor.
            string ImageFileName = id.ToString() + ".jpg";
            string FolderPath = Path.Combine(Server.MapPath("~/Images"), ImageFileName);
            if (System.IO.File.Exists(FolderPath))
            {
                System.IO.File.Delete(FolderPath);
            }
            //resim silindi.
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SaveImages()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveImages(string hiddenId,HttpPostedFileBase UploadedImage)
        {
            if (UploadedImage.ContentLength > 0)
            {
                string ImageFileName = hiddenId + ".jpg";
                string FolderPath = Path.Combine(Server.MapPath("~/Images"), ImageFileName);
                UploadedImage.SaveAs(FolderPath);
            }
            ViewBag.Message = hiddenId + ".jpg isimli resim başarıyla yüklendi.";
            return View();
        }
        public ActionResult Rezerv()
        {
         var deger=db.Rezervasyon_tbl.ToList();
            return View(deger);
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
