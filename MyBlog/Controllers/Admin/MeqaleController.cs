using CustomProject.Entity;
using CustomProject.ORM;
using MyBlog.App_Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class MeqaleController : Controller
    {
        // GET: Meqale
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult IndexPW()
        {
            List<Meqale> data = MeqaleORM.Current.Select().Data;
            return PartialView(data);
        }

        [HttpGet]
        public ActionResult Add()
        {
            List<Kategoriya> data = KategoriyaORM.Current.Select().Data;
            return View(data);
        }

        [HttpPost]
        public ActionResult Add2(Meqale m, Guid UserID)
        {
            m.IsActive = true;
            m.Tarixi = DateTime.Now;
            bool a = MeqaleORM.Current.Insert(m).Data;
            if (a==false)
            {
                return View(0);
            }
            if (UserID!=null)
            {
                int MeqaleID = MeqaleORM.Current.OrderByDesc(1).Data[0].Id;
                MeqaleYazarForMeqale my = new MeqaleYazarForMeqale();
                my.UserID = UserID;
                my.MeqaleID = MeqaleID;

                bool b = MeqaleYazarForMeqaleORM.Current.Insert(my).Data;
                if (b==false)
                {
                    return View(0);
                }
                return View(1);
            }
            

            return View(1);
        }

        [HttpGet]
        public ActionResult PhotoAdd(int id)
        {
            Meqale data = MeqaleORM.Current.SelectById(id).Data[0];
            return View(data);
        }

        [HttpPost]
        public ActionResult PhotosAdd(int id, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                Bitmap bm = new Bitmap(img, AppTools.MeqaleBoyuk);
                Bitmap bm2 = new Bitmap(img, AppTools.MeqaleOrta);
                Bitmap bm3 = new Bitmap(img, AppTools.MeqaleKicik);

                string way = "/Content/MeqaleSekil/BoyukYol/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string way2 = "/Content/MeqaleSekil/OrtaYol/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string way3 = "/Content/MeqaleSekil/KicikYol/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                
                bm.Save(Server.MapPath(way));
                bm2.Save(Server.MapPath(way2));
                bm3.Save(Server.MapPath(way3));

                PhotosForMeqale p = new PhotosForMeqale();
                p.BoyukYol = way;
                p.OrtaYol = way2;
                p.KicikYol = way3;
                p.IsActive = true;
                p.MeqaleID = id;
             

                PhotoForMeqaleORM.Current.Insert(p);

                return View(1);
            }
            return View(0);
        }

        [HttpGet]
        public ActionResult Sekil(int id)
        {
            ViewBag.Meqale = id;
            List<PhotosForMeqale> data = PhotoForMeqaleORM.Current.SelectForeignById(id).Data;
            return View(data);
        }

        [HttpGet]
        public ActionResult Sil(int id)
        {
            Meqale m = MeqaleORM.Current.SelectById(id).Data[0];
            m.IsActive = false;
            bool a = MeqaleORM.Current.Update(m).Data;
            //if (a==true)
            //{
                
            //}
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SekilSil(int photoId)
        {
            PhotosForMeqale p = PhotoForMeqaleORM.Current.SelectById(photoId).Data[0];
            p.IsActive = false;
            p.KategoriyaID = null;
            int b = (int)p.MeqaleID;
            
            string mainPath = @"C:/Users/hp2015/source/repos/MyBlog/MyBlog" + p.BoyukYol;
            string mainPath2 = @"C:/Users/hp2015/source/repos/MyBlog/MyBlog" + p.OrtaYol;
            string mainPath3 = @"C:/Users/hp2015/source/repos/MyBlog/MyBlog" + p.KicikYol;


            System.IO.File.Delete(mainPath);
            System.IO.File.Delete(mainPath2);
            System.IO.File.Delete(mainPath3);

            bool a = PhotoForMeqaleORM.Current.Update(p).Data;

            if (a==false)
            {
                return View(0);
            }

            return RedirectToAction("Sekil", "Meqale", new { id = b });
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Meqale data = MeqaleORM.Current.SelectById(id).Data[0];
            return View(data);
        }

        [HttpPost]
        public ActionResult Update2(Meqale m)
        {
            Meqale data = MeqaleORM.Current.SelectById(m.Id).Data[0];
            data.Basliq = m.Basliq;
            data.Aciqlama = m.Aciqlama;
            data.Metn = m.Metn;

            bool a = MeqaleORM.Current.Update(data).Data;
            if (a==false)
            {
                return View(0);
            }
            return View(1);
        }
    }
}
