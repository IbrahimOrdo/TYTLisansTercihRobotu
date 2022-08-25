using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Lisans_3.Models;


namespace Lisans_3.Controllers
{ 
    public class HomeController : Controller
    {
        public TYT_LisansEntities1 DB = new TYT_LisansEntities1();
        public Class1 cs = new Class1();

        public TextBox MaxSiralama { get;  set; }
        public int MinSiralama { get;  set; }
        public int Adet { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Deneme()
        {
            cs.SehirlerList = new SelectList(DB.illers, "ilid", "sehir");
            cs.UniversitelerList = new SelectList(DB.Universiteler_Lisans, "UniID", "UniversiteAdi");
            cs.BolumlerList = new SelectList(DB.Bolumler_Lisans_Uni, "BolumID", "BolumAdi");

            var sonucListesi = DB.Lisans.ToList();
            ViewData["Sonuc"] = sonucListesi;

            return View(cs);
        }

        [HttpPost]
        public ActionResult Deneme (Class1 cs, string BlockBtn, int PT = 0 , int Dil = 0, int Tur = 0,
                                    int Statu = 0, int cb = 0, int ilid = 0, int UniID = 0,
                                    int MaxSiralama = 0, int MinSiralama = 0, int Adet = 0)
        {
            

            switch (BlockBtn)
            {
                case "Ara":

                    var query = DB.Lisans.AsQueryable();

                    if (PT > 0)
                    {
                        query = query.Where(x => x.PTid == PT);
                    }

                    if (Dil > 0)
                    {
                        query = query.Where(x => x.DilId == Dil);
                    }
                    if (Tur > 0)
                    {
                        query = query.Where(x => x.TurID == Tur);
                    }

                    if (Statu > 0)
                    {
                        query = query.Where(x => x.StatuID == Statu);
                    }

                    if (ilid > 0)
                    {
                        query = query.Where(x => x.ilid == ilid);
                    }

                    if (UniID > 0)
                    {
                        query = query.Where(x => x.UniID == UniID);
                    }
                    if (cb > 0)
                    {
                        query = query.Where(x => x.BursID == cb);
                    }
                    if (MaxSiralama > 0)
                    {
                        query = query.Where(x => x.B_Sira_2020 >= MaxSiralama);
                    }
                    if (MinSiralama > 0)
                    {
                        query = query.Where(x => x.B_Sira_2020 <= MinSiralama);
                    }
                    if (Adet > 0)
                    {
                        query = query.Take(Adet);
                    }

                    var sonucListesi = query.ToList();
                    ViewData["Sonuc"] = sonucListesi;
                   

                    cs.SehirlerList = new SelectList(DB.illers, "ilid", "sehir");
                    cs.UniversitelerList = new SelectList(DB.Universiteler_Lisans, "UniID", "UniversiteAdi");
                    cs.BolumlerList = new SelectList(DB.Bolumler_Lisans_Uni, "BolumID", "BolumAdi");
                    

                    break;


                case "Sifirla":

                    cs.SehirlerList = new SelectList(DB.illers, "ilid", "sehir");
                    cs.UniversitelerList = new SelectList(DB.Universiteler_Lisans, "UniID", "UniversiteAdi");
                    cs.BolumlerList = new SelectList(DB.Bolumler_Lisans_Uni, "BolumID", "BolumAdi");

                    var sonucListesi1 = DB.Lisans.ToList();
                    ViewData["Sonuc"] = sonucListesi1;

                    break;
                    
            }

           

            return View(cs);
        }

        public JsonResult unigetir(int p)
        {
            var UniversitelerList = (from x in DB.Universiteler_Lisans
                                     join y in DB.illers on x.iller.ilid equals y.ilid
                                     where x.iller.ilid == p
                                     select new
                                     {
                                         Text = x.UniversiteAdi,
                                         Value = x.UniID.ToString()
                                     }).ToList();
            return Json(UniversitelerList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult bolumgetir(int p)
        {
            var BolumlerList = (from x in DB.Bolumler_Lisans_Uni
                                 join y in DB.Universiteler_Lisans on x.Universiteler_Lisans.UniID equals y.UniID
                                 where x.Universiteler_Lisans.UniID == p
                                 select new
                                 {
                                     Text = x.BolumAdi,
                                     Value = x.BolumID.ToString()
                                 }).ToList();
            return Json(BolumlerList, JsonRequestBehavior.AllowGet);
        }

        
          
    }
}