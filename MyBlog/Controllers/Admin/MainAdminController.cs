using MyBlog.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBlog.Controllers.Admin
{
    public class MainAdminController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            MembershipUserCollection muc = Membership.GetAllUsers();
            return View(muc);
        }

        public ActionResult Rollar()
        {
            List<string> rollar = Roles.GetAllRoles().ToList();
            return View(rollar);
        }

        public ActionResult RolAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RolAdd(string rol)
        {
            Roles.CreateRole(rol);
            return RedirectToAction("Rollar");
        }

        public ActionResult UserRol(string id)
        {
            MembershipUserCollection mu = Membership.FindUsersByName(id);
            ViewBag.Rollar = Roles.GetAllRoles();
            return View(mu);
        }


        [HttpPost]
        public ActionResult UserRol(UserInfo ui)
        {
            Roles.AddUserToRole(ui.Adi, ui.UserRol);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UserSil(string id)
        {
            if (id != null)
            {
                Membership.DeleteUser(id);
            }
            return RedirectToAction("Index");
        }
    }
}