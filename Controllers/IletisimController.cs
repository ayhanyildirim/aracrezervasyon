using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace randevu.Controllers
{
    public class IletisimController : Controller
    {
        // GET: Iletisim
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Email model)
        {
            MailMessage mailim = new MailMessage();//mail mesaj sınıfını kullandık. ve bir değişkene atadık
            mailim.To.Add("muhammedyasarrr@gmail.com");//bu değişkene bir mail adresi ekledik.
            mailim.From =new MailAddress( "muhammedyasarrr@gmail.com");
            mailim.Subject = "İstanbul Otomotiv:" + model.Konu;// konu başlığımız
            mailim.Body ="Gönderen:  "+ model.AdveSoyad +"<br>"+"Mesaj:  "+ model.Mesaj;//mesaj içeriği ve ad soyad
            mailim.IsBodyHtml = true;// mailin bir html sayfasında gelmesini söledik.
            SmtpClient smtp = new SmtpClient();//smtp bir haberleşme sunucusudur.
            smtp.Credentials = new System.Net.NetworkCredential("muhammedyasarrr@gmail.com", "Benjamin24!!");//buraya gelecek mailin smtp de tanınması için tüm mail bilgilerimizi girdik.
            smtp.Port = 587;//bu google un port numarası.
            smtp.Host = "smtp.gmail.com";//buda google un host maili sabittir.
            smtp.EnableSsl = true;//güvenlik için.
            try//try moduyla maili güvenli olarak ileticek.
            {
                smtp.Send(mailim);
                TempData["Message"] = "Mesajınız iletildi.en kısa zamanda dönüş yapılacaktır.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Mesaj gönderilemedi.Hata nedeni:" + ex.Message;
                
            }

            return View();
        }
    }
}