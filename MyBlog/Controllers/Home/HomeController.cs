using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomProject.Common;
using CustomProject.Entity;
using CustomProject.ORM;

namespace MyBlog.Controllers.Home
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Meqale> data = MeqaleORM.Current.SelectActive().Data;
            return View(data);
        }

        public PartialViewResult LastPostPW()
        {
            List<Meqale> data = MeqaleORM.Current.OrderByDesc(10).Data;
            return PartialView(data);
        }

        public PartialViewResult KategoriyalarPW()
        {
            List<Kategoriya> data = KategoriyaORM.Current.SelectActive().Data;
            return PartialView(data);
        }

        public PartialViewResult TagPW()
        {
            List<Tag> data = TagORM.Current.SelectActive().Data;
            return PartialView(data);
        }

        [HttpGet]
        public ActionResult Meqale(int id)
        {
            Meqale data = MeqaleORM.Current.SelectById(id).Data[0];
            return View(data);
        }

        [HttpGet]
        public ActionResult Tag(int id)
        {
            List<MeqaleTagForTag> mt = MeqaleTagForTagORM.Current.SelectForeignByIdCom(id).Data;
            List<Meqale> data = new List<Meqale>();
            foreach (var item in mt)
            {
                data.Add(MeqaleORM.Current.SelectById(item.MeqaleID).Data[0]);
            }
            ViewBag.Tag = TagORM.Current.SelectById(id).Data[0].Adi;
            return View(data);
        }

        [HttpGet]
        public ActionResult Kategoriya(int id)
        {
            List<Meqale> data = MeqaleORM.Current.SelectForeignById(id).Data;
            ViewBag.Kategoriya = KategoriyaORM.Current.SelectById(id).Data[0].Adi;
            return View(data);
        }

        [HttpPost]
        public ActionResult Rey(Rey r)
        {
            int b = r.MeqaleID;
            r.IsActive = true;
            r.Tarixi = DateTime.Now;
            r.UserName = UserORM.Current.GetUserById(r.UserID);

            bool a = ReyORM.Current.Insert(r).Data;
            return RedirectToAction("Meqale","Home",new { id =  b });
        }

        [HttpGet]
        public ActionResult ReySil(int id)
        {
            Rey r = ReyORM.Current.SelectById(id).Data[0];
            r.IsActive = false;
            int b = r.MeqaleID;
            ReyORM.Current.Update(r);

            return RedirectToAction("Meqale","Home", new { id = b });
        }

        [HttpPost]
        public ActionResult Beyenmek(int meqaleId,Guid userId)
        {
            Guid g = userId;
            LikeForMeqale data = LikeForMeqaleORM.Current.Select().Data.FirstOrDefault(x => x.MeqaleID == meqaleId && x.UserID == userId);
            if (data == null)
            {
                LikeForMeqale lfm = new LikeForMeqale();
                lfm.UserID = g;
                lfm.MeqaleID = meqaleId;
                
                LikeForMeqaleORM.Current.Insert(lfm);
                return RedirectToAction("Meqale", "Home", new { id = meqaleId });
            }
            else
            {
                LikeForMeqaleORM.Current.Delete(data);
                return RedirectToAction("Meqale", "Home", new { id = meqaleId });
            }
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
    }
}