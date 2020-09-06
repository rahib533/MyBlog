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
    public class BeyenmeController : Controller
    {
        // GET: Beyenme
        public ActionResult Index()
        {
            List<Meqale> data = MeqaleORM.Current.SelectActive().Data;
            return View(data);
        }

        [HttpGet]
        public ActionResult Gor(int id)
        {
            List<LikeForMeqale> data = LikeForMeqaleORM.Current.SelectForeignByIdCom(id).Data;
            return View(data);
        }
    }
}