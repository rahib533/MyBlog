using CustomProject.Entity;
using CustomProject.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBlog.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class ReyController : Controller
    {
        // GET: Rey
        public ActionResult Index()
        {
            List<Meqale> data = MeqaleORM.Current.SelectActive().Data;
            return View(data);
        }

        [HttpGet]
        public ActionResult Gor(int id)
        {
            List<Rey> data = ReyORM.Current.SelectForeignById(id).Data;
            
            return View(data);
        }

        [HttpPost]
        public ActionResult Sil(int reyId)
        {
            Rey r = ReyORM.Current.SelectById(reyId).Data[0];
            r.IsActive = false;
            int b = r.MeqaleID;
            bool a = ReyORM.Current.Update(r).Data;
            if (a==false)
            {
                return View(0);
            }
            return RedirectToAction("Gor","Rey", new { id = b });
        }
    }
}