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
    public class MeqaleTagController : Controller
    {
        // GET: MeqaleTag
        public ActionResult Add(int id)
        {
            List<MeqaleTagForMeqale> data = MeqaleTagForMeqaleORM.Current.SelectForeignByIdCom(id).Data;
            ViewBag.Meqale = id;
            //ViewBag.Meqale = MeqaleORM.Current.SelectById(id).Data[0].Id;
            return View(data);
        }

        [HttpPost]
        public ActionResult Add2(MeqaleTagForMeqale mt)
        {
            bool a = MeqaleTagForMeqaleORM.Current.Insert(mt).Data;
            if (a==false)
            {
                return View(0);
            }
            return View(1);
        }

        [HttpGet]
        public ActionResult Sil(int id)
        {
            List<MeqaleTagForMeqale> data = MeqaleTagForMeqaleORM.Current.SelectForeignByIdCom(id).Data;
            ViewBag.Meqale = id;
            //ViewBag.Meqale = MeqaleORM.Current.SelectById(id).Data[0].Id;
            return View(data);
        }

        [HttpPost]
        public ActionResult Sil2(int MeqaleID, int TagID)
        {
            MeqaleTagForMeqale m = MeqaleTagForMeqaleORM.Current.Select().Data.FirstOrDefault(x => x.MeqaleID == MeqaleID && x.TagID == TagID);
            bool a = MeqaleTagForMeqaleORM.Current.Delete(m).Data;

            if (a==false)
            {
                return View(0);
            }
            return RedirectToAction("Sil", "MeqaleTag", new { id = MeqaleID });
        }
    }
}