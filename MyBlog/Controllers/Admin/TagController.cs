using CustomProject.Entity;
using CustomProject.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index()
        {
            List<Tag> data = TagORM.Current.Select().Data;
            return View(data);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add2(Tag t)
        {
            t.IsActive = true;
            bool a = TagORM.Current.Insert(t).Data;
            if (a==false)
            {
                return View(0);
            }
            return View(1);
        }

        [HttpGet]
        public ActionResult Sil(int id)
        {
            Tag t = TagORM.Current.SelectById(id).Data[0];
            t.IsActive = false;
            TagORM.Current.Update(t);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Tag data = TagORM.Current.SelectById(id).Data[0];
            return View(data);
        }

        [HttpPost]
        public ActionResult Update2(Tag t)
        {
            Tag data = TagORM.Current.SelectById(t.Id).Data[0];
            data.Adi = t.Adi;
            

            bool a = TagORM.Current.Update(data).Data;
            if (a == false)
            {
                return View(0);
            }
            return View(1);
        }
    }
}