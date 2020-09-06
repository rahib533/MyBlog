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
    public class KategoriyaController : Controller
    {
        // GET: Kategoriya
        public ActionResult Index()
        {
            List<Kategoriya> data = KategoriyaORM.Current.Select().Data;
            return View(data);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add2(Kategoriya k)
        {
            k.IsActive = true;
            bool data = KategoriyaORM.Current.Insert(k).Data;
            if (data==false)
            {
                return View(0);
            }
            return View(1);
        }

        [HttpGet]
        public ActionResult PhotoAdd(int id)
        {
            Kategoriya data = KategoriyaORM.Current.SelectById(id).Data[0];
            return View(data);
        }

        [HttpPost]
        public ActionResult PhotosAdd(int id,HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null)
            {
                Image img = Image.FromStream(fileUpload.InputStream);

                Bitmap bm = new Bitmap(img, AppTools.MeqaleBoyuk);
                Bitmap bm2 = new Bitmap(img, AppTools.MeqaleOrta);
                Bitmap bm3 = new Bitmap(img, AppTools.MeqaleKicik);

                string way = "/Content/KatiqoriyaSekil/BoyukYol/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string way2 = "/Content/KatiqoriyaSekil/OrtaYol/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
                string way3 = "/Content/KatiqoriyaSekil/KicikYol/" + Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);

                bm.Save(Server.MapPath(way));
                bm2.Save(Server.MapPath(way2));
                bm3.Save(Server.MapPath(way3));

                PhotosForKategoriya p = new PhotosForKategoriya();
                p.BoyukYol = way;
                p.OrtaYol = way2;
                p.KicikYol = way3;
                p.KategoriyaID = id;
                p.Main = true;
                p.MeqaleID = null;
                p.IsActive = true;

                bool data = PhotoForKategoriyaORM.Current.Insert(p).Data;

                return View(1);
            }
            return View(0);
        }

        [HttpGet]
        public ActionResult Sekil(int id)
        {
            ViewBag.Kategoriya = id;
            List<PhotosForKategoriya> data = PhotoForKategoriyaORM.Current.SelectForeignById(id).Data;
            return View(data);
        }

        [HttpGet]
        public ActionResult Sil(int id)
        {
            Kategoriya k = KategoriyaORM.Current.SelectById(id).Data[0];
            k.IsActive = false;
            KategoriyaORM.Current.Update(k);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SekilSil(int photoId)
        {
            PhotosForKategoriya p = PhotoForKategoriyaORM.Current.SelectById(photoId).Data[0];
            p.IsActive = false;
            
            int b = (int)p.KategoriyaID;

            string mainPath = @"C:/Users/hp2015/source/repos/MyBlog/MyBlog" + p.BoyukYol;
            string mainPath2 = @"C:/Users/hp2015/source/repos/MyBlog/MyBlog" + p.OrtaYol;
            string mainPath3 = @"C:/Users/hp2015/source/repos/MyBlog/MyBlog" + p.KicikYol;


            System.IO.File.Delete(mainPath);
            System.IO.File.Delete(mainPath2);
            System.IO.File.Delete(mainPath3);

            bool a = PhotoForKategoriyaORM.Current.Update(p).Data;

            if (a == false)
            {
                return View(0);
            }

            return RedirectToAction("Sekil", "Kategoriya", new { id = b });
        }


        [HttpGet]
        public ActionResult Update(int id)
        {
            Kategoriya data = KategoriyaORM.Current.SelectById(id).Data[0];
            return View(data);
        }

        [HttpPost]
        public ActionResult Update2(Kategoriya k)
        {
            Kategoriya data = KategoriyaORM.Current.SelectById(k.Id).Data[0];
            data.Adi = k.Adi;
            data.Aciqlama = k.Aciqlama;
            

            bool a = KategoriyaORM.Current.Update(data).Data;
            if (a == false)
            {
                return View(0);
            }
            return View(1);
        }
    }
}